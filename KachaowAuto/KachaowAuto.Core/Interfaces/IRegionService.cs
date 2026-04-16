using KachaowAuto.Core.Models.RegionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionListServiceModel>> GetAllAsync();
        Task<RegionDetailsServiceModel?> GetByIdAsync(int id);
        Task<RegionEditServiceModel?> GetEditByIdAsync(int id);
        Task CreateAsync(RegionCreateServiceModel model);
        Task UpdateAsync(RegionEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
