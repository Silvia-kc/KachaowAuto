using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.PartImageModels;
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
    public class PartImageService : IPartImageService
    {
        private readonly KachaowAutoDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public PartImageService(KachaowAutoDbContext _context, ICloudinaryService _cloudinaryService)
        {
            context = _context;
            cloudinaryService = _cloudinaryService;
        }

        public async Task<IEnumerable<PartImageListServiceModel>> GetAllAsync()
        {
            return await context.PartImages
                .Include(pi => pi.Part)
                .OrderByDescending(pi => pi.PartImageId)
                .Select(pi => new PartImageListServiceModel
                {
                    PartImageId = pi.PartImageId,
                    PartId = pi.PartId,
                    PartName = pi.Part != null ? pi.Part.PartName : "",
                    ImageUrl = pi.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<PartImageCreatePageServiceModel> GetCreatePageModelAsync(int? partId = null)
        {
            return new PartImageCreatePageServiceModel
            {
                Image = new PartImageCreateServiceModel
                {
                    PartId = partId ?? 0
                },
                Parts = await context.Parts
                    .OrderBy(p => p.PartName)
                    .Select(p => new PartOptionServiceModel
                    {
                        PartId = p.PartId,
                        PartName = p.PartName
                    })
                    .ToListAsync()
            };
        }

        public async Task<int?> CreateAsync(PartImageCreateServiceModel model)
        {
            if (model.PartId <= 0)
            {
                return null;
            }

            var partExists = await context.Parts.AnyAsync(p => p.PartId == model.PartId);
            if (!partExists)
            {
                return null;
            }

            if (model.ImageFile == null || model.ImageFile.Length == 0)
            {
                return null;
            }

            var uploadResult = await cloudinaryService.UploadImageAsync(model.ImageFile, "kachaowauto/parts");

            if (uploadResult == null || string.IsNullOrWhiteSpace(uploadResult.Url))
            {
                return null;
            }

            var image = new PartImage
            {
                PartId = model.PartId,
                ImageUrl = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            await context.PartImages.AddAsync(image);
            await context.SaveChangesAsync();

            return model.PartId;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            var image = await context.PartImages
                .FirstOrDefaultAsync(i => i.PartImageId == id);

            if (image == null)
            {
                return null;
            }

            var partId = image.PartId;

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await cloudinaryService.DeleteImageAsync(image.PublicId);
            }

            context.PartImages.Remove(image);
            await context.SaveChangesAsync();

            return partId;
        }
    }
}
