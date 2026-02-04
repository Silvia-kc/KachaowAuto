using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegionController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public RegionController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic,Client")]
        public async Task<IActionResult> Index()
        {
            var regions = await context.Regions
                                      .Include(a => a.Workshops)
                                      .ToListAsync();
            return View(regions);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Workshops = await context.Workshops.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Workshops = await context.Workshops.ToListAsync();
                return View(region);
            }
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Workshops = await context.Workshops.ToListAsync();
            var region = await context.Regions.FirstOrDefaultAsync(a => a.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Region region)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Workshops = await context.Workshops.ToListAsync();
                return View(region);

            }
            context.Regions.Update(region);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(a => a.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(a => a.RegionId == id);

            if (region == null)
            {
                return NotFound();
            }

            context.Regions.Remove(region);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
