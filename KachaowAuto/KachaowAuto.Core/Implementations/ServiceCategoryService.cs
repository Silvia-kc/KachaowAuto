using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ServiceCategoryModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Implementations
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly KachaowAutoDbContext context;

        public ServiceCategoryService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ServiceCategoryListServiceModel>> GetAllAsync()
        {
            return await context.ServiceCategories
                .Include(sc => sc.Services)
                .Select(sc => new ServiceCategoryListServiceModel
                {
                    ServiceCategoryId = sc.ServiceCategoryId,
                    CategoryName = sc.CategoryName,
                    ServicesCount = sc.Services.Count
                })
                .ToListAsync();
        }

        public async Task<ServiceCategoryDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.ServiceCategories
                .Where(sc => sc.ServiceCategoryId == id)
                .Select(sc => new ServiceCategoryDetailsServiceModel
                {
                    ServiceCategoryId = sc.ServiceCategoryId,
                    CategoryName = sc.CategoryName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceCategoryEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.ServiceCategories
                .Where(sc => sc.ServiceCategoryId == id)
                .Select(sc => new ServiceCategoryEditServiceModel
                {
                    ServiceCategoryId = sc.ServiceCategoryId,
                    CategoryName = sc.CategoryName
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ServiceCategoryCreateServiceModel model)
        {
            var serviceCategory = new ServiceCategory
            {
                CategoryName = model.CategoryName
            };

            await context.ServiceCategories.AddAsync(serviceCategory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceCategoryEditServiceModel model)
        {
            var serviceCategory = await context.ServiceCategories
                .FirstOrDefaultAsync(sc => sc.ServiceCategoryId == model.ServiceCategoryId);

            if (serviceCategory == null)
            {
                return;
            }

            serviceCategory.CategoryName = model.CategoryName;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var serviceCategory = await context.ServiceCategories
                .FirstOrDefaultAsync(sc => sc.ServiceCategoryId == id);

            if (serviceCategory == null)
            {
                return false;
            }

            context.ServiceCategories.Remove(serviceCategory);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
