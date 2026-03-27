using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartImageController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly IWebHostEnvironment env;
        public PartImageController(KachaowAutoDbContext _context, IWebHostEnvironment _env)
        {
            context = _context;
            env = _env;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var partImages = await context.PartImages
                                         .Include(a => a.Part)
                                         .ToListAsync();
            return View(partImages);
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

            await context.PartImages.AddAsync(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Part");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Parts = await context.Parts.ToListAsync();
            var partImage = await context.PartImages.FirstOrDefaultAsync(a => a.PartImageId == id);
            if (partImage == null)
            {
                return NotFound();
            }
            return View(partImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartImage partImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Parts = await context.Parts.ToListAsync();
                return View(partImage);

            }
            context.PartImages.Update(partImage);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
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

            int partId = image.PartId;

            context.PartImages.Remove(image);
            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Part", new { id = partId });
        }
    }
}

