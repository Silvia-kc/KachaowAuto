using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.ServiceCategory
{
    public class ServiceCategoryCreateViewModel
    {
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = null!;
    }
}
