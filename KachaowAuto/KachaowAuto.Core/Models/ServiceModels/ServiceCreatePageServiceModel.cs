using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.ServiceModels
{
    public class ServiceCreatePageServiceModel
    {
        public ServiceCreateServiceModel Service { get; set; } = new();
        public List<ServiceCategoryOptionServiceModel> Categories { get; set; } = new();
    }
}
