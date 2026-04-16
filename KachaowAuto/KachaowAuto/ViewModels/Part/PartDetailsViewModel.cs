namespace KachaowAuto.ViewModels.Part
{
    public class PartDetailsViewModel
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public string? CategoryName { get; set; }
        public List<PartDetailsImageViewModel> Images { get; set; } = new();
    }
}

