using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.EngineTypeModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.EngineType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Admin")]
    public class EngineTypeController : Controller
    {
        private readonly IEngineTypeService engineTypeService;

        public EngineTypeController(IEngineTypeService _engineTypeService)
        {
            engineTypeService = _engineTypeService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await engineTypeService.GetAllAsync();

            var viewModels = serviceModels.Select(e => new EngineTypeListViewModel
            {
                EngineTypeId = e.EngineTypeId,
                Name = e.Name,
                ModelsCount = e.ModelsCount
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            var viewModel = new EngineTypeCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EngineTypeCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new EngineTypeCreateServiceModel
            {
                Name = viewModel.Name
            };

            await engineTypeService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await engineTypeService.GetEditByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new EngineTypeEditViewModel
            {
                EngineTypeId = serviceModel.EngineTypeId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EngineTypeEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new EngineTypeEditServiceModel
            {
                EngineTypeId = viewModel.EngineTypeId,
                Name = viewModel.Name
            };

            await engineTypeService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await engineTypeService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new EngineTypeDetailsViewModel
            {
                EngineTypeId = serviceModel.EngineTypeId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await engineTypeService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
