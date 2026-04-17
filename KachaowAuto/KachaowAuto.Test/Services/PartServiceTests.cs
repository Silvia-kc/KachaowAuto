using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models;
using KachaowAuto.Core.Models.PartModels;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Xunit;

namespace KachaowAuto.Tests.Services
{
    public class PartServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPartsOrderedByName()
        {
            using var context = DbContextFactory.Create();

            var category = new PartCategory
            {
                PartCategoryId = 1,
                Name = "Brakes"
            };

            var part1 = new Part
            {
                PartId = 1,
                PartName = "Brake Disc",
                Manufacturer = "Bosch",
                PartNumber = "BD-001",
                Description = "Front brake disc",
                UnitPrice = 120,
                IsActive = true,
                PartCategoryId = 1,
                Category = category
            };

            var part2 = new Part
            {
                PartId = 2,
                PartName = "Brake Pads",
                Manufacturer = "Brembo",
                PartNumber = "BP-002",
                Description = "Rear brake pads",
                UnitPrice = 80,
                IsActive = true,
                PartCategoryId = 1,
                Category = category
            };

            var image1 = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                Part = part1,
                ImageUrl = "disc-old.jpg",
                PublicId = "disc-old"
            };

            var image2 = new PartImage
            {
                PartImageId = 2,
                PartId = 1,
                Part = part1,
                ImageUrl = "disc-new.jpg",
                PublicId = "disc-new"
            };

            context.PartCategories.Add(category);
            context.Parts.AddRange(part1, part2);
            context.PartImages.AddRange(image1, image2);
            await context.SaveChangesAsync();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Brake Disc", result[0].PartName);
            Assert.Equal("Brake Pads", result[1].PartName);
            Assert.Equal("Brakes", result[0].CategoryName);
            Assert.Equal("disc-new.jpg", result[0].FirstImageUrl);
            Assert.Null(result[1].FirstImageUrl);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPartDetails_WhenExists()
        {
            using var context = DbContextFactory.Create();

            var category = new PartCategory
            {
                PartCategoryId = 1,
                Name = "Filters"
            };

            var part = new Part
            {
                PartId = 1,
                PartName = "Air Filter",
                Manufacturer = "Mann",
                PartNumber = "AF-100",
                Description = "Engine air filter",
                UnitPrice = 35,
                IsActive = true,
                PartCategoryId = 1,
                Category = category
            };

            var image1 = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                Part = part,
                ImageUrl = "img1.jpg",
                PublicId = "img1"
            };

            var image2 = new PartImage
            {
                PartImageId = 2,
                PartId = 1,
                Part = part,
                ImageUrl = "img2.jpg",
                PublicId = "img2"
            };

