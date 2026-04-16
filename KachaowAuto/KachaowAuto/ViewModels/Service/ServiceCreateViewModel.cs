using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Service
{
    public class ServiceCreateViewModel
    {
        [Required]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Price From")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public decimal? PriceTo { get; set; }

        [Required]
        [Display(Name = "Service Category")]
        public int ServiceCategoryId { get; set; }

        public List<ServiceCategoryOptionViewModel> Categories { get; set; } = new();
    }
}
