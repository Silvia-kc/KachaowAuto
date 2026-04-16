using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.CarModels
{
    public class CarListServiceModel
    {
        public int CarId { get; set; }
        public string ModelName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public int Year { get; set; }
        public string VIN { get; set; } = null!;
        public int AppointmentsCount { get; set; }
    }
}
