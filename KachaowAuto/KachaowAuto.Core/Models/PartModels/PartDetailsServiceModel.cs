using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartModels
{
    public class PartDetailsServiceModel
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string? Manufacturer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public string? CategoryName { get; set; }
        public List<PartDetailsImageServiceModel> Images { get; set; } = new();
    }
}
