using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<AppointmentMechanic> AppointmentMechanics { get; set; } = new List<AppointmentMechanic>();
    }
}
