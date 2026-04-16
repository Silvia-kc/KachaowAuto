using KachaowAuto.Core.Models.WorkshopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IWorkshopService
    {
        Task<IEnumerable<WorkshopListServiceModel>> GetAllAsync();
        Task<WorkshopCreatePageServiceModel> GetCreatePageModelAsync();
        Task CreateAsync(WorkshopCreateServiceModel model);
        Task<WorkshopEditPageServiceModel?> GetEditPageModelAsync(int id);
        Task UpdateAsync(WorkshopEditServiceModel model);
        Task<WorkshopDetailsServiceModel?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
