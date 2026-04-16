using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.PartCategory
{
    public class PartCategoryCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
    }
}
