using KachaowAuto.Core.Models.ServiceCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<IEnumerable<ServiceCategoryListServiceModel>> GetAllAsync();
        Task<ServiceCategoryDetailsServiceModel?> GetByIdAsync(int id);
        Task<ServiceCategoryEditServiceModel?> GetEditByIdAsync(int id);
        Task CreateAsync(ServiceCategoryCreateServiceModel model);
        Task UpdateAsync(ServiceCategoryEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
