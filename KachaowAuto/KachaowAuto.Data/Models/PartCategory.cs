using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class PartCategory
    {
        public int PartCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
