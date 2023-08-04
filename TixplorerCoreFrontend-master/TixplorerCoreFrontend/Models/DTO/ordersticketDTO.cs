namespace TixplorerCoreFrontend.Models.DTO
{
    public class ordersticketDTO
    {
        public string TicketId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Type { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
