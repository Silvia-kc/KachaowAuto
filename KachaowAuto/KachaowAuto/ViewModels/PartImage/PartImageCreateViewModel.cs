using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.PartImage
{
    public class PartImageCreateViewModel
    {
        [Required]
        [Display(Name = "Part")]
        public int PartId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public List<PartOptionViewModel> Parts { get; set; } = new();
    }
}
