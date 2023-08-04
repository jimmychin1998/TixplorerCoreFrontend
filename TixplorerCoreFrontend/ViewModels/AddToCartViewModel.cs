namespace TixplorerCoreFrontend.ViewModels
{
    public class AddToCartViewModel
    {
        public int Type { get; set; }
        public string Count { get; set; }
        public string TotalPrice { get; set; }
        public string MainPId { get; set; }
        public string Pid1 { get; set; }
        public string? Pid2 { get; set; }
        public string? Pid3 { get; set; }
        public string? Account { get; set; }
        public string? SessionCartBeforeLogin { get; set; }
    }
}
