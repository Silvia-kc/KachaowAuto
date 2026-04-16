using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.EngineTypeModels;
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
    public class EngineTypeService : IEngineTypeService
    {
        private readonly KachaowAutoDbContext context;

        public EngineTypeService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<EngineTypeListServiceModel>> GetAllAsync()
        {
            return await context.EngineTypes
                .Include(e => e.Models)
                .Select(e => new EngineTypeListServiceModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name,
                    ModelsCount = e.Models.Count
                })
                .ToListAsync();
        }

        public async Task<EngineTypeDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.EngineTypes
                .Where(e => e.EngineTypeId == id)
                .Select(e => new EngineTypeDetailsServiceModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EngineTypeEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.EngineTypes
                .Where(e => e.EngineTypeId == id)
                .Select(e => new EngineTypeEditServiceModel
                {
                    EngineTypeId = e.EngineTypeId,
                    Name = e.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(EngineTypeCreateServiceModel model)
        {
            var engineType = new EngineType
            {
                Name = model.Name
            };

            await context.EngineTypes.AddAsync(engineType);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EngineTypeEditServiceModel model)
        {
            var engineType = await context.EngineTypes
                .FirstOrDefaultAsync(e => e.EngineTypeId == model.EngineTypeId);

            if (engineType == null)
            {
                return;
            }

            engineType.Name = model.Name;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var engineType = await context.EngineTypes
                .FirstOrDefaultAsync(e => e.EngineTypeId == id);

            if (engineType == null)
            {
                return false;
            }

            context.EngineTypes.Remove(engineType);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
