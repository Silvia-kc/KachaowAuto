using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public AppointmentController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: AppointmentController
        public async Task<IActionResult> Index()
        {
            var appointments = await context.Appointments
                                            .Include(a => a.Car)
                                            .Include(a => a.Workshop)
                                            .Include(a => a.Service)
                                            .Include(a => a.Status)
                                            .ToListAsync();
            return View(appointments);
        }
        // GET: AppointmentController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Cars = await context.Cars.ToListAsync();
            ViewBag.Workshops = await context.Workshops.ToListAsync();
            ViewBag.Services = await context.Services.ToListAsync();
            ViewBag.Statuses = await context.AppointmentStatuses.ToListAsync();
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cars = await context.Cars.ToListAsync();
                ViewBag.Workshops = await context.Workshops.ToListAsync();
                ViewBag.Services = await context.Services.ToListAsync();
                ViewBag.Statuses = await context.AppointmentStatuses.ToListAsync();
                return View(appointment);
            }    
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: AppointmentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Cars = await context.Cars.ToListAsync();
            ViewBag.Workshops = await context.Workshops.ToListAsync();
            ViewBag.Services = await context.Services.ToListAsync();
            ViewBag.Statuses = await context.AppointmentStatuses.ToListAsync();
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            if(appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: AppointmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);

            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
