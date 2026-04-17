namespace KachaowAuto.ViewModels.Appointment
{
    public class AppointmentIndexFilterViewModel
    {
        public int? StatusId { get; set; }
        public int? ServiceId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? SearchTerm { get; set; }
    }
}
