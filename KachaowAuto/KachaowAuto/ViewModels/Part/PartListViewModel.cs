namespace KachaowAuto.ViewModels.Part
{
    public class PartListViewModel
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public string? FirstImageUrl { get; set; }
    }
}
