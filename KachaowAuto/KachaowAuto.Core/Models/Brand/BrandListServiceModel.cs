using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Brand
{
    public class BrandListServiceModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public int ModelsCount { get; set; }
    }
}
