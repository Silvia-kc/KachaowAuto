using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EngineTypeController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public EngineTypeController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var engineTypes = await context.EngineTypes
                                    .Include(a => a.Models)
                                    .ToListAsync();
            return View(engineTypes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EngineType engineType)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(engineType);
            }
            await context.EngineTypes.AddAsync(engineType);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Models = await context.Models.ToListAsync();
            var engineType = await context.EngineTypes.FirstOrDefaultAsync(a => a.EngineTypeId == id);
            if (engineType == null)
            {
                return NotFound();
            }
            return View(engineType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EngineType engineType)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(engineType);

            }
            context.EngineTypes.Update(engineType);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var engineType = await context.EngineTypes.FirstOrDefaultAsync(a => a.EngineTypeId == id);
            if (engineType == null)
            {
                return NotFound();
            }
            return View(engineType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var engineType = await context.EngineTypes.FirstOrDefaultAsync(a => a.EngineTypeId == id);

            if (engineType == null)
            {
                return NotFound();
            }

            context.EngineTypes.Remove(engineType);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
