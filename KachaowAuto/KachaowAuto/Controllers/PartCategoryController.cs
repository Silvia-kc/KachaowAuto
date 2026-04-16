using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartCategoryModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.PartCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace KachaowAuto.Controllers
{
    public class PartCategoryController : Controller
    {
        private readonly IPartCategoryService partCategoryService;

        public PartCategoryController(IPartCategoryService _partCategoryService)
        {
            partCategoryService = _partCategoryService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await partCategoryService.GetAllAsync();

            var viewModels = serviceModels.Select(pc => new PartCategoryListViewModel
            {
                PartCategoryId = pc.PartCategoryId,
                Name = pc.Name,
                PartsCount = pc.PartsCount
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            var viewModel = new PartCategoryCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartCategoryCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new PartCategoryCreateServiceModel
            {
                Name = viewModel.Name
            };

            await partCategoryService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await partCategoryService.GetEditByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartCategoryEditViewModel
            {
                PartCategoryId = serviceModel.PartCategoryId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartCategoryEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new PartCategoryEditServiceModel
            {
                PartCategoryId = viewModel.PartCategoryId,
                Name = viewModel.Name
            };

            await partCategoryService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await partCategoryService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartCategoryDetailsViewModel
            {
                PartCategoryId = serviceModel.PartCategoryId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await partCategoryService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
