using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class BrandController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public BrandController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: BrandController
        public async Task<IActionResult> Index()
        {
            var brands = await context.Brands
                                      .Include(a => a.Models)
                                      .ToListAsync();
            return View(brands);
        }

        // GET: BrandController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(brand);
            }
            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: BrandController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Models = await context.Models.ToListAsync();
            var brand = await context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(brand);

            }
            context.Brands.Update(brand);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: BrandController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);

            context.Brands.Remove(brand);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
