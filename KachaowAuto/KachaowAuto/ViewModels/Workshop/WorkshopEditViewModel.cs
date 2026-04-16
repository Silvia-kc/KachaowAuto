using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Workshop
{
    public class WorkshopEditViewModel
    {
        public int WorkshopId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public List<WorkshopRegionOptionViewModel> Regions { get; set; } = new();
    }
}
