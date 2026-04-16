using KachaowAuto.Core.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceListServiceModel>> GetAllAsync();

        Task<ServiceCreatePageServiceModel> GetCreatePageModelAsync();
        Task CreateAsync(ServiceCreateServiceModel model);

        Task<ServiceEditPageServiceModel?> GetEditPageModelAsync(int id);
        Task UpdateAsync(ServiceEditServiceModel model);

        Task<ServiceDetailsServiceModel?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
