using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ServiceCategoryModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.ServiceCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ServiceCategoryController : Controller
    {
        private readonly IServiceCategoryService serviceCategoryService;

        public ServiceCategoryController(IServiceCategoryService _serviceCategoryService)
        {
            serviceCategoryService = _serviceCategoryService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await serviceCategoryService.GetAllAsync();

            var viewModels = serviceModels.Select(sc => new ServiceCategoryListViewModel
            {
                ServiceCategoryId = sc.ServiceCategoryId,
                CategoryName = sc.CategoryName,
                ServicesCount = sc.ServicesCount
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            var viewModel = new ServiceCategoryCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCategoryCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new ServiceCategoryCreateServiceModel
            {
                CategoryName = viewModel.CategoryName
            };

            await serviceCategoryService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await serviceCategoryService.GetEditByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceCategoryEditViewModel
            {
                ServiceCategoryId = serviceModel.ServiceCategoryId,
                CategoryName = serviceModel.CategoryName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceCategoryEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new ServiceCategoryEditServiceModel
            {
                ServiceCategoryId = viewModel.ServiceCategoryId,
                CategoryName = viewModel.CategoryName
            };

            await serviceCategoryService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await serviceCategoryService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ServiceCategoryDetailsViewModel
            {
                ServiceCategoryId = serviceModel.ServiceCategoryId,
                CategoryName = serviceModel.CategoryName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await serviceCategoryService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
