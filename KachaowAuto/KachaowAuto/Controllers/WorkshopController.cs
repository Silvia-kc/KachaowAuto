using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
        
    public class WorkshopController : Controller
    {
        private readonly KachaowAutoDbContext context;

        public WorkshopController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic,Client")]
        public async Task<IActionResult> Index(string? city)
        {
            var workshopsQuery = context.Workshops
                .Include(w => w.Region)
                .Where(w => w.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
            {
                workshopsQuery = workshopsQuery
                    .Where(w => w.City.ToLower() == city.ToLower());
            }

            var workshops = await workshopsQuery
                                 .Select(w => new WorkshopMapItemViewModel
                                 {
                                   WorkshopId = w.WorkshopId,
                                   Name = w.Name,
                                   City = w.City,
                                   Address = w.Address,
                                   PhoneNumber = w.PhoneNumber,
                                   Latitude = (decimal?)(w.Latitude.HasValue ? (double?)w.Latitude.Value : null),
                                 Longitude = (decimal?)(w.Longitude.HasValue ? (double?)w.Longitude.Value : null)
                                 })
                                .ToListAsync();

            var cities = await context.Workshops
                .Where(w => w.IsActive)
                .Select(w => w.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            ViewBag.Cities = cities;

            var model = new WorkshopMapViewModel
            {
                SelectedCity = city,
                Workshops = workshops
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var workshops = await context.Workshops
                .Include(w => w.Region)
                .ToListAsync();

            return View(workshops);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Regions = await context.Regions.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Workshop workshop)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Regions = await context.Regions.ToListAsync();
                ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(workshop);
            }

            await context.Workshops.AddAsync(workshop);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Regions = await context.Regions.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();

            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);
            if (workshop == null) return NotFound();

            return View(workshop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Workshop workshop)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Regions = await context.Regions.ToListAsync();
                ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(workshop);
            }

            context.Workshops.Update(workshop);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);
            if (workshop == null) return NotFound();

            return View(workshop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);

            if (workshop == null)
            {
                return NotFound();
            }

            context.Workshops.Remove(workshop);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}