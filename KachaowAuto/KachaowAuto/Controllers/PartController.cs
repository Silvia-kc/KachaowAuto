using KachaowAuto.Core.Interfaces;
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
    [Authorize]
    public class PartController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly ICloudinaryService _cloudinaryService;

        public PartController(KachaowAutoDbContext _context, ICloudinaryService cloudinaryService)
        {
            context = _context;
            _cloudinaryService = cloudinaryService;
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

        [Authorize(Roles = "Admin,Mechanic")]
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
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Create(int partId, IFormFile imageFile)
        {
            if (partId <= 0)
            {
                ModelState.AddModelError("PartId", "Избери част.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("imageFile", "Моля, избери снимка.");
            }

            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);
            if (part == null)
            {
                ModelState.AddModelError("PartId", "Избраната част не съществува.");
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

                var model = new PartImage
                {
                    PartId = partId
                };

                return View(model);
            }

            var uploadResult = await _cloudinaryService.UploadImageAsync(imageFile, "kachaowauto/parts");

            if (uploadResult == null || string.IsNullOrWhiteSpace(uploadResult.Url))
            {
                ModelState.AddModelError("", "Грешка при качване на снимката.");

                ViewBag.Parts = await context.Parts
                    .OrderBy(p => p.PartName)
                    .Select(p => new SelectListItem
                    {
                        Value = p.PartId.ToString(),
                        Text = p.PartName
                    })
                    .ToListAsync();

                var model = new PartImage
                {
                    PartId = partId
                };

                return View(model);
            }

            var partImage = new PartImage
            {
                PartId = partId,
                ImageUrl = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            await context.PartImages.AddAsync(partImage);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = partId });
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await context.PartCategories.ToListAsync();

            var part = await context.Parts.FirstOrDefaultAsync(a => a.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Edit(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await context.PartCategories.ToListAsync();
                return View(part);
            }

            context.Parts.Update(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Mechanic")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
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
                foreach (var image in part.Images)
                {
                    if (!string.IsNullOrWhiteSpace(image.PublicId))
                    {
                        await _cloudinaryService.DeleteImageAsync(image.PublicId);
                    }
                }

                context.PartImages.RemoveRange(part.Images);
            }

            context.Parts.Remove(part);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await context.PartImages.FirstOrDefaultAsync(i => i.PartImageId == id);

            if (image == null)
            {
                return NotFound();
            }

            var partId = image.PartId;

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await _cloudinaryService.DeleteImageAsync(image.PublicId);
            }

            context.PartImages.Remove(image);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = partId });
        }
    }
}
