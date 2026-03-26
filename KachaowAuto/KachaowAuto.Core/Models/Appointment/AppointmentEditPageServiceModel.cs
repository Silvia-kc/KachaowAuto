using KachaowAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentEditPageServiceModel
    {
        public AppointmentEditServiceModel Appointment { get; set; } = null!;

        public List<Car> Cars { get; set; } = new();
        public List<Workshop> Workshops { get; set; } = new();
        public List<KachaowAuto.Data.Models.Service> Services { get; set; } = new();
        public List<AppointmentStatus> Statuses { get; set; } = new();
    }
}
