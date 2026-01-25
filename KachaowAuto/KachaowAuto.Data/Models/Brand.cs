using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[Index(nameof(BrandName), IsUnique = true)]
	public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public ICollection<Model> Models { get; set; } = new List<Model>();
    }
}
