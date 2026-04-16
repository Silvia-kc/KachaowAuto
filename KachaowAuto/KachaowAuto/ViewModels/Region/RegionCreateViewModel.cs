using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Region
{
    public class RegionCreateViewModel
    {
        [Required]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; } = null!;
    }
}
