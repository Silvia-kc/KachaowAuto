using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.ModelModels
{
    public class ModelDetailsServiceModel
    {
        public int ModelId { get; set; }
        public string BrandName { get; set; } = null!;
        public string ModelName { get; set; } = null!;
        public string EngineTypeName { get; set; } = null!;
        public decimal EngineVolume { get; set; }
        public int HorsePower { get; set; }
        public string BodyTypeName { get; set; } = null!;
    }
}
