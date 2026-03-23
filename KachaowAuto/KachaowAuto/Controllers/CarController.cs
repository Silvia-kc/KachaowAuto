using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public CarController(KachaowAutoDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
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
            return RedirectToAction("MyCars");
        }
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MyCars()
        {
            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
                return Forbid();

            int userId = int.Parse(userIdStr);

            var cars = await context.Cars
                .Where(c => c.UserId == userId)
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Appointments)
                .Select(c => new MyCarViewModel
                {
                    CarId = c.CarId,
                    BrandName = c.Model.Brand.BrandName,
                    ModelName = c.Model.ModelName,
                    Year = c.Year,
                    VIN = c.VIN,
                    LatestStatus = c.Appointments
                                    .OrderByDescending(a => a.ScheduledDate)
                                    .Select(a =>
                                            a.CompletedAt != null ? "Completed" :
                                            a.ScheduledDate > DateTime.Now ? "Upcoming" :
                                    "In Progress")
                                    .FirstOrDefault() ?? "No appointments"
                })
                .ToListAsync();

            return View(cars);
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
