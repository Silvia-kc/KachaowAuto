using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartImageModels
{
    public class PartImageCreateServiceModel
    {
        public int PartId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
