using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.Appointment
{
    public class AppointmentEditViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int WorkshopId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int AppointmentStatusId { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        public string? ProblemDescription { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
