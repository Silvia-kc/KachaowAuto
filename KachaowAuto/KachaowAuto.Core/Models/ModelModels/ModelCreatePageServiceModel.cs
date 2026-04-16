using KachaowAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.ModelModels
{
    public class ModelCreatePageServiceModel
    {
        public ModelCreateServiceModel Model { get; set; } = new();
        public List<KachaowAuto.Data.Models.Brand> Brands { get; set; } = new();
        public List<KachaowAuto.Data.Models.EngineType> EngineTypes { get; set; } = new();
        public List<KachaowAuto.Data.Models.BodyType> BodyTypes { get; set; } = new();
    }
}
