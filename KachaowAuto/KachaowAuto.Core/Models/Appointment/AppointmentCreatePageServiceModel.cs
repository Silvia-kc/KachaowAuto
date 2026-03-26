using KachaowAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Models.Appointment
{
    public class AppointmentCreatePageServiceModel
    {
        public List<Brand> Brands { get; set; } = new();
        public List<Model> Models { get; set; } = new();
        public List<KachaowAuto.Data.Models.Service> Services { get; set; } = new();
        public List<Workshop> Workshops { get; set; } = new();
        public List<EngineType> EngineTypes { get; set; } = new();
        public List<BodyType> BodyTypes { get; set; } = new();

        public int BrandsCount { get; set; }
        public int ModelsCount { get; set; }
        public int ServicesCount { get; set; }
        public int WorkshopsCount { get; set; }
    }
}
