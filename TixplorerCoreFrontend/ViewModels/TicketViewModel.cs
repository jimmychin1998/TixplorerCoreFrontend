using TixplorerCoreFrontend.Models;

namespace TixplorerCoreFrontend.ViewModels
{
    public class TicketViewModel
    {
        //public Ticket ticket { get; set; }
        public Product product { get; set; }
        public Product hotel { get; set; }
        public Product restaurant { get; set; }
        public int isEventTicket { get; set; }
        public int isPackgeTicket { get; set; }       
        public List<string> HotelPId { get; set; }
        public List<string> HotelName { get; set; }
        public List<decimal> HotelPrice { get; set; }
        public List<string> HotelDesc { get; set; }
        public List<string> HotelImage { get; set; }
        public List<string> RestaurantPId { get; set; }
        public List<string> RestaurantName { get; set; }
        public List<decimal> RestaurantPrice { get; set; }
        public List<string> RestaurantDesc { get; set; }
        public List<string> RestaurantImage { get; set; }

       
    }
}
