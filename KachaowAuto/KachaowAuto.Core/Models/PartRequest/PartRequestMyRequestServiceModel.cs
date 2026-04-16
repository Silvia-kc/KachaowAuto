using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartRequest
{
    public class PartRequestMyRequestServiceModel
    {
        public int PartRequestId { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public string? AdminNote { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
