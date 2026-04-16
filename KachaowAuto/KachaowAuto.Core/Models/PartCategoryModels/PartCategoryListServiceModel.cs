using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartCategoryModels
{
    public class PartCategoryListServiceModel
    {
        public int PartCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int PartsCount { get; set; }
    }
}
