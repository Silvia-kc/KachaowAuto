using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceCategoryController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public ServiceCategoryController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceCategory = await context.ServiceCategories
                                        .Include(a => a.Services)
                                        .ToListAsync();
            return View(serviceCategory);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await context.Services.ToListAsync();
            return View();
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var serviceCategory = await context.ServiceCategories.FirstOrDefaultAsync(a => a.ServiceCategoryId == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCategory = await context.ServiceCategories.FirstOrDefaultAsync(a => a.ServiceCategoryId == id);

            if (serviceCategory == null)
            {
                return NotFound();
            }

            context.ServiceCategories.Remove(serviceCategory);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
