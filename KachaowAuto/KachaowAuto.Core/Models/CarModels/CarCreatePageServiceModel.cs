using KachaowAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.CarModels
{
    public class CarCreatePageServiceModel
    {
        public CarCreateServiceModel Car { get; set; } = new();

        public List<KachaowAuto.Data.Models.Model> Models { get; set; } = new();
    }
}
