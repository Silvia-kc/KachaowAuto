using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using KachaowAuto.ViewModels.Part;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class PartController : Controller
    {
        private readonly IPartService partService;

        public PartController(IPartService _partService)
        {
            partService = _partService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModels = await partService.GetAllAsync();

            var viewModels = serviceModels.Select(p => new PartListViewModel
            {
                PartId = p.PartId,
                PartName = p.PartName,
                Manufacturer = p.Manufacturer,
                PartNumber = p.PartNumber,
                CategoryName = p.CategoryName,
                UnitPrice = p.UnitPrice,
                IsActive = p.IsActive,
                FirstImageUrl = p.FirstImageUrl
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var serviceModel = await partService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartDetailsViewModel
            {
                PartId = serviceModel.PartId,
                PartName = serviceModel.PartName,
                Manufacturer = serviceModel.Manufacturer,
                PartNumber = serviceModel.PartNumber,
                Description = serviceModel.Description,
                UnitPrice = serviceModel.UnitPrice,
                IsActive = serviceModel.IsActive,
                CategoryName = serviceModel.CategoryName,
                Images = serviceModel.Images.Select(i => new PartDetailsImageViewModel
                {
                    PartImageId = i.PartImageId,
                    ImageUrl = i.ImageUrl
                }).ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Create()
        {
            var serviceModel = await partService.GetCreatePageModelAsync();

            var viewModel = new PartCreateViewModel
            {
                Categories = serviceModel.Categories.Select(c => new PartCategoryOptionViewModel
                {
                    PartCategoryId = c.PartCategoryId,
                    Name = c.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Create(PartCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await partService.GetCreatePageModelAsync();

                viewModel.Categories = reload.Categories.Select(c => new PartCategoryOptionViewModel
                {
                    PartCategoryId = c.PartCategoryId,
                    Name = c.Name
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new PartCreateServiceModel
            {
                PartName = viewModel.PartName,
                Manufacturer = viewModel.Manufacturer,
                PartNumber = viewModel.PartNumber,
                Description = viewModel.Description,
                UnitPrice = viewModel.UnitPrice,
                IsActive = viewModel.IsActive,
                PartCategoryId = viewModel.PartCategoryId,
                ImageFile = viewModel.ImageFile
            };

            await partService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await partService.GetEditPageModelAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartEditViewModel
            {
                PartId = serviceModel.Part.PartId,
                PartName = serviceModel.Part.PartName,
                Manufacturer = serviceModel.Part.Manufacturer,
                PartNumber = serviceModel.Part.PartNumber,
                Description = serviceModel.Part.Description,
                UnitPrice = serviceModel.Part.UnitPrice,
                IsActive = serviceModel.Part.IsActive,
                PartCategoryId = serviceModel.Part.PartCategoryId,
                Categories = serviceModel.Categories.Select(c => new PartCategoryOptionViewModel
                {
                    PartCategoryId = c.PartCategoryId,
                    Name = c.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Edit(PartEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await partService.GetCreatePageModelAsync();

                viewModel.Categories = reload.Categories.Select(c => new PartCategoryOptionViewModel
                {
                    PartCategoryId = c.PartCategoryId,
                    Name = c.Name
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new PartEditServiceModel
            {
                PartId = viewModel.PartId,
                PartName = viewModel.PartName,
                Manufacturer = viewModel.Manufacturer,
                PartNumber = viewModel.PartNumber,
                Description = viewModel.Description,
                UnitPrice = viewModel.UnitPrice,
                IsActive = viewModel.IsActive,
                PartCategoryId = viewModel.PartCategoryId
            };

            await partService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await partService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartDetailsViewModel
            {
                PartId = serviceModel.PartId,
                PartName = serviceModel.PartName,
                Manufacturer = serviceModel.Manufacturer,
                PartNumber = serviceModel.PartNumber,
                Description = serviceModel.Description,
                UnitPrice = serviceModel.UnitPrice,
                IsActive = serviceModel.IsActive,
                CategoryName = serviceModel.CategoryName,
                Images = serviceModel.Images.Select(i => new PartDetailsImageViewModel
                {
                    PartImageId = i.PartImageId,
                    ImageUrl = i.ImageUrl
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await partService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var isDeleted = await partService.DeleteImageAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
