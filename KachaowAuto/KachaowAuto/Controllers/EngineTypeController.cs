using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class EngineTypeController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public EngineTypeController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: EngineTypeController
        public async Task<IActionResult> Index()
        {
            var engineTypes = await context.EngineTypes
                                    .Include(a => a.Models)
                                    .ToListAsync();
            return View(engineTypes);
        }

        // GET: EngineTypeController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

        // POST: EngineTypeController/Create
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

        // GET: EngineTypeController/Edit/5
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

        // POST: EngineTypeController/Edit/5
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


        // GET: EngineTypeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var engineType = await context.EngineTypes.FirstOrDefaultAsync(a => a.EngineTypeId == id);
            if (engineType == null)
            {
                return NotFound();
            }
            return View(engineType);
        }

        // POST: EngineTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var engineType = await context.EngineTypes.FirstOrDefaultAsync(a => a.EngineTypeId == id);

            context.EngineTypes.Remove(engineType);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
