using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.RegionModels;
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
    public class RegionService : IRegionService
    {
        private readonly KachaowAutoDbContext context;

        public RegionService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<RegionListServiceModel>> GetAllAsync()
        {
            return await context.Regions
                .Include(r => r.Workshops)
                .Select(r => new RegionListServiceModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName,
                    WorkshopsCount = r.Workshops.Count
                })
                .ToListAsync();
        }

        public async Task<RegionDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Regions
                .Where(r => r.RegionId == id)
                .Select(r => new RegionDetailsServiceModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RegionEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.Regions
                .Where(r => r.RegionId == id)
                .Select(r => new RegionEditServiceModel
                {
                    RegionId = r.RegionId,
                    RegionName = r.RegionName
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(RegionCreateServiceModel model)
        {
            var region = new Region
            {
                RegionName = model.RegionName
            };

            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RegionEditServiceModel model)
        {
            var region = await context.Regions
                .FirstOrDefaultAsync(r => r.RegionId == model.RegionId);

            if (region == null)
            {
                return;
            }

            region.RegionName = model.RegionName;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var region = await context.Regions
                .FirstOrDefaultAsync(r => r.RegionId == id);

            if (region == null)
            {
                return false;
            }

            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return true;
        }
    }

}
