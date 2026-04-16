using KachaowAuto.Core.Models.ModelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelListServiceModel>> GetAllAsync();

        Task<ModelCreatePageServiceModel> GetCreatePageModelAsync();
        Task CreateAsync(ModelCreateServiceModel model);

        Task<ModelEditPageServiceModel?> GetEditPageModelAsync(int id);
        Task UpdateAsync(ModelEditServiceModel model);

        Task<ModelDetailsServiceModel?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
