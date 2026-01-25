using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class ServiceCategory
    {
        public int ServiceCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}

