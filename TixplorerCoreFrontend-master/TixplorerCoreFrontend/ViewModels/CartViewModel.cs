using System.ComponentModel.DataAnnotations;
using TixplorerCoreFrontend.Models;

namespace TixplorerCoreFrontend.ViewModels
{
    public class CartViewModel
    {
        public string CartItemId { get; set; }  // 該項商品在該會員的購物車的編號
        public int ProductType { get; set; }    // 記錄商品類型，0 是單票，1 是套票
        public int? ProductGroup { get; set; }   // 如果是套票則記錄商品所屬群組
        public int MemberBonus { get; set; } // 會員持有的點數
        public int Count { get; set; } // 加入購物車的商品數量
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal SubTotal { get; set; } // 此項商品的小計金額
        //紀錄購物車包含的購物車明細的主商品
        public Product mainProduct { get; set; } // 商品編號
        //紀錄購物車包含的購物車明細的子商品有哪些
        public List<Product> product { get; set; } = new List<Product>();
    }
}