using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartModels
{
    public class PartEditServiceModel
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public int? PartCategoryId { get; set; }
    }
}
