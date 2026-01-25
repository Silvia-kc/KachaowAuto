using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class BodyType
    {
        public int BodyTypeId { get; set; }
        public string Name { get; set; } = null!; 
        public ICollection<Model> Models { get; set; } = new List<Model>();
    }
}
