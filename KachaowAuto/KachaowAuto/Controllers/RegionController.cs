using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class RegionController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public RegionController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: RegionController
        public async Task<IActionResult> Index()
        {
            var regions = await context.Regions
                                      .Include(a => a.Workshops)
                                      .ToListAsync();
            return View(regions);
        }

        // GET: RegionController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Workshops = await context.Workshops.ToListAsync();
            return View();
        }

        // POST: RegionController/Create
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

        // GET: RegionController/Edit/5
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

        // POST: RegionController/Edit/5
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

        // GET: RegionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(a => a.RegionId == id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: RegionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(a => a.RegionId == id);

            context.Regions.Remove(region);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
