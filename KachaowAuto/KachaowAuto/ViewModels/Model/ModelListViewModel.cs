namespace KachaowAuto.ViewModels.Model
{
    public class ModelListViewModel
    {
        public int ModelId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string EngineTypeName { get; set; } = null!;
        public decimal EngineVolume { get; set; }
        public int HorsePower { get; set; }
        public string BodyTypeName { get; set; } = null!;
    }
}
