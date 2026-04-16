using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.BodyType
{
    public class BodyTypeEditViewModel
    {
        public int BodyTypeId { get; set; }

        [Required]
        [Display(Name = "Body Type Name")]
        public string Name { get; set; } = null!;
    }
}
