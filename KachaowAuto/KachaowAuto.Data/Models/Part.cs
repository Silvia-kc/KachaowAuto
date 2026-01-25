using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[Index(nameof(Manufacturer), nameof(PartNumber), IsUnique = true)]
	public class Part
    {
        public int PartId { get; set; }

        public string PartName { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;

        public int? PartCategoryId { get; set; }
        public PartCategory? Category { get; set; }

        public ICollection<PartImage> Images { get; set; } = new List<PartImage>();
        public ICollection<AppointmentPart> AppointmentParts { get; set; } = new List<AppointmentPart>();
    }
}
