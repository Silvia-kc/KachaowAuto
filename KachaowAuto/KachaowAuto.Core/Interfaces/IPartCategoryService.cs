using KachaowAuto.Core.Models.PartCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IPartCategoryService
    {
        Task<IEnumerable<PartCategoryListServiceModel>> GetAllAsync();
        Task<PartCategoryDetailsServiceModel?> GetByIdAsync(int id);
        Task<PartCategoryEditServiceModel?> GetEditByIdAsync(int id);
        Task CreateAsync(PartCategoryCreateServiceModel model);
        Task UpdateAsync(PartCategoryEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
