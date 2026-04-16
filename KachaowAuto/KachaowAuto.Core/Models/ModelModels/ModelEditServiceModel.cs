using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.ModelModels
{
    public class ModelEditServiceModel
    {
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; } = null!;
        public int EngineTypeId { get; set; }
        public decimal EngineVolume { get; set; }
        public int HorsePower { get; set; }
        public int BodyTypeId { get; set; }
    }
}
