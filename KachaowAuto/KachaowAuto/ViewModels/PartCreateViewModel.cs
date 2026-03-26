using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels
{
    public class PartCreateViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Part name")]
        public string PartName { get; set; } = null!;

        [StringLength(100)]
        public string? Manufacturer { get; set; }

        [StringLength(100)]
        [Display(Name = "Part number")]
        public string? PartNumber { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 100000)]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "Category")]
        public int? PartCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
