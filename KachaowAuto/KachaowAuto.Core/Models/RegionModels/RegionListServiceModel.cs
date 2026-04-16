using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.RegionModels
{
    public class RegionListServiceModel
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; } = null!;
        public int WorkshopsCount { get; set; }
    }
}
