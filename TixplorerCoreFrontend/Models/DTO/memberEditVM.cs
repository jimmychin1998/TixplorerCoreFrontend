namespace TixplorerCoreFrontend.Models.DTO
{
    public class memberEditVM
    {
        public string MId { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; } = null!;

        public string? Favorite { get; set; }

        public int BonusPoint { get; set; }


    }
}
