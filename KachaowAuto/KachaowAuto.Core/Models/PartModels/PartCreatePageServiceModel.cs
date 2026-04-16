using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartModels
{
    public class PartCreatePageServiceModel
    {
        public PartCreateServiceModel Part { get; set; } = new();
        public List<PartCategoryOptionServiceModel> Categories { get; set; } = new();
    }
}
