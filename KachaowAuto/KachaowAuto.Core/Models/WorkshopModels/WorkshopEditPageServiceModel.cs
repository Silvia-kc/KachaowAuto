using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.WorkshopModels
{
    public class WorkshopEditPageServiceModel
    {
        public WorkshopEditServiceModel Workshop { get; set; } = new();
        public List<WorkshopRegionOptionServiceModel> Regions { get; set; } = new();
    }
}
