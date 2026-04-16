using KachaowAuto.Core.Models.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandListServiceModel>> GetAllAsync();
        Task<BrandDetailsServiceModel?> GetByIdAsync(int id);
        Task<BrandEditServiceModel?> GetEditByIdAsync(int id);
        Task CreateAsync(BrandCreateServiceModel model);
        Task UpdateAsync(BrandEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
