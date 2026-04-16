using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Part
{
    public class PartCreateViewModel
    {
        [Required]
        [Display(Name = "Part Name")]
        public string PartName { get; set; } = null!;

        [Display(Name = "Manufacturer")]
        public string? Manufacturer { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 100000)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Category")]
        public int? PartCategoryId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public List<PartCategoryOptionViewModel> Categories { get; set; } = new();
    }
}
