using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels
{
    public class BookAppointmentViewModel
    {
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ModelId { get; set; }

        [Required]
        [Range(1950, 2100)]
        public int Year { get; set; }

        [Required, StringLength(8)]
        public string VIN { get; set; } = null!;
        [Required]
        public int EngineTypeId { get; set; }
        [Required]
        public int BodyTypeId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int WorkshopId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required, MaxLength(1000)]
        public string ProblemDescription { get; set; } = null!;
    }
}
