using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentCreateServiceModel
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }

        public int Year { get; set; }
        public string VIN { get; set; } = null!;

        public int ServiceId { get; set; }
        public int WorkshopId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string? ProblemDescription { get; set; }

        public int UserId { get; set; }
    }
}
