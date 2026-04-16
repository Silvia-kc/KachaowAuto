using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.CarModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using KachaowAuto.ViewModels.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly UserManager<ApplicationUser> userManager;

        public CarController(ICarService _carService, UserManager<ApplicationUser> _userManager)
        {
            carService = _carService;
            userManager = _userManager;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await carService.GetAllAsync();

            var viewModels = serviceModels.Select(c => new CarListViewModel
            {
                CarId = c.CarId,
                BrandName = c.BrandName,
                ModelName = c.ModelName,
                Year = c.Year,
                VIN = c.VIN,
                AppointmentsCount = c.AppointmentsCount
            }).ToList();

            return View(viewModels);
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create()
        {
            var serviceModel = await carService.GetCreatePageModelAsync();

            var viewModel = new CarCreateViewModel
            {
                Models = serviceModel.Models.Select(m => new CarModelOptionViewModel
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    BrandName = m.Brand?.BrandName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create(CarCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await carService.GetCreatePageModelAsync();

                viewModel.Models = reload.Models.Select(m => new CarModelOptionViewModel
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    BrandName = m.Brand?.BrandName
                }).ToList();

                return View(viewModel);
            }

            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            var serviceModel = new CarCreateServiceModel
            {
                UserId = int.Parse(userIdStr),
                ModelId = viewModel.ModelId,
                Year = viewModel.Year,
                VIN = viewModel.VIN
            };

            await carService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(MyCars));
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MyCars()
        {
            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            int userId = int.Parse(userIdStr);
            var serviceModels = await carService.GetMyCarsAsync(userId);

            var viewModels = serviceModels.Select(c => new MyCarViewModel
            {
                CarId = c.CarId,
                BrandName = c.BrandName,
                ModelName = c.ModelName,
                Year = c.Year,
                VIN = c.VIN,
                LatestStatus = c.LatestStatus
            }).ToList();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await carService.GetEditPageModelAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new CarEditViewModel
            {
                CarId = serviceModel.Car.CarId,
                UserId = serviceModel.Car.UserId,
                ModelId = serviceModel.Car.ModelId,
                Year = serviceModel.Car.Year,
                VIN = serviceModel.Car.VIN,
                Models = serviceModel.Models.Select(m => new CarModelOptionViewModel
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    BrandName = m.Brand?.BrandName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CarEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await carService.GetCreatePageModelAsync();

                viewModel.Models = reload.Models.Select(m => new CarModelOptionViewModel
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    BrandName = m.Brand?.BrandName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new CarEditServiceModel
            {
                CarId = viewModel.CarId,
                UserId = viewModel.UserId,
                ModelId = viewModel.ModelId,
                Year = viewModel.Year,
                VIN = viewModel.VIN
            };

            await carService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await carService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new CarDetailsViewModel
            {
                CarId = serviceModel.CarId,
                BrandName = serviceModel.BrandName,
                ModelName = serviceModel.ModelName,
                Year = serviceModel.Year,
                VIN = serviceModel.VIN
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await carService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
