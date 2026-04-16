using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ModelModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Admin")]
    public class ModelController : Controller
    {
        private readonly IModelService modelService;

        public ModelController(IModelService _modelService)
        {
            modelService = _modelService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await modelService.GetAllAsync();

            var viewModels = serviceModels.Select(m => new ModelListViewModel
            {
                ModelId = m.ModelId,
                BrandName = m.BrandName,
                ModelName = m.ModelName,
                EngineTypeName = m.EngineTypeName,
                EngineVolume = m.EngineVolume,
                HorsePower = m.HorsePower,
                BodyTypeName = m.BodyTypeName
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var serviceModel = await modelService.GetCreatePageModelAsync();

            var viewModel = new ModelCreateViewModel
            {
                Brands = serviceModel.Brands.Select(b => new ModelBrandOptionViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                }).ToList(),
                EngineTypes = serviceModel.EngineTypes.Select(e => new ModelEngineTypeOptionViewModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                }).ToList(),
                BodyTypes = serviceModel.BodyTypes.Select(b => new ModelBodyTypeOptionViewModel
                {
                    BodyTypeId = b.BodyTypeId,
                    Name = b.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await modelService.GetCreatePageModelAsync();

                viewModel.Brands = reload.Brands.Select(b => new ModelBrandOptionViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                }).ToList();

                viewModel.EngineTypes = reload.EngineTypes.Select(e => new ModelEngineTypeOptionViewModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                }).ToList();

                viewModel.BodyTypes = reload.BodyTypes.Select(b => new ModelBodyTypeOptionViewModel
                {
                    BodyTypeId = b.BodyTypeId,
                    Name = b.Name
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new ModelCreateServiceModel
            {
                BrandId = viewModel.BrandId,
                ModelName = viewModel.ModelName,
                EngineTypeId = viewModel.EngineTypeId,
                EngineVolume = viewModel.EngineVolume,
                HorsePower = viewModel.HorsePower,
                BodyTypeId = viewModel.BodyTypeId
            };

            await modelService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await modelService.GetEditPageModelAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ModelEditViewModel
            {
                ModelId = serviceModel.Model.ModelId,
                BrandId = serviceModel.Model.BrandId,
                ModelName = serviceModel.Model.ModelName,
                EngineTypeId = serviceModel.Model.EngineTypeId,
                EngineVolume = serviceModel.Model.EngineVolume,
                HorsePower = serviceModel.Model.HorsePower,
                BodyTypeId = serviceModel.Model.BodyTypeId,
                Brands = serviceModel.Brands.Select(b => new ModelBrandOptionViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                }).ToList(),
                EngineTypes = serviceModel.EngineTypes.Select(e => new ModelEngineTypeOptionViewModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                }).ToList(),
                BodyTypes = serviceModel.BodyTypes.Select(b => new ModelBodyTypeOptionViewModel
                {
                    BodyTypeId = b.BodyTypeId,
                    Name = b.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ModelEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var reload = await modelService.GetCreatePageModelAsync();

                viewModel.Brands = reload.Brands.Select(b => new ModelBrandOptionViewModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                }).ToList();

                viewModel.EngineTypes = reload.EngineTypes.Select(e => new ModelEngineTypeOptionViewModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                }).ToList();

                viewModel.BodyTypes = reload.BodyTypes.Select(b => new ModelBodyTypeOptionViewModel
                {
                    BodyTypeId = b.BodyTypeId,
                    Name = b.Name
                }).ToList();

                return View(viewModel);
            }

            var serviceModel = new ModelEditServiceModel
            {
                ModelId = viewModel.ModelId,
                BrandId = viewModel.BrandId,
                ModelName = viewModel.ModelName,
                EngineTypeId = viewModel.EngineTypeId,
                EngineVolume = viewModel.EngineVolume,
                HorsePower = viewModel.HorsePower,
                BodyTypeId = viewModel.BodyTypeId
            };

            await modelService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await modelService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new ModelDetailsViewModel
            {
                ModelId = serviceModel.ModelId,
                BrandName = serviceModel.BrandName,
                ModelName = serviceModel.ModelName,
                EngineTypeName = serviceModel.EngineTypeName,
                EngineVolume = serviceModel.EngineVolume,
                HorsePower = serviceModel.HorsePower,
                BodyTypeName = serviceModel.BodyTypeName
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await modelService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
