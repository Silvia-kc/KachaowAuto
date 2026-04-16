using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.ModelModels;
using KachaowAuto.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Implementations
{
    public class ModelService : IModelService
    {
        private readonly KachaowAutoDbContext context;

        public ModelService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ModelListServiceModel>> GetAllAsync()
        {
            return await context.Models
                .Include(m => m.Brand)
                .Include(m => m.EngineType)
                .Include(m => m.BodyType)
                .Select(m => new ModelListServiceModel
                {
                    ModelId = m.ModelId,
                    BrandName = m.Brand.BrandName,
                    ModelName = m.ModelName,
                    EngineTypeName = m.EngineType.Name,
                    EngineVolume = m.EngineVolume,
                    HorsePower = m.HorsePower,
                    BodyTypeName = m.BodyType.Name
                })
                .ToListAsync();
        }

        public async Task<ModelCreatePageServiceModel> GetCreatePageModelAsync()
        {
            return new ModelCreatePageServiceModel
            {
                Brands = await context.Brands.ToListAsync(),
                EngineTypes = await context.EngineTypes.ToListAsync(),
                BodyTypes = await context.BodyTypes.ToListAsync()
            };
        }

        public async Task CreateAsync(ModelCreateServiceModel model)
        {
            var entity = new Data.Models.Model
            {
                BrandId = model.BrandId,
                ModelName = model.ModelName,
                EngineTypeId = model.EngineTypeId,
                EngineVolume = model.EngineVolume,
                HorsePower = model.HorsePower,
                BodyTypeId = model.BodyTypeId
            };

            await context.Models.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<ModelEditPageServiceModel?> GetEditPageModelAsync(int id)
        {
            var entity = await context.Models
                .FirstOrDefaultAsync(m => m.ModelId == id);

            if (entity == null)
            {
                return null;
            }

            return new ModelEditPageServiceModel
            {
                Model = new ModelEditServiceModel
                {
                    ModelId = entity.ModelId,
                    BrandId = entity.BrandId,
                    ModelName = entity.ModelName,
                    EngineTypeId = entity.EngineTypeId,
                    EngineVolume = entity.EngineVolume,
                    HorsePower = entity.HorsePower,
                    BodyTypeId = entity.BodyTypeId
                },
                Brands = await context.Brands.ToListAsync(),
                EngineTypes = await context.EngineTypes.ToListAsync(),
                BodyTypes = await context.BodyTypes.ToListAsync()
            };
        }

        public async Task UpdateAsync(ModelEditServiceModel model)
        {
            var entity = await context.Models
                .FirstOrDefaultAsync(m => m.ModelId == model.ModelId);

            if (entity == null)
            {
                return;
            }

            entity.BrandId = model.BrandId;
            entity.ModelName = model.ModelName;
            entity.EngineTypeId = model.EngineTypeId;
            entity.EngineVolume = model.EngineVolume;
            entity.HorsePower = model.HorsePower;
            entity.BodyTypeId = model.BodyTypeId;

            await context.SaveChangesAsync();
        }

        public async Task<ModelDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Models
                .Include(m => m.Brand)
                .Include(m => m.EngineType)
                .Include(m => m.BodyType)
                .Where(m => m.ModelId == id)
                .Select(m => new ModelDetailsServiceModel
                {
                    ModelId = m.ModelId,
                    BrandName = m.Brand.BrandName,
                    ModelName = m.ModelName,
                    EngineTypeName = m.EngineType.Name,
                    EngineVolume = m.EngineVolume,
                    HorsePower = m.HorsePower,
                    BodyTypeName = m.BodyType.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await context.Models
                .FirstOrDefaultAsync(m => m.ModelId == id);

            if (entity == null)
            {
                return false;
            }

            context.Models.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
