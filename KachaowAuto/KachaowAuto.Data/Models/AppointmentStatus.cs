using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class AppointmentStatus
    {
        public int AppointmentStatusId { get; set; }
        public string StatusName { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

