using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartModels;
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
    public class PartService : IPartService
    {
        private readonly KachaowAutoDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public PartService(KachaowAutoDbContext _context, ICloudinaryService _cloudinaryService)
        {
            context = _context;
            cloudinaryService = _cloudinaryService;
        }

        public async Task<IEnumerable<PartListServiceModel>> GetAllAsync()
        {
            return await context.Parts
                .Include(p => p.Category)
                .Include(p => p.Images)
                .OrderBy(p => p.PartName)
                .Select(p => new PartListServiceModel
                {
                    PartId = p.PartId,
                    PartName = p.PartName,
                    Manufacturer = p.Manufacturer,
                    PartNumber = p.PartNumber,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    UnitPrice = p.UnitPrice,
                    IsActive = p.IsActive,
                    FirstImageUrl = p.Images
                        .OrderByDescending(i => i.PartImageId)
                        .Select(i => i.ImageUrl)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<PartDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Parts
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Where(p => p.PartId == id)
                .Select(p => new PartDetailsServiceModel
                {
                    PartId = p.PartId,
                    PartName = p.PartName,
                    Manufacturer = p.Manufacturer,
                    PartNumber = p.PartNumber,
                    Description = p.Description,
                    UnitPrice = p.UnitPrice,
                    IsActive = p.IsActive,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    Images = p.Images
                        .OrderByDescending(i => i.PartImageId)
                        .Select(i => new PartDetailsImageServiceModel
                        {
                            PartImageId = i.PartImageId,
                            ImageUrl = i.ImageUrl
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PartCreatePageServiceModel> GetCreatePageModelAsync()
        {
            return new PartCreatePageServiceModel
            {
                Categories = await context.PartCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new PartCategoryOptionServiceModel
                    {
                        PartCategoryId = c.PartCategoryId,
                        Name = c.Name
                    })
                    .ToListAsync()
            };
        }

        public async Task CreateAsync(PartCreateServiceModel model)
        {
            var part = new Part
            {
                PartName = model.PartName,
                Manufacturer = model.Manufacturer,
                PartNumber = model.PartNumber,
                Description = model.Description,
                UnitPrice = model.UnitPrice,
                IsActive = model.IsActive,
                PartCategoryId = model.PartCategoryId
            };

            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadResult = await cloudinaryService.UploadImageAsync(model.ImageFile, "kachaowauto/parts");

                if (uploadResult != null && !string.IsNullOrWhiteSpace(uploadResult.Url))
                {
                    var partImage = new PartImage
                    {
                        PartId = part.PartId,
                        ImageUrl = uploadResult.Url,
                        PublicId = uploadResult.PublicId
                    };

                    await context.PartImages.AddAsync(partImage);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<PartEditPageServiceModel?> GetEditPageModelAsync(int id)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == id);

            if (part == null)
            {
                return null;
            }

            return new PartEditPageServiceModel
            {
                Part = new PartEditServiceModel
                {
                    PartId = part.PartId,
                    PartName = part.PartName,
                    Manufacturer = part.Manufacturer,
                    PartNumber = part.PartNumber,
                    Description = part.Description,
                    UnitPrice = part.UnitPrice,
                    IsActive = part.IsActive,
                    PartCategoryId = part.PartCategoryId
                },
                Categories = await context.PartCategories
                    .OrderBy(c => c.Name)
                    .Select(c => new PartCategoryOptionServiceModel
                    {
                        PartCategoryId = c.PartCategoryId,
                        Name = c.Name
                    })
                    .ToListAsync()
            };
        }

        public async Task UpdateAsync(PartEditServiceModel model)
        {
            var part = await context.Parts.FirstOrDefaultAsync(p => p.PartId == model.PartId);

            if (part == null)
            {
                return;
            }

            part.PartName = model.PartName;
            part.Manufacturer = model.Manufacturer;
            part.PartNumber = model.PartNumber;
            part.Description = model.Description;
            part.UnitPrice = model.UnitPrice;
            part.IsActive = model.IsActive;
            part.PartCategoryId = model.PartCategoryId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var part = await context.Parts
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.PartId == id);

            if (part == null)
            {
                return false;
            }

            if (part.Images.Any())
            {
                foreach (var image in part.Images)
                {
                    if (!string.IsNullOrWhiteSpace(image.PublicId))
                    {
                        await cloudinaryService.DeleteImageAsync(image.PublicId);
                    }
                }

                context.PartImages.RemoveRange(part.Images);
            }

            context.Parts.Remove(part);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await context.PartImages.FirstOrDefaultAsync(i => i.PartImageId == imageId);

            if (image == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await cloudinaryService.DeleteImageAsync(image.PublicId);
            }

            context.PartImages.Remove(image);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
