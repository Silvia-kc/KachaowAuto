using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Brand
{
    public class BrandCreateViewModel
    {
        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
