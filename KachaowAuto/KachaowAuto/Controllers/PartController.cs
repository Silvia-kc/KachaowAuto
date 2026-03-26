using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index()
        {
            var parts = await context.Parts
                .Include(p => p.Category)
                .OrderBy(p => p.PartName)
                .ToListAsync();

            return View(parts);
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Create()
        {
            var model = new PartCreateViewModel
            {
                Categories = await context.PartCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.PartCategoryId.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await context.PartCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.PartCategoryId.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync();

                return View(model);
            }

            bool exists = await context.Parts.AnyAsync(p =>
                p.Manufacturer == model.Manufacturer &&
                p.PartNumber == model.PartNumber);

            if (exists)
            {
                ModelState.AddModelError("", "Part with the same manufacturer and part number already exists.");

                model.Categories = await context.PartCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.PartCategoryId.ToString(),
                        Text = c.Name
                    })
                    .ToListAsync();

                return View(model);
            }

            var part = new KachaowAuto.Data.Models.Part
            {
                PartName = model.PartName,
                Manufacturer = model.Manufacturer,
                PartNumber = model.PartNumber,
                Description = model.Description,
                UnitPrice = model.UnitPrice,
                IsActive = model.IsActive,
                PartCategoryId = model.PartCategoryId
            };

            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
