using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class PartImage
    {
            public int PartImageId { get; set; }

            public int PartId { get; set; }
            public Part Part { get; set; } = null!;

            public string ImageUrl { get; set; } = null!; 
    }
}
