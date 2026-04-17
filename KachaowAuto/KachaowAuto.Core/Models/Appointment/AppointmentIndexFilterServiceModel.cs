using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{

    public class AppointmentIndexFilterServiceModel
    {
        public int? StatusId { get; set; }
        public int? ServiceId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? SearchTerm { get; set; }
    }
}
