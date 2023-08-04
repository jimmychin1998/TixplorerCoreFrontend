using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Reflection.Metadata;
using TixplorerCoreFrontend.Models;
using TixplorerCoreFrontend.Models.DTO;
using TixplorerCoreFrontend.ViewModels;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TixplorerCoreFrontend.Controllers
{
    public class SearchController : Controller
    {
        TixplorerContext _context;

        public SearchController(TixplorerContext content)
        {//DI注入
            _context = content;
        }

        [Route("search")]
        public async Task<ActionResult> List(string? productType, string? productCondition, string? txtKeyword)
        {//搜尋全部物件
            SearchViewModel vm = new SearchViewModel();
            vm.productType = "0";
            vm.productCondition = "0";
            vm.txtKeyword = "";
            if (productType != null || productCondition != null || txtKeyword != null)
            {
                if (productType != null)
                    vm.productType = productType;
                if (productCondition != null)
                    vm.productCondition = productCondition;
                if (txtKeyword != null)
                    vm.txtKeyword = txtKeyword;
            }
            return View(vm);
        }

        public List<SearchDTO> innerJoinSearch(List<Product> productDatas)
        {
            List<SearchDTO> datas = new List<SearchDTO>();

            foreach (var product in productDatas)
            {
                if (product.TicketId != null)
                {
                    var ticketData = _context.Tickets.FirstOrDefault(t => t.TicketId == product.TicketId);
                    var destData = _context.Destinations.FirstOrDefault(d => d.DestId == ticketData.DestId);
                    //存取VM資料
                    SearchDTO data = new SearchDTO();
                    data.productId = product.PId;
                    data.productName = product.Name;
                    data.productImage = product.Image;
                    if (destData == null)
                        data.productCounty = "N/A";
                    else
                        data.productCounty = destData.County;
                    data.productPrice = product.Price;
                    data.productDiscount = product.DiscountPrice;
                    datas.Add(data);
                }
                //找到VM需要的資料「產品所在的縣市」
            }
            return datas;
        }

        public async Task<List<SearchDTO>> getProductDatas(string? productType, string? productCondition, string? txtKeyword)
        {//取得要顯示的產品資料
         //將傳來的搜尋條件先轉換為SQL使用之數字編號
            int pType = typeConvertPType(productType);

            //如果有選擇搜尋類型
            //如果搜尋類別為「旅遊類別」
            if (pType == 1)
            {
                int pCondition = typeConvertPCondition(productCondition);
                //如果沒有輸入關鍵字，則商品資料全撈
                if (txtKeyword == null || txtKeyword == "")
                {
                    var ticketDatas = _context.Tickets.Where(t => t.Type == pCondition);
                    TixplorerContext db = new TixplorerContext();

                    List<Product> productDatas = new List<Product>();
                    foreach (var ticket in ticketDatas)
                    {

                        var products = db.Products.Where(p => p.TicketId == ticket.TicketId);
                        productDatas.AddRange(products);

                    }
                    return innerJoinSearch(productDatas);
                }
                //如果有輸入關鍵字，則撈指定商品資料
                else
                {
                    var ticketDatas = _context.Tickets.Where(t => t.Type == pCondition);
                    TixplorerContext db = new TixplorerContext();

                    List<Product> productDatas = new List<Product>();
                    foreach (var ticket in ticketDatas)
                    {
                        var products = db.Products.Where(p => p.TicketId == ticket.TicketId && p.Name.Contains(txtKeyword));
                        productDatas.AddRange(products);
                    }
                    return innerJoinSearch(productDatas);
                }
            }
            //如果搜尋類別為「縣市」
            else if (pType == 2)
            {
                //如果沒有輸入關鍵字，則商品資料全撈
                if (txtKeyword == null || txtKeyword == "")
                {
                    var destDatas = _context.Destinations.Where(d => d.County == productCondition);
                    TixplorerContext db = new TixplorerContext();

                    List<Ticket> ticketDatas = new List<Ticket>();
                    foreach (var destData in destDatas)
                    {

                        var tickets = db.Tickets.Where(t => t.DestId == destData.DestId);
                        ticketDatas.AddRange(tickets);
                    }
                    TixplorerContext db2 = new TixplorerContext();

                    List<Product> productDatas = new List<Product>();
                    foreach (var ticket in ticketDatas)
                    {
                        var products = db2.Products.Where(p => p.TicketId == ticket.TicketId);
                        productDatas.AddRange(products);
                    }
                    return innerJoinSearch(productDatas);
                }
                //如果有輸入關鍵字，則撈指定商品資料
                else
                {
                    var destDatas = _context.Destinations.Where(d => d.County == productCondition);
                    TixplorerContext db = new TixplorerContext();

                    List<Ticket> ticketDatas = new List<Ticket>();
                    foreach (var destData in destDatas)
                    {
                        var tickets = db.Tickets.Where(t => t.DestId == destData.DestId);
                        ticketDatas.AddRange(tickets);
                    }
                    TixplorerContext db2 = new TixplorerContext();

                    List<Product> productDatas = new List<Product>();
                    foreach (var ticket in ticketDatas)
                    {
                        var products = db2.Products.Where(p => p.TicketId == ticket.TicketId && p.Name.Contains(txtKeyword));
                        productDatas.AddRange(products);
                    }
                    return innerJoinSearch(productDatas);
                }
            }
            //如果沒有選擇搜尋類型
            else
            {
                //如果沒有輸入關鍵字，則商品資料全撈
                if (txtKeyword == null || txtKeyword == "")
                {
                    var productDatas = _context.Products.Select(p => p);
                    return innerJoinSearch(productDatas.ToList());
                }
                //如果有輸入關鍵字，則撈指定商品資料
                else
                {
                    var productDatas = _context.Products.Where(p => p.Name.Contains(txtKeyword));
                    return innerJoinSearch(productDatas.ToList());
                }
            }
        }

        public int typeConvertPType(string productType)
        {//轉換產品搜尋類別為數值
            switch (productType)
            {
                case "旅遊類別":
                    return 1;
                case "縣市":
                    return 2;
                default:
                    return 0;
            }
        }

        public int typeConvertPCondition(string productCondition)
        {//轉換產品搜尋細項為數值→僅轉換旅遊類別、縣市用字串搜尋
            switch (productCondition)
            {
                //產品搜尋類別為「旅遊類別」時
                case "景點":
                    return 1;
                case "飯店":
                    return 2;
                case "旅館":
                    return 3;
                default:
                    //條件為「活動」時
                    return 0;
            }
        }
    }
}
