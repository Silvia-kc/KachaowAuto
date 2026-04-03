using KachaowAuto.Core.Interfaces;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin,Mechanic")]
    public class PartImageController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly ICloudinaryService _cloudinaryService;

        public PartImageController(KachaowAutoDbContext _context, ICloudinaryService cloudinaryService)
        {
            context = _context;
            _cloudinaryService = cloudinaryService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var partImages = await context.PartImages
                .Include(pi => pi.Part)
                .OrderByDescending(pi => pi.PartImageId)
                .ToListAsync();

            return View(partImages);
        }

        [HttpGet]
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

            var partExists = await context.Parts.AnyAsync(p => p.PartId == partId);
            if (!partExists)
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

            var image = new PartImage
            {
                PartId = partId,
                ImageUrl = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            await context.PartImages.AddAsync(image);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Part", new { id = partId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await context.PartImages
                .FirstOrDefaultAsync(i => i.PartImageId == id);

            if (image == null)
            {
                return RedirectToAction("Index", "Part");
            }

            var partId = image.PartId;

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await _cloudinaryService.DeleteImageAsync(image.PublicId);
            }

            context.PartImages.Remove(image);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Part", new { id = partId });
        }
    }
}

