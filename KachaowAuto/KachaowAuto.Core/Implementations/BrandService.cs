using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.Brand;
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
    public class BrandService : IBrandService
    {
        private readonly KachaowAutoDbContext context;

        public BrandService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<BrandListServiceModel>> GetAllAsync()
        {
            return await context.Brands
                .Include(b => b.Models)
                .Select(b => new BrandListServiceModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    ModelsCount = b.Models.Count
                })
                .ToListAsync();
        }

        public async Task<BrandDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Brands
                .Where(b => b.BrandId == id)
                .Select(b => new BrandDetailsServiceModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<BrandEditServiceModel?> GetEditByIdAsync(int id)
        {
            return await context.Brands
                .Where(b => b.BrandId == id)
                .Select(b => new BrandEditServiceModel
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(BrandCreateServiceModel model)
        {
            var brand = new Brand
            {
                BrandName = model.BrandName
            };

            await context.Brands.AddAsync(brand);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BrandEditServiceModel model)
        {
            var brand = await context.Brands
                .FirstOrDefaultAsync(b => b.BrandId == model.BrandId);

            if (brand == null)
            {
                return;
            }

            brand.BrandName = model.BrandName;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await context.Brands
                .FirstOrDefaultAsync(b => b.BrandId == id);

            if (brand == null)
            {
                return false;
            }

            context.Brands.Remove(brand);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
