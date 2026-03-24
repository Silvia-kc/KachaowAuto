namespace KachaowAuto.ViewModels.Workshop
{
    public class WorkshopMapItemViewModel
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
