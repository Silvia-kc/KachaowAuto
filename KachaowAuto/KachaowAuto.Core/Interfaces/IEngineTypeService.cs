using KachaowAuto.Core.Models.EngineTypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IEngineTypeService
    {
        Task<IEnumerable<EngineTypeListServiceModel>> GetAllAsync();
        Task<EngineTypeDetailsServiceModel?> GetByIdAsync(int id);
        Task<EngineTypeEditServiceModel?> GetEditByIdAsync(int id);
        Task CreateAsync(EngineTypeCreateServiceModel model);
        Task UpdateAsync(EngineTypeEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
