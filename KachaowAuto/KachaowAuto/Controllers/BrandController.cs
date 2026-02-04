using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public BrandController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var brands = await context.Brands
                                      .Include(a => a.Models)
                                      .ToListAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await context.Brands.FirstOrDefaultAsync(a => a.BrandId == id);

            if (brand == null)
            {
                return NotFound();
            }

            context.Brands.Remove(brand);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
