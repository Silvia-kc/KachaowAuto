using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.EngineType
{
    public class EngineTypeEditViewModel
    {
        public int EngineTypeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
    }
}
