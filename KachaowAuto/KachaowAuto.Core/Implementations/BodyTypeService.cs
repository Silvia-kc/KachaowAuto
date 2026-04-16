using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.BodyType;
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
    public class BodyTypeService : IBodyTypeService
    {
        private readonly KachaowAutoDbContext context;

        public BodyTypeService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<BodyTypeListServiceModel>> GetAllAsync()
        {
            return await context.BodyTypes
                .Include(bt => bt.Models)
                .Select(bt => new BodyTypeListServiceModel
                {
                    BodyTypeId = bt.BodyTypeId,
                    Name = bt.Name,
                    ModelsCount = bt.Models.Count
                })
                .ToListAsync();
        }

        public async Task<BodyTypeDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.BodyTypes
                .Where(bt => bt.BodyTypeId == id)
                .Select(bt => new BodyTypeDetailsServiceModel
                {
                    BodyTypeId = bt.BodyTypeId,
                    Name = bt.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(BodyTypeCreateServiceModel model)
        {
            var bodyType = new BodyType
            {
                Name = model.Name
            };

            await context.BodyTypes.AddAsync(bodyType);
            await context.SaveChangesAsync();
        }

        public async Task<BodyTypeEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.BodyTypes
                .Where(bt => bt.BodyTypeId == id)
                .Select(bt => new BodyTypeEditServiceModel
                {
                    BodyTypeId = bt.BodyTypeId,
                    Name = bt.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(BodyTypeEditServiceModel model)
        {
            var bodyType = await context.BodyTypes
                .FirstOrDefaultAsync(bt => bt.BodyTypeId == model.BodyTypeId);

            if (bodyType == null)
            {
                return;
            }

            bodyType.Name = model.Name;

            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bodyType = await context.BodyTypes
                .FirstOrDefaultAsync(bt => bt.BodyTypeId == id);

            if (bodyType == null)
            {
                return false;
            }

            context.BodyTypes.Remove(bodyType);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
