namespace KachaowAuto.ViewModels.Car
{
    public class CarListViewModel
    {
        public int CarId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public int Year { get; set; }
        public string VIN { get; set; } = null!;
        public int AppointmentsCount { get; set; }
    }
}
