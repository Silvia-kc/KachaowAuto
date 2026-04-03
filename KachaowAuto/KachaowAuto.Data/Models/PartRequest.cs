using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class PartRequest
    {
        public int PartRequestId { get; set; }

        [Required]
        public int PartId { get; set; }
        public Part Part { get; set; } = null!;

        [Required]
        public int MechanicId { get; set; }
        public ApplicationUser Mechanic { get; set; } = null!;

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Pending";

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; }

        [StringLength(500)]
        public string? AdminNote { get; set; }
    }
}
