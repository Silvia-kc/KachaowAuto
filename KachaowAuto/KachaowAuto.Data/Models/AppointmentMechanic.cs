using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[PrimaryKey(nameof(AppointmentId), nameof(MechanicId))]
	public class AppointmentMechanic
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public int MechanicId { get; set; }
        public ApplicationUser Mechanic { get; set; } = null!;
    }
}
