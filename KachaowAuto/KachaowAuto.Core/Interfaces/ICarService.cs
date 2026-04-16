using KachaowAuto.Core.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KachaowAuto.Core.Models.CarModels;
namespace KachaowAuto.Core.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarListServiceModel>> GetAllAsync();
        Task<IEnumerable<MyCarServiceModel>> GetMyCarsAsync(int userId);

        Task<CarCreatePageServiceModel> GetCreatePageModelAsync();
        Task CreateAsync(CarCreateServiceModel model);

        Task<CarEditPageServiceModel?> GetEditPageModelAsync(int id);
        Task UpdateAsync(CarEditServiceModel model);

        Task<CarDetailsServiceModel?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
