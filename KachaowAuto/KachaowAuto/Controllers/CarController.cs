using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    public class CarController : Controller
    {
        private readonly KachaowAutoDbContext context;
        public CarController(KachaowAutoDbContext _context)
        {
            context = _context;
        }
        // GET: CarController
        public async Task<IActionResult> Index()
        {
            var cars = await context.Cars
                                    .Include(a => a.Model)
                                    .Include(a => a.Appointments)
                                    .ToListAsync();
            return View(cars);
        }

        // GET: CarController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Models = await context.Models.ToListAsync();
            ViewBag.Appointments = await context.Appointments.ToListAsync();
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: CarController/Edit/5
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

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: CarController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(a => a.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(a => a.CarId == id);

            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
