using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartRequest
{
    public class PartRequestCreateServiceModel
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Note { get; set; }
    }
}
