using KachaowAuto.Core.Models.PartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IPartRequestService
    {
        Task<PartRequestCreateServiceModel?> GetCreateModelAsync(int partId);
        Task<bool> CreateAsync(PartRequestCreateServiceModel model, int mechanicId);

        Task<IEnumerable<PartRequestMyRequestServiceModel>> GetMyRequestsAsync(int mechanicId);
        Task<IEnumerable<PartRequestAdminServiceModel>> GetAllRequestsAsync();

        Task<bool> ApproveAsync(int id, string? adminNote);
        Task<bool> RejectAsync(int id, string? adminNote);
        Task<bool> MarkAsSentAsync(int id, string? adminNote);
    }
}
