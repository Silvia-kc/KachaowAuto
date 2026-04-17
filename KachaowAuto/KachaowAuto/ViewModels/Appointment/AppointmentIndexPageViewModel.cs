using KachaowAuto.Data.Models;

namespace KachaowAuto.ViewModels.Appointment
{
    public class AppointmentIndexPageViewModel
    {
        public AppointmentIndexFilterViewModel Filter { get; set; } = new AppointmentIndexFilterViewModel();

        public IEnumerable<AppointmentIndexViewModel> Appointments { get; set; }
            = new List<AppointmentIndexViewModel>();

        public IEnumerable<AppointmentStatus> Statuses { get; set; }
            = new List<AppointmentStatus>();

        public IEnumerable<KachaowAuto.Data.Models.Service> Services { get; set; }
            = new List<KachaowAuto.Data.Models.Service>();
    }
}
