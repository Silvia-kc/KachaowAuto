using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class PartImageController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public PartImageController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: PartImageController
        public async Task<IActionResult> Index()
        {
            var partImages = await context.PartImages
                                         .Include(a => a.Part)
                                         .ToListAsync();
            return View(partImages);
        }

        // GET: PartImageController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Parts = await context.Parts.ToListAsync();
            return View();
        }


        // POST: PartImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartImage partImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Parts = await context.Parts.ToListAsync();
                return View(partImage);
            }
            await context.PartImages.AddAsync(partImage);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: PartImageController/Edit/5
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

        // POST: PartImageController/Edit/5
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

        // GET: PartImageController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var partImage = await context.PartImages.FirstOrDefaultAsync(a => a.PartImageId == id);
            if (partImage == null)
            {
                return NotFound();
            }
            return View(partImage);
        }

        // POST: PartImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partImage = await context.PartImages.FirstOrDefaultAsync(a => a.PartImageId == id);

            context.PartImages.Remove(partImage);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
