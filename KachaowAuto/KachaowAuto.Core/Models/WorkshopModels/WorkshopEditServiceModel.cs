using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.WorkshopModels
{
    public class WorkshopEditServiceModel
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; } = null!;
        public int RegionId { get; set; }
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
