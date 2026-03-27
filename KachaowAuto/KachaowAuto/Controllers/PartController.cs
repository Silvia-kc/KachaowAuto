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
                .Include(p => p.Images)
                .OrderBy(p => p.PartName)
                .ToListAsync();

            return View(parts);
        }
        public async Task<IActionResult> Details(int id)
        {
            var part = await context.Parts
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.PartId == id);

            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }
        public async Task<IActionResult> Create(int? partId)
        {
            ViewBag.Parts = await context.Parts
                .OrderBy(p => p.PartName)
                .Select(p => new SelectListItem
                {
                    Value = p.PartId.ToString(),
                    Text = p.PartName
                })
                .ToListAsync();

            var model = new PartImage();

            if (partId.HasValue)
            {
                model.PartId = partId.Value;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartImage model)
        {
            if (model.PartId <= 0)
            {
                ModelState.AddModelError("PartId", "Select a part.");
            }

            if (string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                ModelState.AddModelError("ImageUrl", "Image URL is required.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Parts = await context.Parts
                    .OrderBy(p => p.PartName)
                    .Select(p => new SelectListItem
                    {
                        Value = p.PartId.ToString(),
                        Text = p.PartName
                    })
                    .ToListAsync();

                return View(model);
            }

            var partExists = await context.Parts.AnyAsync(p => p.PartId == model.PartId);
            if (!partExists)
            {
                ModelState.AddModelError("PartId", "Selected part does not exist.");

                ViewBag.Parts = await context.Parts
                    .OrderBy(p => p.PartName)
                    .Select(p => new SelectListItem
                    {
                        Value = p.PartId.ToString(),
                        Text = p.PartName
                    })
                    .ToListAsync();

                return View(model);
            }

            await context.PartImages.AddAsync(new PartImage
            {
                PartId = model.PartId,
                ImageUrl = model.ImageUrl.Trim()
            });

            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Part", new { id = model.PartId });
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
    var part = await context.Parts
        .Include(p => p.Category)
        .Include(p => p.Images)
        .FirstOrDefaultAsync(p => p.PartId == id);

    if (part == null)
    {
        return NotFound();
    }

    return View(part);
}

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await context.Parts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.PartId == id);

            if (part == null)
            {
                return NotFound();
            }

            if (part.Images != null && part.Images.Any())
            {
                context.PartImages.RemoveRange(part.Images);
            }

            context.Parts.Remove(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
