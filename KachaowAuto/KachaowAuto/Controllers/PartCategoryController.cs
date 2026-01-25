using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KachaowAuto.Controllers
{
    public class PartCategoryController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public PartCategoryController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: PartCategoryController
        public async Task<IActionResult> Index()
        {
            var partCategories = await context.PartCategories
                                    .Include(a => a.Parts)
                                    .ToListAsync();
            return View(partCategories);
        }

        // GET: PartCategoryController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Parts = await context.Parts.ToListAsync();
            return View();
        }

        // POST: PartCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartCategory partCategory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Parts = await context.Parts.ToListAsync();
                return View(partCategory);
            }
            await context.PartCategories.AddAsync(partCategory);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: PartCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Parts = await context.Parts.ToListAsync();
            var partCategory = await context.PartCategories.FirstOrDefaultAsync(a => a.PartCategoryId == id);
            if (partCategory == null)
            {
                return NotFound();
            }
            return View(partCategory);
        }

        // POST: PartCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartCategory partCategory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Parts = await context.Parts.ToListAsync();
                return View(partCategory);

            }
            context.PartCategories.Update(partCategory);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: PartCategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var partCategory = await context.PartCategories.FirstOrDefaultAsync(a => a.PartCategoryId == id);
            if (partCategory == null)
            {
                return NotFound();
            }
            return View(partCategory);
        }

        // POST: PartCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partCategory = await context.PartCategories.FirstOrDefaultAsync(a => a.PartCategoryId == id);

            context.PartCategories.Remove(partCategory);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
