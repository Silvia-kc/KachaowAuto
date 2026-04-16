using KachaowAuto.Core.Models.BodyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IBodyTypeService
    {
        Task<IEnumerable<BodyTypeListServiceModel>> GetAllAsync();
        Task<BodyTypeDetailsServiceModel?> GetByIdAsync(int id);
        Task CreateAsync(BodyTypeCreateServiceModel model);
        Task<BodyTypeEditServiceModel?> GetEditByIdAsync(int id);
        Task UpdateAsync(BodyTypeEditServiceModel model);
        Task<bool> DeleteAsync(int id);
    }
}
