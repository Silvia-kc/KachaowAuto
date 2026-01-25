using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; } = null!;

        public ICollection<WorkshopService> WorkshopServices { get; set; } = new List<WorkshopService>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
