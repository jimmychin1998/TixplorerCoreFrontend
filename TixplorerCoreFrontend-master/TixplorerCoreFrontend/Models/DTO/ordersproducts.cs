namespace TixplorerCoreFrontend.Models.DTO
{
    public class ordersproducts
    {
        public string PId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? TicketId { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string? Image { get; set; }
    }
}
