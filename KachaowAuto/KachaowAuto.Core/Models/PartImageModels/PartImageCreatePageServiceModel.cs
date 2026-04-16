using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartImageModels
{
    public class PartImageCreatePageServiceModel
    {
        public PartImageCreateServiceModel Image { get; set; } = new();
        public List<PartOptionServiceModel> Parts { get; set; } = new();
    }
}
