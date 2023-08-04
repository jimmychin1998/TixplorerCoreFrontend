namespace TixplorerCoreFrontend.Models.DTO
{
    public class SearchDTO
    {
        //搜尋用產品編號
        public string productId { get; set; }
        //搜尋用產品名稱
        public string productName { get; set; }
        //搜尋用產品所在的縣市
        public string productCounty { get; set; }
        //搜尋用產品圖片
        public string productImage { get; set; }
        //搜尋用產品價格
        public decimal productPrice { get; set; }
        //搜尋用產品特價
        public decimal? productDiscount { get; set; }
    }
}
