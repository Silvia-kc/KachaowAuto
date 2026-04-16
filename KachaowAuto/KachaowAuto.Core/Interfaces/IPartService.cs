using KachaowAuto.Core.Models.PartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IPartService
    {
        Task<IEnumerable<PartListServiceModel>> GetAllAsync();
        Task<PartDetailsServiceModel?> GetByIdAsync(int id);

        Task<PartCreatePageServiceModel> GetCreatePageModelAsync();
        Task CreateAsync(PartCreateServiceModel model);

        Task<PartEditPageServiceModel?> GetEditPageModelAsync(int id);
        Task UpdateAsync(PartEditServiceModel model);

        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteImageAsync(int imageId);
    }
}
