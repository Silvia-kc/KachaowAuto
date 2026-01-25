using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; } = null!;

        public ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
    }
}
