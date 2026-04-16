using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.ServiceCategoryModels
{
    public class ServiceCategoryEditServiceModel
    {
        public int ServiceCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
