using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartImageModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.PartImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin,Mechanic")]
    public class PartImageController : Controller
    {
        private readonly IPartImageService partImageService;

        public PartImageController(IPartImageService _partImageService)
        {
            partImageService = _partImageService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await partImageService.GetAllAsync();

            var viewModels = serviceModels.Select(pi => new PartImageListViewModel
            {
                PartImageId = pi.PartImageId,
                PartId = pi.PartId,
                PartName = pi.PartName,
                ImageUrl = pi.ImageUrl
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? partId)
        {
            var serviceModel = await partImageService.GetCreatePageModelAsync(partId);

            var viewModel = new PartImageCreateViewModel
            {
                PartId = serviceModel.Image.PartId,
                Parts = serviceModel.Parts.Select(p => new PartOptionViewModel
                {
                    PartId = p.PartId,
                    PartName = p.PartName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartImageCreateViewModel viewModel)
        {
            if (viewModel.PartId <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.PartId), "Избери част.");
            }

            if (viewModel.ImageFile == null || viewModel.ImageFile.Length == 0)
            {
                ModelState.AddModelError(nameof(viewModel.ImageFile), "Моля, избери снимка.");
            }

            if (!ModelState.IsValid)
            {
                var reload = await partImageService.GetCreatePageModelAsync(viewModel.PartId);

                viewModel.Parts = reload.Parts.Select(p => new PartOptionViewModel
                {
                    PartId = p.PartId,
                    PartName = p.PartName
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new PartImageCreateServiceModel
            {
                PartId = viewModel.PartId,
                ImageFile = viewModel.ImageFile
            };

            var partId = await partImageService.CreateAsync(serviceModel);

            if (partId == null)
            {
                ModelState.AddModelError("", "Грешка при качване на снимката.");

                var reload = await partImageService.GetCreatePageModelAsync(viewModel.PartId);

                viewModel.Parts = reload.Parts.Select(p => new PartOptionViewModel
                {
                    PartId = p.PartId,
                    PartName = p.PartName
                }).ToList();

                return View(viewModel);
            }

            return RedirectToAction("Details", "Part", new { id = partId.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var partId = await partImageService.DeleteAsync(id);

            if (partId == null)
            {
                return RedirectToAction("Index", "Part");
            }

            return RedirectToAction("Details", "Part", new { id = partId.Value });
        }
    }
}

