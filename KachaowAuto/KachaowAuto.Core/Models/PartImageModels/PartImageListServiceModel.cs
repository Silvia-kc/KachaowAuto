using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.PartImageModels
{
    public class PartImageListServiceModel
    {
        public int PartImageId { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
