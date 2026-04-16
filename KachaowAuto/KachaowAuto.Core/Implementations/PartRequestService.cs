using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartRequest;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Implementations
{
    public class PartRequestService : IPartRequestService
    {
        private readonly KachaowAutoDbContext context;

        public PartRequestService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<PartRequestCreateServiceModel?> GetCreateModelAsync(int partId)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);

            if (part == null)
            {
                return null;
            }

            return new PartRequestCreateServiceModel
            {
                PartId = part.PartId,
                PartName = part.PartName,
                Quantity = 1
            };
        }

        public async Task<bool> CreateAsync(PartRequestCreateServiceModel model, int mechanicId)
        {
            var partExists = await context.Parts.AnyAsync(p => p.PartId == model.PartId);

            if (!partExists)
            {
                return false;
            }

            var request = new PartRequest
            {
                PartId = model.PartId,
                MechanicId = mechanicId,
                Quantity = model.Quantity,
                Note = model.Note,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };

            await context.PartRequests.AddAsync(request);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PartRequestMyRequestServiceModel>> GetMyRequestsAsync(int mechanicId)
        {
            return await context.PartRequests
                .Include(r => r.Part)
                .Where(r => r.MechanicId == mechanicId)
                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new PartRequestMyRequestServiceModel
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
        }

        public async Task<IEnumerable<PartRequestAdminServiceModel>> GetAllRequestsAsync()
        {
            return await context.PartRequests
                .Include(r => r.Part)
                .Include(r => r.Mechanic)
                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new PartRequestAdminServiceModel
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
        }

        public async Task<bool> ApproveAsync(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return false;

            request.Status = "Approved";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAsync(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return false;

            request.Status = "Rejected";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsSentAsync(int id, string? adminNote)
        {
            var request = await context.PartRequests.FirstOrDefaultAsync(r => r.PartRequestId == id);
            if (request == null) return false;

            request.Status = "Sent";
            request.AdminNote = adminNote;
            request.ProcessedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
