using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public ServiceController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var services = await context.Services
                                      .Include(a => a.ServiceCategory)
                                      .Include(a => a.WorkshopServices)
                                      .Include(a => a.Appointments)
                                      .ToListAsync();
            return View(services);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ServiceCategories = await context.ServiceCategories.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ServiceCategories = await context.ServiceCategories.ToListAsync();
                ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(service);
            }
            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ServiceCategories = await context.ServiceCategories.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            var service = await context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service service)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ServiceCategories = await context.ServiceCategories.ToListAsync();
                ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(service);

            }
            context.Services.Update(service);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);

            if (service == null)
            {
                return NotFound();
            }

            context.Services.Remove(service);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
