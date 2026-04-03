using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KachaowAuto.ViewModels.PartRequest
{
    public class PartRequestCreateViewModel 
    {
        public int PartId { get; set; }

        public string? PartName { get; set; } 

        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int Quantity { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}
