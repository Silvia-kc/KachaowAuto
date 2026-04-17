using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models;
using KachaowAuto.Core.Models.PartImageModels;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KachaowAuto.Tests.Services
{
    public class PartImageServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnImagesOrderedDescending()
        {
            using var context = DbContextFactory.Create();

            var category = new PartCategory { PartCategoryId = 1, Name = "Brakes" };

            var part = new Part
            {
                PartId = 1,
                PartName = "Brake Pads",
                PartCategoryId = 1,
                Category = category
            };

            var img1 = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                Part = part,
                ImageUrl = "url1",
                PublicId = "p1"
            };

            var img2 = new PartImage
            {
                PartImageId = 2,
                PartId = 1,
                Part = part,
                ImageUrl = "url2",
                PublicId = "p2"
            };

            context.PartCategories.Add(category);
            context.Parts.Add(part);
            context.PartImages.AddRange(img1, img2);
            await context.SaveChangesAsync();

            var sut = new PartImageService(context, new FakeCloudinaryService());

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[0].PartImageId);
            Assert.Equal("Brake Pads", result[0].PartName);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNull_WhenPartIdInvalid()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartImageService(context, new FakeCloudinaryService());

            var result = await sut.CreateAsync(new PartImageCreateServiceModel
            {
                PartId = 0,
                ImageFile = CreateFakeFile()
            });

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNull_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartImageService(context, new FakeCloudinaryService());

            var result = await sut.CreateAsync(new PartImageCreateServiceModel
            {
                PartId = 999,
                ImageFile = CreateFakeFile()
            });

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNull_WhenImageMissing()
        {
            using var context = DbContextFactory.Create();

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Filter",
                PartCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new PartImageService(context, new FakeCloudinaryService());

            var result = await sut.CreateAsync(new PartImageCreateServiceModel
            {
                PartId = 1,
                ImageFile = null
            });

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateImage_WhenValid()
        {
            using var context = DbContextFactory.Create();

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Air Filter",
                PartCategoryId = 1
            });

            await context.SaveChangesAsync();

            var fakeCloud = new FakeCloudinaryService
            {
                UploadUrl = "https://img.com/test.jpg",
                UploadPublicId = "parts/test"
            };

            var sut = new PartImageService(context, fakeCloud);

            var result = await sut.CreateAsync(new PartImageCreateServiceModel
            {
                PartId = 1,
                ImageFile = CreateFakeFile()
            });

            Assert.Equal(1, result);
            Assert.Equal(1, await context.PartImages.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenNotFound()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartImageService(context, new FakeCloudinaryService());

            var result = await sut.DeleteAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteImage_WhenExists()
        {
            using var context = DbContextFactory.Create();

            var part = new Part
            {
                PartId = 1,
                PartName = "Lamp",
                PartCategoryId = 1
            };

            var image = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                Part = part,
                ImageUrl = "url",
                PublicId = "public123"
            };

            context.Parts.Add(part);
            context.PartImages.Add(image);
            await context.SaveChangesAsync();

            var fakeCloud = new FakeCloudinaryService();
            var sut = new PartImageService(context, fakeCloud);

            var result = await sut.DeleteAsync(1);

            Assert.Equal(1, result);
            Assert.Equal(0, await context.PartImages.CountAsync());
            Assert.Contains("public123", fakeCloud.DeletedPublicIds);
        }

        private static IFormFile CreateFakeFile()
        {
            var bytes = Encoding.UTF8.GetBytes("fake image");
            var stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "file", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
        }
    }

}
