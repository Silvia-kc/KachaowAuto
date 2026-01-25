using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[Index(nameof(BrandId), nameof(ModelName), nameof(EngineTypeId), nameof(EngineVolume), nameof(HorsePower), nameof(BodyTypeId), IsUnique = true)]
	public class Model
    {
        public int ModelId { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        public string ModelName { get; set; } = null!;

        public int EngineTypeId { get; set; }
        public EngineType EngineType { get; set; } = null!;

        public decimal EngineVolume { get; set; } 
        public int HorsePower { get; set; }

        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; } = null!;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
