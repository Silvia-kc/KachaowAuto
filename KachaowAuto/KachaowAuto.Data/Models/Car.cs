using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
	[Index(nameof(VIN), IsUnique = true)]
	public class Car
    {
        public int CarId { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int ModelId { get; set; }
        public Model Model { get; set; } = null!;

        public int Year { get; set; }
        public string VIN { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
