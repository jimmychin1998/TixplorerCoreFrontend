//using AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TixplorerFore.Controllers
{
    public class CartController : Controller
    {
        // 使用者選購商品加入購物車
        [HttpGet]
        public ActionResult AddToCart(string? datas)
        {
            if (!string.IsNullOrEmpty(datas))
            {
                string OriginalSession = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART);

                if (OriginalSession != null && OriginalSession != "")
                {   // 如果原本有購物車 Session 資料，則將其取消，與新增的購物車 Session 資料存成同一筆
                    List<AddToCartViewModel> SessionVM = JsonSerializer.Deserialize<List<AddToCartViewModel>>(OriginalSession);
                    AddToCartViewModel VM = JsonSerializer.Deserialize<AddToCartViewModel>(datas);
                    SessionVM.Add(VM);

                    string jsondatas = JsonSerializer.Serialize(SessionVM);

                    HttpContext.Session.SetString(CDictionary.TIXPLORER_USER_CART, jsondatas);
                }
                else
                {   // 如果原本沒有購物車 Session 資料，則直接將新增的購物車 Session 資料存起來
                    List<AddToCartViewModel> SessionVM = new List<AddToCartViewModel>();
                    AddToCartViewModel VM = JsonSerializer.Deserialize<AddToCartViewModel>(datas);
                    SessionVM.Add(VM);

                    string jsondatas = JsonSerializer.Serialize(SessionVM);

                    HttpContext.Session.SetString(CDictionary.TIXPLORER_USER_CART, jsondatas);
                }

                // 會員登入狀態的加入購物車處理
                string cartId = "" ;

                if (HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != "" && HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != null)
                {
                    TixplorerContext db = new TixplorerContext();
                    var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
                    var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
                    string MemberId = Member.MId;

                    string datasStr = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART);
                    List<AddToCartViewModel> vms = JsonSerializer.Deserialize<List<AddToCartViewModel>>(datasStr);
                    vms.RemoveAll(vm => vm.SessionCartBeforeLogin == "1");  // 檢查會員登入前是否在訪客狀態加入購物車

                    bool visitorCartRemoved = false;
                    if (vms.Any(vm => string.IsNullOrEmpty(vm.SessionCartBeforeLogin)))
                    {
                        visitorCartRemoved = true;
                    }

                    foreach (AddToCartViewModel vm in vms)
                    {
                        var mainProduct = new Cart
                        {
                            MId = MemberId,
                            PId = vm.MainPId,
                            Type = vm.Type,
                            Count = Convert.ToInt32(vm.Count),
                            Totalprice = Convert.ToDecimal(vm.TotalPrice),
                        };
                        db.Carts.Add(mainProduct);
                        db.SaveChanges();
                        cartId = mainProduct.CId;

                        var vms2 = new List<AddToCartViewModel> { vm };

                        foreach (AddToCartViewModel vm2 in vms2)
                        {
                            var Cartdata = db.Carts.Where(c => c.MId == MemberId && c.PId == vm.MainPId).OrderBy(c => c.MId).LastOrDefault(c => c.MId == MemberId && c.PId == vm.MainPId);

                            if (Cartdata != null)   // 主票資料存進購物車明細中
                            {
                                CartDetail product1 = new CartDetail();
                                var product = db.Products.FirstOrDefault(p => p.PId == Cartdata.PId);
                                product1.CId = cartId;
                                product1.PId = Cartdata.PId;
                                product1.GroupId = product.GroupId;

                                db.CartDetails.Add(product1);
                                db.SaveChanges();
                            }
                            if (vm.Pid2 != null && vm.Pid2 != "")
                            {
                                CartDetail product2 = new CartDetail();
                                var product = db.Products.FirstOrDefault(p => p.PId == vm.Pid2);
                                product2.CId = cartId;
                                product2.PId = vm.Pid2;
                                product2.GroupId = product.GroupId;

                                db.CartDetails.Add(product2);
                                db.SaveChanges();
                            }
                            if (vm.Pid3 != null && vm.Pid3 != "")
                            {
                                CartDetail product3 = new CartDetail();
                                var product = db.Products.FirstOrDefault(p => p.PId == vm.Pid3);
                                product3.CId = cartId;
                                product3.PId = vm.Pid3;
                                product3.GroupId = product.GroupId;

                                db.CartDetails.Add(product3);
                                db.SaveChanges();
                            }
                        }
                    }

                    // 清空 Session 資料
                    HttpContext.Session.Remove(CDictionary.TIXPLORER_USER_CART);
                    if (visitorCartRemoved == true)
                    {
                        return RedirectToAction("CartView", new { testTest = true });
                    }
                }
            }
            return RedirectToAction("CartView");
        }

        // 讀取購物車資料
        public IActionResult CartView(bool testTest)
        {
            // 如果會員有登入，則直接讀取該會員的購物車
            if (HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != "" && HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != null)
            {
                var MemberAccount = JsonSerializer.Deserialize<AddToCartViewModel>(HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA));
                TixplorerContext db = new TixplorerContext();

                var Member = db.Members.FirstOrDefault(m => m.Email == MemberAccount.Account);
                string MemberId = Member.MId;

                // 如果該會員在登入前有加入商品進 Session 購物車，先讀取 Session 購物車並加入該會員的購物車資料表

                if (HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART) != null && HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART) != "" && testTest == false)
                {
                    var data = new
                    {
                        SessionCartBeforeLogin = "1"
                    };
                    string datas = JsonSerializer.Serialize(data);
                    return RedirectToAction("AddToCart", new { datas = datas });
                }

                TixplorerContext db2 = new TixplorerContext();
                
                var dbCartDatas = (from c in db2.Carts
                             where c.MId == MemberId
                             select c).ToList();
                List<CartViewModel> cartdatas = new List<CartViewModel>();

                if (dbCartDatas.Count == 0)   // 購物車沒有商品，帶入空購物車畫面。
                {
                    return View("CartViewNull");
                }

                if (dbCartDatas != null)
                {
                    foreach (var dbCartData in dbCartDatas)
                    {
                        CartViewModel cartdata = new CartViewModel();
                        var productData = db.Products.FirstOrDefault(p => p.PId == dbCartData.PId);
                        cartdata.CartItemId = dbCartData.CId;
                        cartdata.SubTotal = dbCartData.Totalprice;
                        cartdata.Count = dbCartData.Count;
                        cartdata.ProductType = dbCartData.Type;
                        cartdata.mainProduct = productData;
                        cartdata.MemberBonus = db.Members.FirstOrDefault(m => m.MId == MemberId).BonusPoint;

                    TixplorerContext db3 = new TixplorerContext();
                    //取MainPID的資料
                    var cartDetailDatas = db3.CartDetails.Where(cd => cd.CId == cartdata.CartItemId && cd.PId != dbCartData.PId).ToList();
                        if(cartDetailDatas != null)
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
            else // 如果是訪客身份或未登入的會員，則讀取 Session
            {
                TixplorerContext db = new TixplorerContext();

                string datasStr = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART);
                if (datasStr == null)   // 購物車沒有商品，帶入空購物車畫面。
                {
                    return View("CartViewNull");
                }
                List<AddToCartViewModel> datas = JsonSerializer.Deserialize<List<AddToCartViewModel>>(datasStr);
                List<CartViewModel> cartdatas = new List<CartViewModel>();

                if (datas != null)
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        CartViewModel cartdata = new CartViewModel();
                        var productData = db.Products.FirstOrDefault(p => p.PId == datas[i].MainPId);
                        cartdata.CartItemId = i.ToString();
                        cartdata.SubTotal = Convert.ToDecimal(datas[i].TotalPrice);
                        //cartdata.Count = Convert.ToInt32(datas[i].Count);
                        cartdata.Count = Convert.ToInt32(datas[i].Count);
                        cartdata.ProductType = datas[i].Type;
                        cartdata.mainProduct = productData;

                        if (datas[i].Pid2 != null && datas[i].Pid2 != "")
                        {
                            var subProduct = db.Products.FirstOrDefault(p => p.PId == datas[i].Pid2);
                            cartdata.product.Add(subProduct);
                        }
                        if (datas[i].Pid3 != null && datas[i].Pid3 != "")
                        {
                            var subProduct = db.Products.FirstOrDefault(p => p.PId == datas[i].Pid3);
                            cartdata.product.Add(subProduct);
                        }
                        cartdatas.Add(cartdata);
                    }
                }
                return View(cartdatas);
            }
        }

        // 移除購物車商品
        public ActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != "" && HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA) != null)
            {

                if (id != null)
                {
                    TixplorerContext db = new TixplorerContext();
                    Cart cart = db.Carts.FirstOrDefault(c => c.CId == id);
                    //CartDetail cartDetail = db.CartDetails.FirstOrDefault(cd => cd.CId == cart.CId);
                    //if (cart != null)
                    //{
                    //    if (cartDetail != null)
                    //    {
                    //        db.CartDetails.Remove(cartDetail);
                    //    }
                    //    db.Carts.Remove(cart);
                    //    db.SaveChanges();
                    //}


                    // Step 1: 刪除購物車明細資料表中的相關項目
                    var cartDetailsToRemove = db.CartDetails.Where(cd => cd.CId == id).ToList();
                    db.CartDetails.RemoveRange(cartDetailsToRemove);
                    db.SaveChanges();

                    // Step 2: 刪除購物車資料表中的相關項目
                    var cartToRemove = db.Carts.FirstOrDefault(c => c.CId == id);
                    if (cartToRemove != null)
                    {
                        db.Carts.Remove(cartToRemove);
                        db.SaveChanges();
                    }

                    if (cart.Count == 0)   // 如果商品移除後購物車沒有商品，帶入空購物車畫面。
                    {
                        return View("CartViewNull");
                    }
                }
                return RedirectToAction("CartView");
            }
            else // 如果是訪客身份或未登入的會員，則移除 Session 購物車內的商品
            {
                if (id != null)
                {
                    TixplorerContext db = new TixplorerContext();
                    string datasStr = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_CART);
                    List<AddToCartViewModel> datas = JsonSerializer.Deserialize<List<AddToCartViewModel>>(datasStr);
                    List<CartViewModel> cartdatas = new List<CartViewModel>();

                    if (datas != null)
                    {
                        for (int i = 0; i < datas.Count; i++)
                        {
                            CartViewModel cartdata = new CartViewModel();
                            var productData = db.Products.FirstOrDefault(p => p.PId == datas[i].MainPId);
                            cartdata.CartItemId = i.ToString();
                            cartdata.SubTotal = Convert.ToDecimal(datas[i].TotalPrice);
                            cartdata.Count = Convert.ToInt32(datas[i].Count);
                            cartdata.ProductType = datas[i].Type;
                            cartdata.mainProduct = productData;

                            if (datas[i].Pid2 != null && datas[i].Pid2 != "")
                            {
                                var subProduct = db.Products.FirstOrDefault(p => p.PId == datas[i].Pid2);
                                cartdata.product.Add(subProduct);
                            }
                            if (datas[i].Pid3 != null && datas[i].Pid3 != "")
                            {
                                var subProduct = db.Products.FirstOrDefault(p => p.PId == datas[i].Pid3);
                                cartdata.product.Add(subProduct);
                            }
                            cartdatas.Add(cartdata);
                        }
                    }
                    foreach (var cartItem in cartdatas)
                    {
                        if (cartItem.CartItemId == id)
                        {
                            datas.RemoveAll(item => item.MainPId == cartItem.mainProduct.PId);
                            HttpContext.Session.SetString(CDictionary.TIXPLORER_USER_CART, JsonSerializer.Serialize(datas));
                            if (datas.Count == 0)   // 如果商品移除後購物車沒有商品，帶入空購物車畫面。
                            {
                                return View("CartViewNull");
                            }
                        }
                    }
                }
                return RedirectToAction("CartView");
            }
        }
    }
}
