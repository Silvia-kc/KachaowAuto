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
        private readonly KachaowAutoDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public PartRequestController(
            KachaowAutoDbContext _context,
            UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }

        [Authorize(Roles = "Mechanic")]
        [HttpGet]
        public async Task<IActionResult> Create(int partId)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);

            if (part == null)
            {
                return NotFound();
            }

            var viewModel = new PartRequestCreateViewModel
            {
                PartId = part.PartId,
                PartName = part.PartName,
                Quantity = 1
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Mechanic")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartRequestCreateViewModel viewModel)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == viewModel.PartId);

            if (part == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.PartName = part.PartName;
                return View(viewModel);
            }

            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            int mechanicId = int.Parse(userIdStr);

            var request = new PartRequest
            {
                PartId = viewModel.PartId,
                MechanicId = mechanicId,
                Quantity = viewModel.Quantity,
                Note = viewModel.Note,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };

            context.PartRequests.Add(request);
            await context.SaveChangesAsync();

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

            var viewModels = await context.PartRequests
                .Include(r => r.Part)
                .Where(r => r.MechanicId == mechanicId)
                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new PartRequestMyRequestViewModel
                {
                    PartRequestId = r.PartRequestId,
                    PartId = r.PartId,
                    PartName = r.Part.PartName,
                    Quantity = r.Quantity,
                    Status = r.Status,
                    Note = r.Note,
                    AdminNote = r.AdminNote,
                    RequestedAt = r.RequestedAt,
                    ProcessedAt = r.ProcessedAt
                })
                .ToListAsync();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModels = await context.PartRequests
                .Include(r => r.Part)
                .Include(r => r.Mechanic)
                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new PartRequestAdminViewModel
                {
                    PartRequestId = r.PartRequestId,
                    PartId = r.PartId,
                    PartName = r.Part.PartName,
                    MechanicId = r.MechanicId,
                    MechanicName = r.Mechanic.UserName!,
                    Quantity = r.Quantity,
                    Status = r.Status,
                    Note = r.Note,
                    AdminNote = r.AdminNote,
                    RequestedAt = r.RequestedAt,
                    ProcessedAt = r.ProcessedAt
                })
                .ToListAsync();

            return View(viewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return NotFound();

            request.Status = "Approved";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return NotFound();

            request.Status = "Rejected";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsSent(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return NotFound();

            request.Status = "Sent";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
