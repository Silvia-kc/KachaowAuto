using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
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
        // GET: WorkshopController
        public async Task<IActionResult> Index()
        {
            var workshops = await context.Workshops
                                               .Include(a => a.Region)
                                               .Include(a => a.WorkshopServices)
                                               .Include(a => a.Appointments)
                                               .ToListAsync();
            return View(workshops);
        }

        // GET: WorkshopController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Regions = await context.Regions.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            return View();
        }

        // POST: WorkshopController/Create
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

        // GET: WorkshopController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Regions = await context.Regions.ToListAsync();
            ViewBag.WorkshopServices = await context.WorkshopServices.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);
            if (workshop == null)
            {
                return NotFound();
            }
            return View(workshop);
        }

        // POST: WorkshopController/Edit/5
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

        // GET: WorkshopController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);
            if (workshop == null)
            {
                return NotFound();
            }
            return View(workshop);
        }

        // POST: WorkshopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workshop = await context.Workshops.FirstOrDefaultAsync(a => a.WorkshopId == id);

            context.Workshops.Remove(workshop);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
