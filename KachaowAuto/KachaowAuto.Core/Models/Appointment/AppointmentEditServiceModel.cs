using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentEditServiceModel
    {
        public int AppointmentId { get; set; }

        public int CarId { get; set; }
        public int WorkshopId { get; set; }
        public int ServiceId { get; set; }
        public int AppointmentStatusId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string? ProblemDescription { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
