using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Region
{
    public class RegionEditViewModel
    {
        public int RegionId { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; } = null!;
    }
}
