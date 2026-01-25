using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[PrimaryKey(nameof(AppointmentId), nameof(PartId))]
	public class AppointmentPart
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public int PartId { get; set; }
        public Part Part { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPriceAtTime { get; set; }
        public string? Notes { get; set; }
    }
}
