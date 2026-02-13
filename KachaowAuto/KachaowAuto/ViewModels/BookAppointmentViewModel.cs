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

        [Required, StringLength(10, MinimumLength = 10)]
        public string VIN { get; set; } = null!;

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
