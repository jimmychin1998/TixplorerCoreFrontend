namespace TixplorerCoreFrontend.Models.DTO
{
    public class ordersDTO
    {
        public string OrderId { get; set; } = null!;

        public string? MId { get; set; }

        public decimal Totalprice { get; set; }

        public DateTime Orderdate { get; set; }

        public DateTime? OrderdateEnd { get; set; }

        public int State { get; set; }
    }
}
