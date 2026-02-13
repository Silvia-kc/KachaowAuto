using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(KachaowAutoDbContext _context, UserManager<ApplicationUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index(int? statusId)
        {
            ViewBag.Statuses = await context.AppointmentStatuses.ToListAsync();
            ViewBag.SelectedStatusId = statusId;

            var query = context.Appointments
                .Include(a => a.Car).ThenInclude(c => c.Model)
                .Include(a => a.Workshop)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .AsQueryable();

            if (statusId.HasValue)
                query = query.Where(a => a.AppointmentStatusId == statusId.Value); 

            var appointments = await query.ToListAsync();
            return View(appointments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await context.Appointments
                .Include(a => a.Car)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await context.Brands.ToListAsync();
            ViewBag.Services = await context.Services.ToListAsync();
            ViewBag.Workshops = await context.Workshops.Include(w => w.Region).ToListAsync();

            return View(new BookAppointmentViewModel
            {
                Year = DateTime.Now.Year,
                ScheduledDate = DateTime.Now.AddDays(1)
            });
        }
        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.Include(m => m.Brand).ToListAsync();
                ViewBag.Services = await context.Services.ToListAsync();
                ViewBag.Workshops = await context.Workshops.Include(w => w.Region).ToListAsync();
                return View(model);
            }

            var userIdStr = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
                return Forbid();

            var userId = int.Parse(userIdStr);

            var car = new Car
            {
                UserId = userId,
                ModelId = model.ModelId,
                Year = model.Year,
                VIN = model.VIN
            };

            context.Cars.Add(car);

            var pending = await context.AppointmentStatuses.FirstOrDefaultAsync(s => s.StatusName == "Pending");
            var statusId = pending != null
                ? pending.AppointmentStatusId
                : await context.AppointmentStatuses.Select(s => s.AppointmentStatusId).FirstAsync();

            var appointment = new Appointment
            {
                Car = car, 
                ServiceId = model.ServiceId,
                WorkshopId = model.WorkshopId,
                AppointmentStatusId = statusId,
                CreatedAt = DateTime.UtcNow,
                ScheduledDate = model.ScheduledDate,
                ProblemDescription = model.ProblemDescription
            };

            context.Appointments.Add(appointment);

            await context.SaveChangesAsync();

            return RedirectToAction("Client", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                    ViewBag.Cars = await context.Cars.ToListAsync();
                    ViewBag.Workshops = await context.Workshops.ToListAsync();
                    ViewBag.Services = await context.Services.ToListAsync();
                    ViewBag.Statuses = await context.AppointmentStatuses.ToListAsync();
                    return View(appointment);
                
            }
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Mechanic")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, int statusId)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (appointment == null) return NotFound();

            appointment.AppointmentStatusId = statusId;

            var status = await context.AppointmentStatuses.FirstOrDefaultAsync(s => s.AppointmentStatusId == statusId);
            if (status != null && status.StatusName == "Completed")
                appointment.CompletedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> GetModelsByBrand(int brandId)
        {
            var models = await context.Models
                .Where(m => m.BrandId == brandId)
                .Select(m => new { id = m.ModelId, name = m.ModelName })
                .OrderBy(m => m.name)
                .ToListAsync();

            return Json(models);
        }
    }
}
