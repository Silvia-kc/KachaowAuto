using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Brand
{
    public class BrandEditViewModel
    {
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = null!;
    }
}
