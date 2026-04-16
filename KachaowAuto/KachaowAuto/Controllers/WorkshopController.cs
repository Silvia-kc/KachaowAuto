using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.WorkshopModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{

    public class WorkshopController : Controller
    {
        private readonly KachaowAutoDbContext context;
        private readonly IWorkshopService workshopService;

        public WorkshopController(KachaowAutoDbContext _context, IWorkshopService _workshopService)
        {
            context = _context;
            workshopService = _workshopService;
        }

        [Authorize(Roles = "Admin,Mechanic,Client")]
        public async Task<IActionResult> Index(string? city)
        {
            var workshopsQuery = context.Workshops
                .Include(w => w.Region)
                .Where(w => w.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(city))
            {
                workshopsQuery = workshopsQuery
                    .Where(w => w.City.ToLower() == city.ToLower());
            }

            var workshops = await workshopsQuery
                .Select(w => new WorkshopMapItemViewModel
                {
                    WorkshopId = w.WorkshopId,
                    Name = w.Name,
                    City = w.City,
                    Address = w.Address,
                    PhoneNumber = w.PhoneNumber,
                    Latitude = (decimal?)(w.Latitude.HasValue ? (double?)w.Latitude.Value : null),
                    Longitude = (decimal?)(w.Longitude.HasValue ? (double?)w.Longitude.Value : null)
                })
                .ToListAsync();

            var cities = await context.Workshops
                .Where(w => w.IsActive)
                .Select(w => w.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            ViewBag.Cities = cities;

            var model = new WorkshopMapViewModel
            {
                SelectedCity = city,
                Workshops = workshops
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var serviceModels = await workshopService.GetAllAsync();

            var viewModels = serviceModels.Select(w => new WorkshopListViewModel
            {
                WorkshopId = w.WorkshopId,
                Name = w.Name,
                RegionName = w.RegionName,
                City = w.City,
                Address = w.Address,
                PhoneNumber = w.PhoneNumber,
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                IsActive = w.IsActive
            }).ToList();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var serviceModel = await workshopService.GetCreatePageModelAsync();

            var viewModel = new WorkshopCreateViewModel
            {
                Regions = serviceModel.Regions.Select(r => new WorkshopRegionOptionViewModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(WorkshopCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await workshopService.GetCreatePageModelAsync();

                viewModel.Regions = reload.Regions.Select(r => new WorkshopRegionOptionViewModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new WorkshopCreateServiceModel
            {
                Name = viewModel.Name,
                RegionId = viewModel.RegionId,
                City = viewModel.City,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                IsActive = viewModel.IsActive
            };

            await workshopService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(AdminIndex));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await workshopService.GetEditPageModelAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new WorkshopEditViewModel
            {
                WorkshopId = serviceModel.Workshop.WorkshopId,
                Name = serviceModel.Workshop.Name,
                RegionId = serviceModel.Workshop.RegionId,
                City = serviceModel.Workshop.City,
                Address = serviceModel.Workshop.Address,
                PhoneNumber = serviceModel.Workshop.PhoneNumber,
                Latitude = serviceModel.Workshop.Latitude,
                Longitude = serviceModel.Workshop.Longitude,
                IsActive = serviceModel.Workshop.IsActive,
                Regions = serviceModel.Regions.Select(r => new WorkshopRegionOptionViewModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(WorkshopEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await workshopService.GetCreatePageModelAsync();

                viewModel.Regions = reload.Regions.Select(r => new WorkshopRegionOptionViewModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new WorkshopEditServiceModel
            {
                WorkshopId = viewModel.WorkshopId,
                Name = viewModel.Name,
                RegionId = viewModel.RegionId,
                City = viewModel.City,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                IsActive = viewModel.IsActive
            };

            await workshopService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(AdminIndex));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await workshopService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new WorkshopDetailsViewModel
            {
                WorkshopId = serviceModel.WorkshopId,
                Name = serviceModel.Name,
                RegionName = serviceModel.RegionName,
                City = serviceModel.City,
                Address = serviceModel.Address,
                PhoneNumber = serviceModel.PhoneNumber,
                Latitude = serviceModel.Latitude,
                Longitude = serviceModel.Longitude,
                IsActive = serviceModel.IsActive
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await workshopService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(AdminIndex));
        }
    }
}