using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentDetailsServiceModel
    {
        public int AppointmentId { get; set; }

        public int CarId { get; set; }
        public string CarModelName { get; set; } = null!;
        public string VIN { get; set; } = null!;
        public int Year { get; set; }

        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; } = null!;

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;

        public int AppointmentStatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public DateTime ScheduledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public string? ProblemDescription { get; set; }
    }
}
