using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public string? ProblemDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ScheduledDate { get; set; }
        public DateTime? CompletedAt { get; set; }

        public int AppointmentStatusId { get; set; }
        public AppointmentStatus Status { get; set; } = null!;

        public ICollection<AppointmentMechanic> AppointmentMechanics { get; set; } = new List<AppointmentMechanic>();
        public ICollection<AppointmentPart> AppointmentParts { get; set; } = new List<AppointmentPart>();
    }
}
