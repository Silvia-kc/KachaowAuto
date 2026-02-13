using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModelController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public ModelController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var models = await context.Models
                                    .Include(a => a.Brand)
                                    .Include(a => a.EngineType)
                                    .Include(a => a.BodyType)
                                    .ToListAsync();
            return View(models);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await context.Brands.ToListAsync();
            ViewBag.EngineTypes = await context.EngineTypes.ToListAsync();
            ViewBag.BodyTypes = await context.BodyTypes.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model model)
        {
            ModelState.Remove(nameof(Model.Brand));
            ModelState.Remove(nameof(Model.EngineType));
            ModelState.Remove(nameof(Model.BodyType));
            ModelState.Remove(nameof(Model.Cars));

            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await context.Brands.ToListAsync();
                ViewBag.EngineTypes = await context.EngineTypes.ToListAsync();
                ViewBag.BodyTypes = await context.BodyTypes.ToListAsync();
                return View(model);
            }

            context.Models.Add(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Brands = await context.Brands.ToListAsync();
            ViewBag.EngineTypes = await context.EngineTypes.ToListAsync();
            ViewBag.BodyTypes = await context.BodyTypes.ToListAsync();
            ViewBag.Cars = await context.Cars.ToListAsync();
            var model = await context.Models.FirstOrDefaultAsync(a => a.ModelId == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await context.Brands.ToListAsync();
                ViewBag.EngineTypes = await context.EngineTypes.ToListAsync();
                ViewBag.BodyTypes = await context.BodyTypes.ToListAsync();
                ViewBag.Cars = await context.Cars.ToListAsync();
                return View(model);

            }
            context.Models.Update(model);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Models.FirstOrDefaultAsync(a => a.ModelId == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await context.Models.FirstOrDefaultAsync(a => a.ModelId == id);

            if (model == null)
            {
                return NotFound();
            }

            context.Models.Remove(model);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
