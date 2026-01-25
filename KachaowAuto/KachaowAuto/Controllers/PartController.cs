using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class PartController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public PartController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: PartController
        public async Task<IActionResult> Index()
        {
            var parts = await context.Parts
                                    .Include(a => a.Category)
                                    .Include(a => a.AppointmentParts)
                                    .Include(a => a.Images)
                                    .ToListAsync();
            return View(parts);
        }

        // GET: PartController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await context.Brands.ToListAsync();
            ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
            ViewBag.Images = await context.PartImages.ToListAsync();
            return View();
        }


        // POST: PartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await context.Brands.ToListAsync();
                ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
                ViewBag.Images = await context.PartImages.ToListAsync();
                return View(part);
            }
            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: PartController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Brands = await context.Brands.ToListAsync();
            ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
            ViewBag.Images = await context.PartImages.ToListAsync();
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        // POST: PartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await context.Brands.ToListAsync();
                ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
                ViewBag.Images = await context.PartImages.ToListAsync();
                return View(part);

            }
            context.Parts.Update(part);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: PartController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        // POST: PartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);

            context.Parts.Remove(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