            context.PartCategories.Add(category);
            context.Parts.Add(part);
            context.PartImages.AddRange(image1, image2);
            await context.SaveChangesAsync();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.PartId);
            Assert.Equal("Air Filter", result.PartName);
            Assert.Equal("Mann", result.Manufacturer);
            Assert.Equal("AF-100", result.PartNumber);
            Assert.Equal("Engine air filter", result.Description);
            Assert.Equal(35, result.UnitPrice);
            Assert.True(result.IsActive);
            Assert.Equal("Filters", result.CategoryName);
            Assert.Equal(2, result.Images.Count);
            Assert.Equal(2, result.Images[0].PartImageId);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCreatePageModelAsync_ShouldReturnOrderedCategories()
        {
            using var context = DbContextFactory.Create();

            context.PartCategories.AddRange(
                new PartCategory { PartCategoryId = 1, Name = "Suspension" },
                new PartCategory { PartCategoryId = 2, Name = "Brakes" });

            await context.SaveChangesAsync();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.GetCreatePageModelAsync();

            Assert.Equal(2, result.Categories.Count);
            Assert.Equal("Brakes", result.Categories[0].Name);
            Assert.Equal("Suspension", result.Categories[1].Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreatePartWithoutImage_WhenNoImageProvided()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var model = new PartCreateServiceModel
            {
                PartName = "Oil Filter",
                Manufacturer = "Bosch",
                PartNumber = "OF-001",
                Description = "Oil filter",
                UnitPrice = 25,
                IsActive = true,
                PartCategoryId = 1,
                ImageFile = null
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Parts.CountAsync());
            Assert.Equal(0, await context.PartImages.CountAsync());

            var part = await context.Parts.FirstAsync();
            Assert.Equal("Oil Filter", part.PartName);
            Assert.Equal("Bosch", part.Manufacturer);
            Assert.Equal("OF-001", part.PartNumber);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreatePartAndImage_WhenImageProvidedAndUploadSucceeds()
        {
            using var context = DbContextFactory.Create();

            var fakeCloud = new FakeCloudinaryService
            {
                UploadUrl = "https://site.com/part.jpg",
                UploadPublicId = "parts/part-1"
            };

            var sut = new PartService(context, fakeCloud);

            var model = new PartCreateServiceModel
            {
                PartName = "Cabin Filter",
                Manufacturer = "Mann",
                PartNumber = "CF-001",
                Description = "Cabin filter",
                UnitPrice = 30,
                IsActive = true,
                PartCategoryId = 1,
                ImageFile = CreateFakeFile()
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Parts.CountAsync());
            Assert.Equal(1, await context.PartImages.CountAsync());

            var image = await context.PartImages.FirstAsync();
            Assert.Equal("https://site.com/part.jpg", image.ImageUrl);
            Assert.Equal("parts/part-1", image.PublicId);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreatePartWithoutImage_WhenUploadFails()
        {
            using var context = DbContextFactory.Create();

            var fakeCloud = new FakeCloudinaryService
            {
                UploadUrl = null,
                UploadPublicId = null
            };

            var sut = new PartService(context, fakeCloud);

            var model = new PartCreateServiceModel
            {
                PartName = "Fuel Filter",
                Manufacturer = "Mahle",
                PartNumber = "FF-001",
                Description = "Fuel filter",
                UnitPrice = 28,
                IsActive = true,
                PartCategoryId = 1,
                ImageFile = CreateFakeFile()
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Parts.CountAsync());
            Assert.Equal(0, await context.PartImages.CountAsync());
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnEditModel_WhenPartExists()
        {
            using var context = DbContextFactory.Create();

            context.PartCategories.AddRange(
                new PartCategory { PartCategoryId = 1, Name = "Brakes" },
                new PartCategory { PartCategoryId = 2, Name = "Engine" });

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Spark Plug",
                Manufacturer = "NGK",
                PartNumber = "SP-001",
                Description = "Spark plug",
                UnitPrice = 18,
                IsActive = true,
                PartCategoryId = 2
            });

            await context.SaveChangesAsync();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.GetEditPageModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Part.PartId);
            Assert.Equal("Spark Plug", result.Part.PartName);
            Assert.Equal("NGK", result.Part.Manufacturer);
            Assert.Equal("SP-001", result.Part.PartNumber);
            Assert.Equal("Spark plug", result.Part.Description);
            Assert.Equal(18, result.Part.UnitPrice);
            Assert.True(result.Part.IsActive);
            Assert.Equal(2, result.Part.PartCategoryId);
            Assert.Equal(2, result.Categories.Count);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnNull_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.GetEditPageModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePart_WhenPartExists()
        {
            using var context = DbContextFactory.Create();

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Old Part",
                Manufacturer = "Old Manufacturer",
                PartNumber = "OLD-001",
                Description = "Old description",
                UnitPrice = 10,
                IsActive = true,
                PartCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new PartService(context, new FakeCloudinaryService());

            var model = new PartEditServiceModel
            {
                PartId = 1,
                PartName = "New Part",
                Manufacturer = "New Manufacturer",
                PartNumber = "NEW-001",
                Description = "New description",
                UnitPrice = 55,
                IsActive = false,
                PartCategoryId = 2
            };

            await sut.UpdateAsync(model);

            var part = await context.Parts.FirstAsync();
            Assert.Equal("New Part", part.PartName);
            Assert.Equal("New Manufacturer", part.Manufacturer);
            Assert.Equal("NEW-001", part.PartNumber);
            Assert.Equal("New description", part.Description);
            Assert.Equal(55, part.UnitPrice);
            Assert.False(part.IsActive);
            Assert.Equal(2, part.PartCategoryId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var model = new PartEditServiceModel
            {
                PartId = 999,
                PartName = "Test",
                Manufacturer = "Test",
                PartNumber = "T-1",
                Description = "Test",
                UnitPrice = 1,
                IsActive = true,
                PartCategoryId = 1
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Parts.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePartAndImages_WhenPartExists()
        {
            using var context = DbContextFactory.Create();

            var part = new Part
            {
                PartId = 1,
                PartName = "Alternator",
                Manufacturer = "Valeo",
                PartNumber = "ALT-001",
                Description = "Alternator",
                UnitPrice = 250,
                IsActive = true,
                PartCategoryId = 1
            };

            var image1 = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                Part = part,
                ImageUrl = "img1.jpg",
                PublicId = "public1"
            };

            var image2 = new PartImage
            {
                PartImageId = 2,
                PartId = 1,
                Part = part,
                ImageUrl = "img2.jpg",
                PublicId = "public2"
            };

            context.Parts.Add(part);
            context.PartImages.AddRange(image1, image2);
            await context.SaveChangesAsync();

            var fakeCloud = new FakeCloudinaryService();
            var sut = new PartService(context, fakeCloud);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Parts.CountAsync());
            Assert.Equal(0, await context.PartImages.CountAsync());
            Assert.Contains("public1", fakeCloud.DeletedPublicIds);
            Assert.Contains("public2", fakeCloud.DeletedPublicIds);
        }

        [Fact]
        public async Task DeleteImageAsync_ShouldReturnFalse_WhenImageDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartService(context, new FakeCloudinaryService());

            var result = await sut.DeleteImageAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteImageAsync_ShouldDeleteImage_WhenImageExists()
        {
            using var context = DbContextFactory.Create();

            var image = new PartImage
            {
                PartImageId = 1,
                PartId = 1,
                ImageUrl = "img.jpg",
                PublicId = "img-public"
            };

            context.PartImages.Add(image);
            await context.SaveChangesAsync();

            var fakeCloud = new FakeCloudinaryService();
            var sut = new PartService(context, fakeCloud);

            var result = await sut.DeleteImageAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.PartImages.CountAsync());
            Assert.Contains("img-public", fakeCloud.DeletedPublicIds);
        }

        private static IFormFile CreateFakeFile()
        {
            var bytes = Encoding.UTF8.GetBytes("fake image content");
            var stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "file", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };
        }
    }
}