using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class BodyTypeController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public BodyTypeController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: BodyTypeController
        public async Task<IActionResult> Index()
        {
            var bodyTypes = await context.BodyTypes
                                         .Include(a => a.Models)
                                         .ToListAsync();
            return View(bodyTypes);
        }

        // GET: BodyTypeController/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            return View();
        }

        // POST: BodyTypeController/Create
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

        // GET: BodyTypeController/Edit/5
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

        // POST: BodyTypeController/Edit/5
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


        // GET: BodyTypeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(a => a.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }
        // POST: BodyTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bodyType = await context.BodyTypes.FirstOrDefaultAsync(a => a.BodyTypeId == id);

            context.BodyTypes.Remove(bodyType);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
