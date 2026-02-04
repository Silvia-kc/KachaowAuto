using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public PartController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var parts = await context.Parts
                                    .Include(a => a.Category)
                                    .Include(a => a.AppointmentParts)
                                    .Include(a => a.Images)
                                    .ToListAsync();
            return View(parts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await context.PartCategories.ToListAsync();
            ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
            ViewBag.Images = await context.PartImages.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await context.PartCategories.ToListAsync();
                ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
                ViewBag.Images = await context.PartImages.ToListAsync();
                return View(part);
            }
            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await context.PartCategories.ToListAsync();
            ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
            ViewBag.Images = await context.PartImages.ToListAsync();
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await context.PartCategories.ToListAsync();
                ViewBag.AppointmentParts = await context.AppointmentParts.ToListAsync();
                ViewBag.Images = await context.PartImages.ToListAsync();
                return View(part);

            }
            context.Parts.Update(part);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);

            if (part == null)
            {
                return NotFound();
            }

            context.Parts.Remove(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
