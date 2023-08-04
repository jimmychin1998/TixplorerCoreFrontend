using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Text.Json;
using System.Text;
using System.Web;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.ViewModels;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Reflection.Metadata;
using System;

namespace TixplorerFore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
            TixplorerContext db = new TixplorerContext();
            var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
            string MemberId = Member.MId;

            List<CartViewModel> cartdatas = new List<CartViewModel>();
            var datas = (from c in db.Carts
                         where c.MId == MemberId
                         select c).ToList();
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    CartViewModel cartdata = new CartViewModel();
                    var productData = db.Products.FirstOrDefault(p => p.PId == data.PId);
                    cartdata.CartItemId = data.CId;
                    cartdata.SubTotal = data.Totalprice;
                    cartdata.Count = data.Count;
                    cartdata.ProductType = data.Type;
                    cartdata.mainProduct = productData;
                    cartdata.MemberBonus = db.Members.FirstOrDefault(m => m.MId == MemberId).BonusPoint;

                    TixplorerContext db3 = new TixplorerContext();
                    //取MainPID的資料
                    var cartDetailDatas = db3.CartDetails.Where(cd => cd.CId == cartdata.CartItemId && cd.PId != data.PId).ToList();
                    if (cartDetailDatas != null)
                    {
                        foreach (var cartDetailData in cartDetailDatas)
                        {
                            TixplorerContext db4 = new TixplorerContext();
                            var subProduct = db4.Products.FirstOrDefault(p => p.PId == cartDetailData.PId);
                            cartdata.product.Add(subProduct);
                        }
                    }
                    cartdatas.Add(cartdata);
                }
            }
            return View(cartdatas);
        }

        [HttpGet]
        public ActionResult BuyNow(string? datas)    // 直接結帳
        {
            if (HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != "" && HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != null)
            {
                var buyNowOrderData = datas;
                HttpContext.Session.SetString("buyNowOrderDatas", buyNowOrderData);

                var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
                TixplorerContext db = new TixplorerContext();
                var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
                string MemberId = Member.MId;

                List<AddToCartViewModel> buyNowData = new List<AddToCartViewModel>();
                AddToCartViewModel buyNow = JsonSerializer.Deserialize<AddToCartViewModel>(datas);
                buyNowData.Add(buyNow);
                List<CartViewModel> buyNowDataToView = new List<CartViewModel>();
                if (!string.IsNullOrEmpty(datas))
                {
                    if (buyNowData != null)
                    {
                        for (int i = 0; i < buyNowData.Count; i++)
                        {
                            CartViewModel cartdata = new CartViewModel();
                            var productData = db.Products.FirstOrDefault(p => p.PId == buyNowData[i].MainPId);
                            cartdata.CartItemId = i.ToString();
                            cartdata.SubTotal = Convert.ToDecimal(buyNowData[i].TotalPrice);
                            cartdata.Count = Convert.ToInt32(buyNowData[i].Count);
                            cartdata.ProductType = buyNowData[i].Type;
                            cartdata.mainProduct = productData;
                            cartdata.MemberBonus = db.Members.FirstOrDefault(m => m.MId == MemberId).BonusPoint;

                            if (buyNowData[i].Pid2 != null && buyNowData[i].Pid2 != "")
                            {
                                var subProduct = db.Products.FirstOrDefault(p => p.PId == buyNowData[i].Pid2);
                                cartdata.product.Add(subProduct);
                            }
                            if (buyNowData[i].Pid3 != null && buyNowData[i].Pid3 != "")
                            {
                                var subProduct = db.Products.FirstOrDefault(p => p.PId == buyNowData[i].Pid3);
                                cartdata.product.Add(subProduct);
                            }
                            buyNowDataToView.Add(cartdata);
                        }
                    }
                }
                return View(buyNowDataToView);
            }
            else
            {
                return Redirect("http://localhost:5173/Login");
            }
        }

        [HttpPost]  // 處理結帳時點數折抵的運算
        public IActionResult UseDiscount(bool useTcoin, decimal tcoinAmount, decimal totalAmount)
        {
            decimal discountAmount = 0; // 設定點數折扣金額
            if (useTcoin && tcoinAmount <= totalAmount) // 如果使用 T 幣折抵，則將優惠金額設為 T 幣的金額
            {
                discountAmount = tcoinAmount;
            }
            decimal finalAmount = totalAmount - discountAmount; // 計算總結帳金額
            return Json(new { finalAmount = finalAmount }); // 傳遞計算後的最終金額回傳到前端
        }

        [HttpGet]   // 綠界支付的程式碼
        public ActionResult Payment(string choosePayment, string totalAmount)
        {
            TixplorerContext db = new TixplorerContext();
            var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
            var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
            string MemberId = Member.MId;
            string ChoosePayments = choosePayment;
            string TotalAmount = totalAmount;
            ViewBag.OrderTotalAmount = totalAmount;

            var orderId = "";  // 生成訂單編號

            var latestOrderId = db.Orders.OrderByDescending(o => o.OrderId).Select(o => o.OrderId).FirstOrDefault();

            if (!string.IsNullOrEmpty(latestOrderId))
            {
                // 解析最新的訂單編號，例如 O23070961，解析出年份、月份和流水編號
                int currentYear = DateTime.Now.Year;
                int currentMonth = DateTime.Now.Month;
                int currentDay = DateTime.Now.Day;
                DateTime now = DateTime.Now;
                int currentTime = now.Hour * 100 + now.Minute;
                string yearPart = currentYear.ToString().Substring(2);
                string monthPart = currentMonth.ToString("D2");
                string DayPart = currentDay.ToString("D2");
                string TimePart = currentTime.ToString();
                string serialNumberPart = latestOrderId.Substring(5);

                // 將流水編號轉換為數字並加1，然後格式化為 4 位數字
                int serialNumber = int.Parse(serialNumberPart);
                serialNumber++;
                string formattedSerialNumber = serialNumber.ToString("D4");

                // 生成下一筆編號
                string nextOrderId = $"TixO{yearPart}{monthPart}{DayPart}{TimePart}{formattedSerialNumber}";
                // 將生成的下一筆編號賦值給 orderId
                orderId = nextOrderId;
            }

            //需填入你的網址
            var website = $"https://localhost:7289";
            var order = new Dictionary<string, string>
            {
                //綠界需要的參數
                { "MerchantTradeNo",  orderId}, // 訂單編號
                { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},  // 訂單生成日期
                { "TotalAmount",  TotalAmount},   // 訂單實際應付款金額
                { "TradeDesc",  "無"},   // 交易描述
                { "ItemName",  "Tixplorer票券"},  // 商品名稱，因有字數及顯示限制，改在此顯示商店名稱
                { "CustomField1",  ""}, // 客製化欄位
                { "CustomField2",  ""}, // 客製化欄位
                { "CustomField3",  ""}, // 客製化欄位
                { "CustomField4",  ""}, // 客製化欄位
                { "ReturnURL",  $"{website}/Checkout/AddPayInfo"},  // Server 端付款完成通知回傳網址
                { "OrderResultURL", $"{website}/Checkout/beforePayInfo/"},    // Client 端回傳付款結果網址
                { "MerchantID",  "2000132"},    // 綠界店家測試用編號，固定填入 2000132
                { "IgnorePayment",  ""},    // 隱藏付款方式
                { "PaymentType",  "aio"},   // 交易類型，固定填入 aio
                { "ChoosePayment",  ChoosePayments},     // 預設付款方式直接設定為 ALL
                { "EncryptType",  "1"},     // CheckMacValue加密類型，預設為 1
            };
            order["CheckMacValue"] = GetCheckMacValue(order);
            return View(order);
        }

        private string GetCheckMacValue(Dictionary<string, string> order)   // 設定綠界的檢查碼
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();
            var checkValue = string.Join("&", param);
            var hashKey = "5294y06JbISpM5x9";   //測試用的 HashKey
            var HashIV = "v77hoKGq4kWxNNIS";    //測試用的 HashIV
            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";
            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();
            checkValue = GetSHA256(checkValue);
            return checkValue.ToUpper();
        }
        private string GetSHA256(string value)  // 使用 SHA256 雜湊演算法，用以加密傳遞至綠界的資料
        {
            var result = new StringBuilder();
            var sha256 = SHA256Managed.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        [HttpPost]
        public IActionResult AddOrder(bool addOrder, string orderTotalAmount)   // 建立訂單
        {
            var buyNowDatas = HttpContext.Session.GetString("buyNowOrderDatas");    // 如果是直接結帳，取得直接結帳的商品資料

            if (addOrder == true)
            {
                // 建立訂單資料的前置作業
                TixplorerContext db = new TixplorerContext();
                var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
                var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
                string MemberId = Member.MId;
                decimal TotalAmount = Convert.ToDecimal(orderTotalAmount);
                int OrderCount = 0;

                // 開始建立訂單資料
                if (buyNowDatas != null)    // 直接結帳的話，使用直接結帳的 Session 建立訂單
                {
                    List<AddToCartViewModel> buyNowData = new List<AddToCartViewModel>();
                    AddToCartViewModel buyNowAddOrder = JsonSerializer.Deserialize<AddToCartViewModel>(buyNowDatas);

                    Order newOrder = new Order();
                    newOrder.MId = MemberId;
                    newOrder.PId = buyNowAddOrder.MainPId;
                    newOrder.Orderdate = DateTime.Now; // 訂單成立日期
                    newOrder.OrderdateEnd = null;
                    newOrder.State = 0; // 訂單建立時預設為 0 = 尚未付款
                    newOrder.Count = Convert.ToInt32(buyNowAddOrder.Count);
                    newOrder.Totalprice = TotalAmount;
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    string newOrderId = newOrder.OrderId; // 訂單建立後取得此筆新訂單的OrderID

                    // 建立訂單後，開始建立訂單明細
                    OrderDetail newOrderDetail = new OrderDetail();
                    newOrderDetail.OrderId = newOrderId;
                    newOrderDetail.Type = buyNowAddOrder.Type;
                    newOrderDetail.PId = buyNowAddOrder.Pid1;

                    db.OrderDetails.Add(newOrderDetail);
                    db.SaveChanges();
                }
                else
                {
                    Order newOrder = new Order();
                    newOrder.MId = MemberId; // 會員ID
                    newOrder.PId = db.Carts.OrderByDescending(c => c.CId).LastOrDefault(c => c.MId == MemberId).PId;
                    newOrder.Orderdate = DateTime.Now; // 訂單成立日期
                    newOrder.OrderdateEnd = null;
                    newOrder.State = 0; // 訂單建立時預設為 0 = 尚未付款
                    // 訂單商品總數量，直接計算該會員購物車總數量
                    var cartData = db.Carts.Where(c => c.MId == MemberId).ToList();
                    OrderCount = cartData.Sum(c => c.Count);
                    newOrder.Count = OrderCount;
                    newOrder.Totalprice = TotalAmount; // 訂單總付款價格
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    string newOrderId = newOrder.OrderId; // 訂單建立後取得此筆新訂單的OrderID

                    // 建立訂單後，開始建立訂單明細
                    foreach (var cart in cartData)
                    {
                        OrderDetail newOrderDetail = new OrderDetail();
                        newOrderDetail.OrderId = newOrderId;
                        newOrderDetail.Type = cart.Type;
                        newOrderDetail.PId = cart.PId;

                        db.OrderDetails.Add(newOrderDetail);
                        db.SaveChanges();
                    }

                    // 建立訂單後，開始清除該會員的購物車資料與購物車明細
                    var cartsToRemove = db.Carts.Where(c => c.MId == MemberId).ToList();
                    var cartDetailsToRemove = db.CartDetails.Where(cd => cartsToRemove.Select(c => c.CId).Contains(cd.CId)).ToList();
                    db.CartDetails.RemoveRange(cartDetailsToRemove);
                    db.Carts.RemoveRange(cartsToRemove);
                    db.SaveChanges();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult beforePayInfo(IFormCollection id)
        {
            return RedirectToAction("PayInfo", new { id = id });
        }

        // 取得付款資訊，更新資料庫
        [HttpGet]
        public ActionResult PayInfo(IFormCollection id)
        {
            TixplorerContext db = new TixplorerContext();
            var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
            var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
            string MemberId = Member.MId;
            var data = new Dictionary<string, string>();

            foreach (string key in id.Keys)
            {
                data.Add(key, id[key]);
            }
            var order = db.Orders.Where(m => m.MId == MemberId)
                                 .OrderByDescending(m => m.OrderId) // 按照 OrderId 降序排序
                                 .FirstOrDefault();
            if (order != null)
            {
                order.State = 1; // 訂單狀態改為 1，表示已付款
                order.OrderdateEnd = DateTime.Now;
                db.SaveChanges();
            }
            return View("PaymentResult", data);
        }
        /* 以下為優惠券邏輯 未完成 暫停使用 */
        /*

        public IActionResult CouponList() // 會員目前持有的優惠券清單
        {
            var MemberId = "M20010001"; // 獲取會員ID 測試預設為M20010001
            TixplorerContext db = new TixplorerContext();
            var CouponList = from clist in db.CouponLists
                             where clist.MId == MemberId
                             select new MemberCouponVM()
                             {
                                 MemberId = MemberId,
                                 CouponId = clist.CouponId,
                                 CouponName = db.Coupons.FirstOrDefault(c => c.CouponId == clist.CouponId).Name,
                                 ProductId = db.Coupons.FirstOrDefault(c => c.CouponId == clist.CouponId).PId,
                                 DiscountType = db.Coupons.FirstOrDefault(c => c.CouponId == clist.CouponId).DiscountType,
                                 Discount = db.Coupons.FirstOrDefault(c => c.CouponId == clist.CouponId).Discount
                             };
            return PartialView(CouponList);
        }

        [HttpPost]
        public IActionResult UseCoupon(string couponId, int discountType, int discount) // 處理使用者點選的優惠券
        {
            // 將優惠券相關資訊儲存至 Session
            HttpContext.Session.SetString("SelectedCouponId", couponId);
            HttpContext.Session.SetInt32("SelectedDiscountType", discountType);
            HttpContext.Session.SetInt32("SelectedDiscount", discount);

            return Ok(); // 或回傳其他合適的結果
        }
        
        */
    }
}
