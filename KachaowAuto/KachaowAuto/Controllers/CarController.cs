using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public CarController(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var cars = await context.Cars
                                    .Include(a => a.Model)
                                    .Include(a => a.Appointments)
                                    .ToListAsync();
            return View(cars);
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(car);
            }
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Models = await context.Models.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            var car = await context.Cars.FirstOrDefaultAsync(a => a.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Car car)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Models = await context.Models.ToListAsync();
                ViewBag.Appointments = await context.Appointments.ToListAsync();
                return View(car);

            }
            context.Cars.Update(car);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(a => a.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(a => a.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
