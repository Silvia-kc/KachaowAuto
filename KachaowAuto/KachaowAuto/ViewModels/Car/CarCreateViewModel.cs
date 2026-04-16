using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Car
{
    public class CarCreateViewModel
    {
        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required]
        [Display(Name = "VIN")]
        public string VIN { get; set; } = null!;

        public List<CarModelOptionViewModel> Models { get; set; } = new();
    }
}
