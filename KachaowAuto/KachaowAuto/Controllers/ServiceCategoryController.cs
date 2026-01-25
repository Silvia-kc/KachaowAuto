using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class ServiceCategoryController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public ServiceCategoryController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: ServiceCategoryVontroller
        public async Task<IActionResult> Index()
        {
            var serviceCategory = await context.ServiceCategories
                                        .Include(a => a.Services)
                                        .ToListAsync();
            return View(serviceCategory);
        }

        // GET: ServiceCategoryVontroller/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await context.Services.ToListAsync();
            return View();
        }

        // POST: ServiceCategoryVontroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCategory serviceCategory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = await context.Services.ToListAsync();
                return View(serviceCategory);
            }
            await context.ServiceCategories.AddAsync(serviceCategory);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ServiceCategoryVontroller/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Services = await context.Services.ToListAsync();
            var serviceCategory = await context.ServiceCategories.FirstOrDefaultAsync(a => a.ServiceCategoryId == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }


        // POST: ServiceCategoryVontroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceCategory serviceCategory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = await context.Services.ToListAsync();
                return View(serviceCategory);

            }
            context.ServiceCategories.Update(serviceCategory);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: ServiceCategoryVontroller/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var serviceCategory = await context.ServiceCategories.FirstOrDefaultAsync(a => a.ServiceCategoryId == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }

        // POST: ServiceCategoryVontroller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCategory = await context.ServiceCategories.FirstOrDefaultAsync(a => a.ServiceCategoryId == id);

            context.ServiceCategories.Remove(serviceCategory);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
