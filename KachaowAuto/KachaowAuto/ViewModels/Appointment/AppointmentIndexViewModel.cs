namespace KachaowAuto.ViewModels.Appointment
{
    public class AppointmentIndexViewModel
    {
        public int AppointmentId { get; set; }
        public string CarModelName { get; set; } = null!;
        public string VIN { get; set; } = null!;
        public string WorkshopName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? ProblemDescription { get; set; }
    }
}
