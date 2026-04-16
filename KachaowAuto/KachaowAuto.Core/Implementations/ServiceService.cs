using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ServiceModels;
using KachaowAuto.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly KachaowAutoDbContext context;

        public ServiceService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ServiceListServiceModel>> GetAllAsync()
        {
            return await context.Services
                .Include(s => s.ServiceCategory)
                .Select(s => new ServiceListServiceModel
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    PriceFrom = s.PriceFrom,
                    PriceTo = s.PriceTo,
                    CategoryName = s.ServiceCategory.CategoryName
                })
                .ToListAsync();
        }

        public async Task<ServiceCreatePageServiceModel> GetCreatePageModelAsync()
        {
            return new ServiceCreatePageServiceModel
            {
                Categories = await context.ServiceCategories
                    .OrderBy(c => c.CategoryName)
                    .Select(c => new ServiceCategoryOptionServiceModel
                    {
                        ServiceCategoryId = c.ServiceCategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToListAsync()
            };
        }

        public async Task CreateAsync(ServiceCreateServiceModel model)
        {
            var service = new Data.Models.Service
            {
                ServiceName = model.ServiceName,
                Description = model.Description,
                PriceFrom = model.PriceFrom,
                PriceTo = model.PriceTo,
                ServiceCategoryId = model.ServiceCategoryId
            };

            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();
        }

        public async Task<ServiceEditPageServiceModel?> GetEditPageModelAsync(int id)
        {
            var service = await context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
            {
                return null;
            }

            return new ServiceEditPageServiceModel
            {
                Service = new ServiceEditServiceModel
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    Description = service.Description,
                    PriceFrom = service.PriceFrom,
                    PriceTo = service.PriceTo,
                    ServiceCategoryId = service.ServiceCategoryId
                },
                Categories = await context.ServiceCategories
                    .OrderBy(c => c.CategoryName)
                    .Select(c => new ServiceCategoryOptionServiceModel
                    {
                        ServiceCategoryId = c.ServiceCategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToListAsync()
            };
        }

        public async Task UpdateAsync(ServiceEditServiceModel model)
        {
            var service = await context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == model.ServiceId);

            if (service == null)
            {
                return;
            }

            service.ServiceName = model.ServiceName;
            service.Description = model.Description;
            service.PriceFrom = model.PriceFrom;
            service.PriceTo = model.PriceTo;
            service.ServiceCategoryId = model.ServiceCategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<ServiceDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Services
                .Include(s => s.ServiceCategory)
                .Where(s => s.ServiceId == id)
                .Select(s => new ServiceDetailsServiceModel
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    PriceFrom = s.PriceFrom,
                    PriceTo = s.PriceTo,
                    CategoryName = s.ServiceCategory.CategoryName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
            {
                return false;
            }

            context.Services.Remove(service);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
