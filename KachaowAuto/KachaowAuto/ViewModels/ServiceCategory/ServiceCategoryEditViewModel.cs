using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.ServiceCategory
{
    public class ServiceCategoryEditViewModel
    {
        public int ServiceCategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = null!;
    }
}
