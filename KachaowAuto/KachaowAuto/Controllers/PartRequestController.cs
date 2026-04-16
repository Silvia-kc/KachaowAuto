using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartRequest;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels.PartRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class PartRequestController : Controller
    {
        private readonly IPartRequestService partRequestService;
        private readonly UserManager<ApplicationUser> userManager;

        public PartRequestController(
            IPartRequestService _partRequestService,
            UserManager<ApplicationUser> _userManager)
        {
            partRequestService = _partRequestService;
            userManager = _userManager;
        }

        [Authorize(Roles = "Mechanic")]
        [HttpGet]
        public async Task<IActionResult> Create(int partId)
        {
            var serviceModel = await partRequestService.GetCreateModelAsync(partId);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new PartRequestCreateViewModel
            {
                PartId = serviceModel.PartId,
                PartName = serviceModel.PartName,
                Quantity = serviceModel.Quantity,
                Note = serviceModel.Note
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Mechanic")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartRequestCreateViewModel viewModel)
        {
            var serviceModelForPart = await partRequestService.GetCreateModelAsync(viewModel.PartId);

            if (serviceModelForPart == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.PartName = serviceModelForPart.PartName;
                return View(viewModel);
            }

            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            int mechanicId = int.Parse(userIdStr);

            var createServiceModel = new PartRequestCreateServiceModel
            {
                PartId = viewModel.PartId,
                PartName = serviceModelForPart.PartName,
                Quantity = viewModel.Quantity,
                Note = viewModel.Note
            };

            var isCreated = await partRequestService.CreateAsync(createServiceModel, mechanicId);

            if (!isCreated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(MyRequests));
        }

        [Authorize(Roles = "Mechanic")]
        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            int mechanicId = int.Parse(userIdStr);

            var serviceModels = await partRequestService.GetMyRequestsAsync(mechanicId);

            var viewModels = serviceModels.Select(r => new PartRequestMyRequestViewModel
            {
                PartRequestId = r.PartRequestId,
                PartId = r.PartId,
                PartName = r.PartName,
                Quantity = r.Quantity,
                Status = r.Status,
                Note = r.Note,
                AdminNote = r.AdminNote,
                RequestedAt = r.RequestedAt,
                ProcessedAt = r.ProcessedAt
            }).ToList();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceModels = await partRequestService.GetAllRequestsAsync();

            var viewModels = serviceModels.Select(r => new PartRequestAdminViewModel
            {
                PartRequestId = r.PartRequestId,
                PartId = r.PartId,
                PartName = r.PartName,
                MechanicId = r.MechanicId,
                MechanicName = r.MechanicName,
                Quantity = r.Quantity,
                Status = r.Status,
                Note = r.Note,
                AdminNote = r.AdminNote,
                RequestedAt = r.RequestedAt,
                ProcessedAt = r.ProcessedAt
            }).ToList();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, string? adminNote)
        {
            var isUpdated = await partRequestService.ApproveAsync(id, adminNote);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string? adminNote)
        {
            var isUpdated = await partRequestService.RejectAsync(id, adminNote);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsSent(int id, string? adminNote)
        {
            var isUpdated = await partRequestService.MarkAsSentAsync(id, adminNote);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
