using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BodyTypeController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public BodyTypeController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var bodyTypes = await context.BodyTypes
                                         .Include(a => a.Models)
                                         .ToListAsync();
            return View(bodyTypes);
        }

        public async Task <IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BodyType bodyType)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(bodyType);
            }
            await context.BodyTypes.AddAsync(bodyType);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Models = await context.Models.ToListAsync();
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(a => a.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BodyType bodyType)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                return View(bodyType);

            }
            context.BodyTypes.Update(bodyType);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(a => a.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(a => a.BodyTypeId == id);

            if (bodyType == null)
            {
                return NotFound();
            }

            context.BodyTypes.Remove(bodyType);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
