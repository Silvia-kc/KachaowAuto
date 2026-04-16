using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.PartCategory
{
    public class PartCategoryEditViewModel
    {
        public int PartCategoryId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
    }
}
