namespace KachaowAuto.ViewModels.PartRequest
{
    public class PartRequestMyRequestViewModel
    {
        public int PartRequestId { get; set; }

        public int PartId { get; set; }
        public string PartName { get; set; } = null!;

        public int Quantity { get; set; }

        public string Status { get; set; } = null!;

        public string? Note { get; set; }

        public string? AdminNote { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime? ProcessedAt { get; set; }
    }
}
