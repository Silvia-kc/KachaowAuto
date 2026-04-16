using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.BodyType;
using KachaowAuto.ViewModels.BodyType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]

    public class BodyTypeController : Controller
    {
        private readonly IBodyTypeService bodyTypeService;

        public BodyTypeController(IBodyTypeService _bodyTypeService)
        {
            bodyTypeService = _bodyTypeService;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await bodyTypeService.GetAllAsync();

            var viewModels = serviceModels.Select(bt => new BodyTypeListViewModel
            {
                BodyTypeId = bt.BodyTypeId,
                Name = bt.Name,
                ModelsCount = bt.ModelsCount
            }).ToList();

            return View(viewModels);
        }

        public IActionResult Create()
        {
            var viewModel = new BodyTypeCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BodyTypeCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new BodyTypeCreateServiceModel
            {
                Name = viewModel.Name
            };

            await bodyTypeService.CreateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceModel = await bodyTypeService.GetEditByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new BodyTypeEditViewModel
            {
                BodyTypeId = serviceModel.BodyTypeId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BodyTypeEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var serviceModel = new BodyTypeEditServiceModel
            {
                BodyTypeId = viewModel.BodyTypeId,
                Name = viewModel.Name
            };

            await bodyTypeService.UpdateAsync(serviceModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await bodyTypeService.GetByIdAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new BodyTypeDetailsViewModel
            {
                BodyTypeId = serviceModel.BodyTypeId,
                Name = serviceModel.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isDeleted = await bodyTypeService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
