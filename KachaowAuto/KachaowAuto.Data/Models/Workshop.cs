using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[Index(nameof(RegionId), nameof(City))]
	public class Workshop
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; } = null!;

        public int RegionId { get; set; }
        public Region Region { get; set; } = null!;

        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<WorkshopService> WorkshopServices { get; set; } = new List<WorkshopService>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

