using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartCategoryModels;
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
    public class PartCategoryService : IPartCategoryService
    {
        private readonly KachaowAutoDbContext context;

        public PartCategoryService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<PartCategoryListServiceModel>> GetAllAsync()
        {
            return await context.PartCategories
                .Include(pc => pc.Parts)
                .Select(pc => new PartCategoryListServiceModel
                {
                    PartCategoryId = pc.PartCategoryId,
                    Name = pc.Name,
                    PartsCount = pc.Parts.Count
                })
                .ToListAsync();
        }

        public async Task<PartCategoryDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.PartCategories
                .Where(pc => pc.PartCategoryId == id)
                .Select(pc => new PartCategoryDetailsServiceModel
                {
                    PartCategoryId = pc.PartCategoryId,
                    Name = pc.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PartCategoryEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.PartCategories
                .Where(pc => pc.PartCategoryId == id)
                .Select(pc => new PartCategoryEditServiceModel
                {
                    PartCategoryId = pc.PartCategoryId,
                    Name = pc.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PartCategoryCreateServiceModel model)
        {
            var partCategory = new PartCategory
            {
                Name = model.Name
            };

            await context.PartCategories.AddAsync(partCategory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PartCategoryEditServiceModel model)
        {
            var partCategory = await context.PartCategories
                .FirstOrDefaultAsync(pc => pc.PartCategoryId == model.PartCategoryId);

            if (partCategory == null)
            {
                return;
            }

            partCategory.Name = model.Name;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var partCategory = await context.PartCategories
                .FirstOrDefaultAsync(pc => pc.PartCategoryId == id);

            if (partCategory == null)
            {
                return false;
            }

            context.PartCategories.Remove(partCategory);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
