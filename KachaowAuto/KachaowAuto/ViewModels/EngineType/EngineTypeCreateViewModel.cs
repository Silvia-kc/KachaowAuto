using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.EngineType
{
        public class EngineTypeCreateViewModel
        {
            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; } = null!;
        }
    
}
