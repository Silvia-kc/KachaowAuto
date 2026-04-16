namespace KachaowAuto.ViewModels.Service
{
    public class ServiceListViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
