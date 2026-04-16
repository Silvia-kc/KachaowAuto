using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.RegionModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.Region;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegionController : Controller
    {
        private readonly IRegionService regionService;

        public RegionController(IRegionService _regionService)
        {
            regionService = _regionService;
        }

        [Authorize(Roles = "Admin,Mechanic,Client")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await regionService.GetAllAsync();

            var viewModels = serviceModels.Select(r => new RegionListViewModel
            {
                RegionId = r.RegionId,
                RegionName = r.RegionName,
                WorkshopsCount = r.WorkshopsCount
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            var viewModel = new RegionCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new RegionCreateServiceModel
            {
                RegionName = viewModel.RegionName
            };

            await regionService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await regionService.GetEditByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new RegionEditViewModel
            {
                RegionId = serviceModel.RegionId,
                RegionName = serviceModel.RegionName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegionEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new RegionEditServiceModel
            {
                RegionId = viewModel.RegionId,
                RegionName = viewModel.RegionName
            };

            await regionService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await regionService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new RegionDetailsViewModel
            {
                RegionId = serviceModel.RegionId,
                RegionName = serviceModel.RegionName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await regionService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
