using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ServiceModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService _serviceService)
        {
            serviceService = _serviceService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await serviceService.GetAllAsync();

            var viewModels = serviceModels.Select(s => new ServiceListViewModel
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                Description = s.Description,
                PriceFrom = s.PriceFrom,
                PriceTo = s.PriceTo,
                CategoryName = s.CategoryName
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var serviceModel = await serviceService.GetCreatePageModelAsync();

            var viewModel = new ServiceCreateViewModel
            {
                Categories = serviceModel.Categories.Select(c => new ServiceCategoryOptionViewModel
                {
                    ServiceCategoryId = c.ServiceCategoryId,
                    CategoryName = c.CategoryName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await serviceService.GetCreatePageModelAsync();

                viewModel.Categories = reload.Categories.Select(c => new ServiceCategoryOptionViewModel
                {
                    ServiceCategoryId = c.ServiceCategoryId,
                    CategoryName = c.CategoryName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new ServiceCreateServiceModel
            {
                ServiceName = viewModel.ServiceName,
                Description = viewModel.Description,
                PriceFrom = viewModel.PriceFrom,
                PriceTo = viewModel.PriceTo,
                ServiceCategoryId = viewModel.ServiceCategoryId
            };

            await serviceService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await serviceService.GetEditPageModelAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceEditViewModel
            {
                ServiceId = serviceModel.Service.ServiceId,
                ServiceName = serviceModel.Service.ServiceName,
                Description = serviceModel.Service.Description,
                PriceFrom = serviceModel.Service.PriceFrom,
                PriceTo = serviceModel.Service.PriceTo,
                ServiceCategoryId = serviceModel.Service.ServiceCategoryId,
                Categories = serviceModel.Categories.Select(c => new ServiceCategoryOptionViewModel
                {
                    ServiceCategoryId = c.ServiceCategoryId,
                    CategoryName = c.CategoryName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await serviceService.GetCreatePageModelAsync();

                viewModel.Categories = reload.Categories.Select(c => new ServiceCategoryOptionViewModel
                {
                    ServiceCategoryId = c.ServiceCategoryId,
                    CategoryName = c.CategoryName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new ServiceEditServiceModel
            {
                ServiceId = viewModel.ServiceId,
                ServiceName = viewModel.ServiceName,
                Description = viewModel.Description,
                PriceFrom = viewModel.PriceFrom,
                PriceTo = viewModel.PriceTo,
                ServiceCategoryId = viewModel.ServiceCategoryId
            };

            await serviceService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await serviceService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceDetailsViewModel
            {
                ServiceId = serviceModel.ServiceId,
                ServiceName = serviceModel.ServiceName,
                Description = serviceModel.Description,
                PriceFrom = serviceModel.PriceFrom,
                PriceTo = serviceModel.PriceTo,
                CategoryName = serviceModel.CategoryName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await serviceService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
