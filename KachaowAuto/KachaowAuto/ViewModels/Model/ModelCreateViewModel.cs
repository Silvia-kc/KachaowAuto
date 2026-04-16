using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Model
{
    public class ModelCreateViewModel
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; } = null!;

        [Required]
        [Display(Name = "Engine Type")]
        public int EngineTypeId { get; set; }

        [Required]
        [Display(Name = "Engine Volume")]
        public decimal EngineVolume { get; set; }

        [Required]
        [Display(Name = "Horse Power")]
        public int HorsePower { get; set; }

        [Required]
        [Display(Name = "Body Type")]
        public int BodyTypeId { get; set; }

        public List<ModelBrandOptionViewModel> Brands { get; set; } = new();
        public List<ModelEngineTypeOptionViewModel> EngineTypes { get; set; } = new();
        public List<ModelBodyTypeOptionViewModel> BodyTypes { get; set; } = new();
    }
}
