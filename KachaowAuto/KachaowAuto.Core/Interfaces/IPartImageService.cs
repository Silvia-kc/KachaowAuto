using KachaowAuto.Core.Models.PartImageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IPartImageService
    {
        Task<IEnumerable<PartImageListServiceModel>> GetAllAsync();
        Task<PartImageCreatePageServiceModel> GetCreatePageModelAsync(int? partId = null);
        Task<int?> CreateAsync(PartImageCreateServiceModel model);
        Task<int?> DeleteAsync(int id);
    }
}
