using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.WorkshopModels;
using KachaowAuto.Data.Models;
using KachaowAuto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Core.Implementations
{
    public class WorkshopService : IWorkshopService
    {
        private readonly KachaowAutoDbContext context;

        public WorkshopService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<WorkshopListServiceModel>> GetAllAsync()
        {
            return await context.Workshops
                .Include(w => w.Region)
                .Select(w => new WorkshopListServiceModel
                {
                    WorkshopId = w.WorkshopId,
                    Name = w.Name,
                    RegionName = w.Region.RegionName,
                    City = w.City,
                    Address = w.Address,
                    PhoneNumber = w.PhoneNumber,
                    Latitude = w.Latitude,
                    Longitude = w.Longitude,
                    IsActive = w.IsActive
                })
                .ToListAsync();
        }

        public async Task<WorkshopCreatePageServiceModel> GetCreatePageModelAsync()
        {
            return new WorkshopCreatePageServiceModel
            {
                Regions = await context.Regions
                    .OrderBy(r => r.RegionName)
                    .Select(r => new WorkshopRegionOptionServiceModel
                    {
                        RegionId = r.RegionId,
                        RegionName = r.RegionName
                    })
                    .ToListAsync()
            };
        }

        public async Task CreateAsync(WorkshopCreateServiceModel model)
        {
            var workshop = new Workshop
            {
                Name = model.Name,
                RegionId = model.RegionId,
                City = model.City,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                IsActive = model.IsActive
            };

            await context.Workshops.AddAsync(workshop);
            await context.SaveChangesAsync();
        }

        public async Task<WorkshopEditPageServiceModel?> GetEditPageModelAsync(int id)
        {
            var workshop = await context.Workshops
                .FirstOrDefaultAsync(w => w.WorkshopId == id);

            if (workshop == null)
            {
                return null;
            }

            return new WorkshopEditPageServiceModel
            {
                Workshop = new WorkshopEditServiceModel
                {
                    WorkshopId = workshop.WorkshopId,
                    Name = workshop.Name,
                    RegionId = workshop.RegionId,
                    City = workshop.City,
                    Address = workshop.Address,
                    PhoneNumber = workshop.PhoneNumber,
                    Latitude = workshop.Latitude,
                    Longitude = workshop.Longitude,
                    IsActive = workshop.IsActive
                },
                Regions = await context.Regions
                    .OrderBy(r => r.RegionName)
                    .Select(r => new WorkshopRegionOptionServiceModel
                    {
                        RegionId = r.RegionId,
                        RegionName = r.RegionName
                    })
                    .ToListAsync()
            };
        }

        public async Task UpdateAsync(WorkshopEditServiceModel model)
        {
            var workshop = await context.Workshops
                .FirstOrDefaultAsync(w => w.WorkshopId == model.WorkshopId);

            if (workshop == null)
            {
                return;
            }

            workshop.Name = model.Name;
            workshop.RegionId = model.RegionId;
            workshop.City = model.City;
            workshop.Address = model.Address;
            workshop.PhoneNumber = model.PhoneNumber;
            workshop.Latitude = model.Latitude;
            workshop.Longitude = model.Longitude;
            workshop.IsActive = model.IsActive;

            await context.SaveChangesAsync();
        }

        public async Task<WorkshopDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Workshops
                .Include(w => w.Region)
                .Where(w => w.WorkshopId == id)
                .Select(w => new WorkshopDetailsServiceModel
                {
                    WorkshopId = w.WorkshopId,
                    Name = w.Name,
                    RegionId = w.RegionId,
                    RegionName = w.Region.RegionName,
                    City = w.City,
                    Address = w.Address,
                    PhoneNumber = w.PhoneNumber,
                    Latitude = w.Latitude,
                    Longitude = w.Longitude,
                    IsActive = w.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var workshop = await context.Workshops
                .FirstOrDefaultAsync(w => w.WorkshopId == id);

            if (workshop == null)
            {
                return false;
            }

            context.Workshops.Remove(workshop);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
