using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentListServiceModel
    {
        public int AppointmentId { get; set; }

        public string CarModelName { get; set; } = null!;
        public string VIN { get; set; } = null!;

        public string WorkshopName { get; set; } = null!;
        public string ServiceName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public int AppointmentStatusId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public string? ProblemDescription { get; set; }
    }
}
