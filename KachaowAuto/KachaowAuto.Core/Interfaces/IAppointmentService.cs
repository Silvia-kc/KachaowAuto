
using KachaowAuto.Core.Models.Appointment;
using KachaowAuto.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentListServiceModel>> GetAllForIndexAsync(int? statusId);
        Task<List<AppointmentStatus>> GetStatusesAsync();

        Task<AppointmentDetailsServiceModel?> GetDetailsAsync(int id);

        Task<AppointmentCreatePageServiceModel> GetCreatePageDataAsync(int? brandId = null);
        Task<(bool Success, string? ErrorMessage)> CreateAsync(AppointmentCreateServiceModel model);

        Task<AppointmentEditPageServiceModel?> GetEditPageDataAsync(int id);
        Task<bool> EditAsync(AppointmentEditServiceModel model);

        Task<AppointmentDetailsServiceModel?> GetDeleteDataAsync(int id);
        Task<bool> DeleteAsync(int id);

        Task<bool> ChangeStatusAsync(int id, int statusId);

        Task<List<ModelLookupServiceModel>> GetModelsByBrandAsync(int brandId);
    }
}
