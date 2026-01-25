using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[PrimaryKey(nameof(WorkshopId), nameof(ServiceId))]
	public class WorkshopService
    {
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}
