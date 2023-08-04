using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.ViewModels;
using System.Text.Json;
using TixplorerCoreFrontend.Models.DTO;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TixplorerCoreFrontend.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Ticket(string? id) //id為網頁傳進去的值 https://localhost:7289/Ticket/Ticket/P2306204
                                                //https://localhost:7289/Ticket/Ticket/P23060003
        {
            TixplorerContext db = new TixplorerContext();
            TicketViewModel viewModel = new TicketViewModel();

            if (id != null && id != "")
            {

            #region 尋找會員的我的最愛資訊
            
            //從session取得登入資訊
            string sessionData = HttpContext.Session.GetString(CDictionary.TIXPLORER_USER_DATA);
            try //若有登入則抓取我的最愛資料
            {
                dynamic loginData = JsonConvert.DeserializeObject(sessionData);
                //取得信箱帳號
                string accountValue = loginData.Account;
                //查找該會員資料
                var mData = db.Members.FirstOrDefault(d => d.Email == accountValue);
                //存入ViewData["favorite"]中
                ViewData["favorite"] = mData.Favorite;
            }catch //沒有登入則設為noLogin
            {
                ViewData["favorite"] = "noLogin";
            }
            //解析JSON格式


            #endregion

            //IQueryable productid = db.Products.FirstOrDefault(p => p.PId == id);
            //以網址傳進的id做搜尋
            // 1. 以id找product資料表 
            viewModel.product = db.Products.FirstOrDefault(p => p.PId == id);   //Product類別

                if (viewModel.product.GroupId != null && viewModel.product.GroupId != 0)
                {
                    //判斷為套票
                    viewModel.isPackgeTicket = 1;

                    //從ComboGroupDetails找到所有與該GroupId對應的PId
                    var p_ids = db.ComboGroupDetails
                    .Where(x => x.GroupId == viewModel.product.GroupId)
                    .Select(x => x.PId)
                    .ToList();
                    //以PID從Products找出與之相應的TicketId
                    List<string> matchingTicketIds = db.Products.Where(t => p_ids.Contains(t.PId)).Select(t => t.TicketId).ToList();

                    // 判別票券是飯店(Type 2)還是餐廳(Type 3)，有無上架，有無庫存量
                    List<string> HotelTicketIds = db.Tickets        //飯店
                    .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 2 && t.State == 1 && t.StockNumber >= 1)
                    .Select(t => t.TicketId)
                    .ToList();

                    List<string> restaurantTicketIds = db.Tickets   //餐廳
                    .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 3 && t.State == 1 && t.StockNumber >= 1)
                    .Select(t => t.TicketId)
                    .ToList();

                    //以TicketId找到與之對應的product資料
                    //飯店
                    viewModel.hotel = db.Products.FirstOrDefault(p => p.TicketId == HotelTicketIds.First());
                    //餐廳
                    viewModel.restaurant = db.Products.FirstOrDefault(p => p.TicketId == restaurantTicketIds.First());

                    return View(viewModel);
                }
                else
                {
                    //判斷非套票
                    viewModel.isPackgeTicket = 0;

                    // 2. 以product資料表中的TicketId，從Ticket資料表中找出與其相同TicketId的資料
                    var ticket = db.Tickets.Include("Products").FirstOrDefault(t => t.TicketId == viewModel.product.TicketId); //ticket類別

                    // 3. 確認該票券是否為活動與景點票券
                    if (ticket.Type == 0 || ticket.Type == 1)

                    {
                        viewModel.isEventTicket = 1;
                    }
                    else
                    {
                        viewModel.isEventTicket = 0;
                    }
                    return View(viewModel);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult TestTicket(string? id) //id為網頁傳進去的值 https://localhost:7289/Ticket/TestTicket?id=P2306001
                                                    //https://localhost:7289/Ticket/TestTicket?id=P2306204
                                                    //https://localhost:7289/Ticket/TestTicket/P23060003
        {
            TixplorerContext db = new TixplorerContext();
            TicketViewModel viewModel = new TicketViewModel();

            if(id != null && id != "")
            {
                //IQueryable productid = db.Products.FirstOrDefault(p => p.PId == id);
                //以網址傳進的id做搜尋
                // 1. 以id找product資料表 
                viewModel.product = db.Products.FirstOrDefault(p => p.PId == id);   //Product類別

                if (viewModel.product.GroupId != null && viewModel.product.GroupId != 0)
                {
                    //判斷為套票
                    viewModel.isPackgeTicket = 1;

                    //從ComboGroupDetails找到所有與該GroupId對應的PId
                    var p_ids = db.ComboGroupDetails
                    .Where(x => x.GroupId == viewModel.product.GroupId)
                    .Select(x => x.PId)
                    .ToList();
                    //以PID從Products找出與之相應的TicketId
                    List<string> matchingTicketIds = db.Products.Where(t => p_ids.Contains(t.PId)).Select(t => t.TicketId).ToList();

                    // 判別票券是飯店(Type 2)還是餐廳(Type 3)，有無上架，有無庫存量
                    List<string> HotelTicketIds = db.Tickets        //飯店
                    .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 2 && t.State == 1 && t.StockNumber >= 1)
                    .Select(t => t.TicketId)
                    .ToList();

                    List<string> restaurantTicketIds = db.Tickets   //餐廳
                    .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 3 && t.State == 1 && t.StockNumber >= 1)
                    .Select(t => t.TicketId)
                    .ToList();

                    //以TicketId找到與之對應的product資料
                    //飯店
                    viewModel.hotel = db.Products.FirstOrDefault(p => p.TicketId == HotelTicketIds.First());
                    //餐廳
                    viewModel.restaurant = db.Products.FirstOrDefault(p => p.TicketId == restaurantTicketIds.First());

                    return View(viewModel);
                }
                else
                {
                    //判斷非套票
                    viewModel.isPackgeTicket = 0;

                    // 2. 以product資料表中的TicketId，從Ticket資料表中找出與其相同TicketId的資料
                    var ticket = db.Tickets.Include("Products").FirstOrDefault(t => t.TicketId == viewModel.product.TicketId); //ticket類別

                    // 3. 確認該票券是否為活動與景點票券
                    if (ticket.Type == 0 || ticket.Type == 1)

                    {
                        viewModel.isEventTicket = 1;
                    }
                    else
                    {
                        viewModel.isEventTicket = 0;
                    }
                    return View(viewModel);
                }
            }
            return View();

        }
        
        //id為網頁傳進去的值 https://localhost:7289/Ticket/TestTicket/P23060001
        //https://localhost:7289/Ticket/TestTicket/P23060003
        [HttpGet]
        public string GetServerData(string id)
        {
            TixplorerContext db = new TixplorerContext();     
             TicketViewModel viewModel = new TicketViewModel();

            //IQueryable productid = db.Products.FirstOrDefault(p => p.PId == id);
            //以網址傳進的id做搜尋
            // 1. 以id找product資料表 
            viewModel.product = db.Products.FirstOrDefault(p => p.PId == id);   //Product類別

            // 2. 以product資料表中的TicketId，從Ticket資料表中找出與其相同TicketId的資料
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketId == viewModel.product.TicketId); //ticket類別

            viewModel.isEventTicket = 1;

            //Destination類別  Destination.county 縣市
            //用Ticket資料表中的DestId找出與之對應的Destination資料
            var Destination = db.Destinations.FirstOrDefault(t => t.DestId == ticket.DestId);
            
            // 1. 找出所有與該活動票券相同縣市(Destination.County)的Destination的DestId
            List<string> matchingDestIds = db.Destinations.Where(d => d.County == Destination.County).Select(d => d.DestId).ToList();

            // 2. 以上一步找到的DestId與Ticket資料表做比對，找出包含該DestId的資料的TicketId
            List<string> matchingTicketIds = db.Tickets.Where(t => matchingDestIds.Contains(t.DestId)).Select(t => t.TicketId).ToList();

            // 3. 判別票券是飯店(Type 2)還是餐廳(Type 3)，有無上架，有無庫存量
            List<string> HotelTicketIds = db.Tickets        //飯店
            .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 2 && t.State == 1 && t.StockNumber >= 1)
            .Select(t => t.TicketId)
            .ToList();

            List<string> restaurantTicketIds = db.Tickets   //餐廳
            .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 3 && t.State == 1 && t.StockNumber >= 1)
            .Select(t => t.TicketId)
            .ToList();

            // 4. 以TicketId找到與之對應的product資料
            //飯店
            viewModel.HotelPId = db.Products
            .Where(p => HotelTicketIds.Contains(p.TicketId))
            .Select(p => p.PId)
            .ToList();

            viewModel.HotelName = db.Products
            .Where(p => HotelTicketIds.Contains(p.TicketId))
            .Select(p => p.Name)
            .ToList();

            viewModel.HotelPrice = db.Products
            .Where(p => HotelTicketIds.Contains(p.TicketId))
            .Select(p => p.Price)
            .ToList();

            viewModel.HotelDesc = db.Products
            .Where(p => HotelTicketIds.Contains(p.TicketId))
            .Select(p => p.Desc)
            .ToList()
            .Where(desc => desc != null)  // 過濾掉為 null 的元素
            .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
            .ToList();

            viewModel.HotelImage = db.Products
            .Where(p => HotelTicketIds.Contains(p.TicketId))
            .Select(p => p.Image)
            .ToList()
            .Where(image => image != null)  // 過濾掉為 null 的元素
            .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
            .ToList();

            //餐廳
            viewModel.RestaurantPId = db.Products
            .Where(p => restaurantTicketIds.Contains(p.TicketId))
            .Select(p => p.PId)
            .ToList();

            viewModel.RestaurantName = db.Products
            .Where(p => restaurantTicketIds.Contains(p.TicketId))
            .Select(p => p.Name)
            .ToList();

            viewModel.RestaurantPrice = db.Products
            .Where(p => restaurantTicketIds.Contains(p.TicketId))
            .Select(p => p.Price)
            .ToList();

            viewModel.RestaurantDesc = db.Products
            .Where(p => restaurantTicketIds.Contains(p.TicketId))
            .Select(p => p.Desc)
            .ToList()
            .Where(desc => desc != null)  // 過濾掉為 null 的元素
            .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
            .ToList();

            viewModel.RestaurantImage = db.Products
            .Where(p => restaurantTicketIds.Contains(p.TicketId))
            .Select(p => p.Image)
            .ToList()
            .Where(image => image != null)  // 過濾掉為 null 的元素
            .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
            .ToList();

            List<TicketDTO> hotel = new List<TicketDTO>();
            for (int i = 0; i < viewModel.HotelPId.Count; i++)
            {
                TicketDTO temphotel = new TicketDTO();

                temphotel.PId = viewModel.HotelPId[i];
                temphotel.Name = viewModel.HotelName[i];
                temphotel.Price = viewModel.HotelPrice[i];
                temphotel.Desc = viewModel.HotelDesc[i];
                temphotel.Image = viewModel.HotelImage[i];

                hotel.Add(temphotel);
            }
            List<TicketDTO> restaurant = new List<TicketDTO>();
            for (int i = 0; i < viewModel.RestaurantPId.Count; i++)
            {
                TicketDTO temphotel = new TicketDTO();

                temphotel.PId = viewModel.RestaurantPId[i];
                temphotel.Name = viewModel.RestaurantName[i];
                temphotel.Price = viewModel.RestaurantPrice[i];
                temphotel.Desc = viewModel.RestaurantDesc[i];
                temphotel.Image = viewModel.RestaurantImage[i];

                restaurant.Add(temphotel);
            }
            List<Array> Array = new List<Array>();
            Array.Add(hotel.ToArray());
            Array.Add(restaurant.ToArray());

            string json = JsonSerializer.Serialize(Array);

            return json;
        }

        [HttpGet]
        public string GetHotelSearchData(string name , string id)
        {
            TixplorerContext db = new TixplorerContext();
            TicketViewModel viewModel = new TicketViewModel();

            //IQueryable productid = db.Products.FirstOrDefault(p => p.PId == id);
            //以網址傳進的id做搜尋
            // 1. 以id找product資料表 
            viewModel.product = db.Products.FirstOrDefault(p => p.PId == id);   //Product類別

            // 2. 以product資料表中的TicketId，從Ticket資料表中找出與其相同TicketId的資料
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketId == viewModel.product.TicketId); //ticket類別

            viewModel.isEventTicket = 1;

            //Destination類別  Destination.county 縣市
            //用Ticket資料表中的DestId找出與之對應的Destination資料
            var Destination = db.Destinations.FirstOrDefault(t => t.DestId == ticket.DestId);

            // 1. 找出所有與該活動票券相同縣市(Destination.County)的Destination的DestId
            List<string> matchingDestIds = db.Destinations.Where(d => d.County == Destination.County).Select(d => d.DestId).ToList();

            // 2. 以上一步找到的DestId與Ticket資料表做比對，找出包含該DestId的資料的TicketId
            List<string> matchingTicketIds = db.Tickets.Where(t => matchingDestIds.Contains(t.DestId)).Select(t => t.TicketId).ToList();

            // 3. 判別票券是飯店(Type 2)，有無上架，有無庫存量
            List<string> HotelTicketIds = db.Tickets        //飯店
            .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 2 && t.State == 1 && t.StockNumber >= 1)
            .Select(t => t.TicketId)
            .ToList();

            if (name == null)
            {
                // 4. 以TicketId找到與之對應的product資料
                //飯店
                viewModel.HotelPId = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId))
                .Select(p => p.PId)
                .ToList();

                viewModel.HotelName = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId))
                .Select(p => p.Name)
                .ToList();

                viewModel.HotelPrice = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId))
                .Select(p => p.Price)
                .ToList();

                viewModel.HotelDesc = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId))
                .Select(p => p.Desc)
                .ToList()
                .Where(desc => desc != null)  // 過濾掉為 null 的元素
                .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();

                viewModel.HotelImage = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId))
                .Select(p => p.Image)
                .ToList()
                .Where(image => image != null)  // 過濾掉為 null 的元素
                .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();
            }
            else
            {
                // 4. 以TicketId找到與之對應的product資料
                //飯店
                viewModel.HotelPId = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.PId)
                .ToList();

                viewModel.HotelName = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Name)
                .ToList();

                viewModel.HotelPrice = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Price)
                .ToList();

                viewModel.HotelDesc = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Desc)
                .ToList()
                .Where(desc => desc != null)  // 過濾掉為 null 的元素
                .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();

                viewModel.HotelImage = db.Products
                .Where(p => HotelTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Image)
                .ToList()
                .Where(image => image != null)  // 過濾掉為 null 的元素
                .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();
            }
            

            List<TicketDTO> hotel = new List<TicketDTO>();
            for (int i = 0; i < viewModel.HotelPId.Count; i++)
            {
                TicketDTO temphotel = new TicketDTO();

                temphotel.PId = viewModel.HotelPId[i];
                temphotel.Name = viewModel.HotelName[i];
                temphotel.Price = viewModel.HotelPrice[i];
                temphotel.Desc = viewModel.HotelDesc[i];
                temphotel.Image = viewModel.HotelImage[i];

                hotel.Add(temphotel);
            }

            List<Array> Array = new List<Array>();
            Array.Add(hotel.ToArray());

            string json = JsonSerializer.Serialize(Array);

            return json;
        }

        [HttpGet]
        public string GetRestaurantSearchData(string name, string id)
        {
            TixplorerContext db = new TixplorerContext();
            TicketViewModel viewModel = new TicketViewModel();

            //IQueryable productid = db.Products.FirstOrDefault(p => p.PId == id);
            //以網址傳進的id做搜尋
            // 1. 以id找product資料表 
            viewModel.product = db.Products.FirstOrDefault(p => p.PId == id);   //Product類別

            // 2. 以product資料表中的TicketId，從Ticket資料表中找出與其相同TicketId的資料
            var ticket = db.Tickets.FirstOrDefault(t => t.TicketId == viewModel.product.TicketId); //ticket類別

            viewModel.isEventTicket = 1;

            //Destination類別  Destination.county 縣市
            //用Ticket資料表中的DestId找出與之對應的Destination資料
            var Destination = db.Destinations.FirstOrDefault(t => t.DestId == ticket.DestId);

            // 1. 找出所有與該活動票券相同縣市(Destination.County)的Destination的DestId
            List<string> matchingDestIds = db.Destinations.Where(d => d.County == Destination.County).Select(d => d.DestId).ToList();

            // 2. 以上一步找到的DestId與Ticket資料表做比對，找出包含該DestId的資料的TicketId
            List<string> matchingTicketIds = db.Tickets.Where(t => matchingDestIds.Contains(t.DestId)).Select(t => t.TicketId).ToList();

            // 3. 判別票券是餐廳(Type 3)，有無上架，有無庫存量
            List<string> restaurantTicketIds = db.Tickets   //餐廳
            .Where(t => matchingTicketIds.Contains(t.TicketId) && t.Type == 3 && t.State == 1 && t.StockNumber >= 1)
            .Select(t => t.TicketId)
            .ToList();

            // 4. 以TicketId找到與之對應的product資料           
            if (name == null)
            {
                //餐廳
                viewModel.RestaurantPId = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId))
                .Select(p => p.PId)
                .ToList();

                viewModel.RestaurantName = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId))
                .Select(p => p.Name)
                .ToList();

                viewModel.RestaurantPrice = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId))
                .Select(p => p.Price)
                .ToList();

                viewModel.RestaurantDesc = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId))
                .Select(p => p.Desc)
                .ToList()
                .Where(desc => desc != null)  // 過濾掉為 null 的元素
                .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();

                viewModel.RestaurantImage = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId))
                .Select(p => p.Image)
                .ToList()
                .Where(image => image != null)  // 過濾掉為 null 的元素
                .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();
            }
            else 
            {
                //餐廳
                viewModel.RestaurantPId = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.PId)
                .ToList();

                viewModel.RestaurantName = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Name)
                .ToList();

                viewModel.RestaurantPrice = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Price)
                .ToList();

                viewModel.RestaurantDesc = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Desc)
                .ToList()
                .Where(desc => desc != null)  // 過濾掉為 null 的元素
                .Select(desc => desc!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();

                viewModel.RestaurantImage = db.Products
                .Where(p => restaurantTicketIds.Contains(p.TicketId) && p.Name.Contains(name))
                .Select(p => p.Image)
                .ToList()
                .Where(image => image != null)  // 過濾掉為 null 的元素
                .Select(image => image!)        // 將非 null 元素轉換為不能為 null 的字串
                .ToList();
            }
            List<TicketDTO> restaurant = new List<TicketDTO>();
            for (int i = 0; i < viewModel.RestaurantPId.Count; i++)
            {
                TicketDTO temphotel = new TicketDTO();

                temphotel.PId = viewModel.RestaurantPId[i];
                temphotel.Name = viewModel.RestaurantName[i];
                temphotel.Price = viewModel.RestaurantPrice[i];
                temphotel.Desc = viewModel.RestaurantDesc[i];
                temphotel.Image = viewModel.RestaurantImage[i];

                restaurant.Add(temphotel);
            }
            List<Array> Array = new List<Array>();
            Array.Add(restaurant.ToArray());

            string json = JsonSerializer.Serialize(Array);

            return json;
        }
    }
}
