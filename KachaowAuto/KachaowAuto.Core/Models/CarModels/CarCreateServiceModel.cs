using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.CarModels
{
    public class CarCreateServiceModel
    {
        public int UserId { get; set; }
        public int ModelId { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; } = null!;
    }
}
