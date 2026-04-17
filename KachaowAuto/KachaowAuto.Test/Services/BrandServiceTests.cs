using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.Brand;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KachaowAuto.Tests.Services
{
    public class BrandServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBrandsWithModelsCount()
        {
            using var context = DbContextFactory.Create();

            var brand1 = new Brand { BrandId = 1, BrandName = "BMW" };
            var brand2 = new Brand { BrandId = 2, BrandName = "Audi" };

            var bodyType = new BodyType { BodyTypeId = 1, Name = "Sedan" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Diesel" };

            var model1 = new Model
            {
                ModelId = 1,
                ModelName = "320d",
                BrandId = 1,
                Brand = brand1,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var model2 = new Model
            {
                ModelId = 2,
                ModelName = "X5",
                BrandId = 1,
                Brand = brand1,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var model3 = new Model
            {
                ModelId = 3,
                ModelName = "A6",
                BrandId = 2,
                Brand = brand2,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Brands.AddRange(brand1, brand2);
            context.Models.AddRange(model1, model2, model3);

            await context.SaveChangesAsync();

            var sut = new BrandService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var bmw = result.First(x => x.BrandName == "BMW");
            var audi = result.First(x => x.BrandName == "Audi");

            Assert.Equal(2, bmw.ModelsCount);
            Assert.Equal(1, audi.ModelsCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBrand_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Brands.Add(new Brand
            {
                BrandId = 1,
                BrandName = "Mercedes"
            });

            await context.SaveChangesAsync();

            var sut = new BrandService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.BrandId);
            Assert.Equal("Mercedes", result.BrandName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new BrandService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Brands.Add(new Brand
            {
                BrandId = 1,
                BrandName = "Toyota"
            });

            await context.SaveChangesAsync();

            var sut = new BrandService(context);

            var result = await sut.GetEditByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.BrandId);
            Assert.Equal("Toyota", result.BrandName);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new BrandService(context);

            var result = await sut.GetEditByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewBrand()
        {
            using var context = DbContextFactory.Create();

            var sut = new BrandService(context);

            var model = new BrandCreateServiceModel
            {
                BrandName = "Honda"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Brands.CountAsync());

            var brand = await context.Brands.FirstAsync();
            Assert.Equal("Honda", brand.BrandName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBrand_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Brands.Add(new Brand
            {
                BrandId = 1,
                BrandName = "Old Name"
            });

            await context.SaveChangesAsync();

            var sut = new BrandService(context);

            var model = new BrandEditServiceModel
            {
                BrandId = 1,
                BrandName = "New Name"
            };

            await sut.UpdateAsync(model);

            var brand = await context.Brands.FirstAsync();
            Assert.Equal("New Name", brand.BrandName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenBrandDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new BrandService(context);

            var model = new BrandEditServiceModel
            {
                BrandId = 999,
                BrandName = "DoesNotMatter"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Brands.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveBrand_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Brands.Add(new Brand
            {
                BrandId = 1,
                BrandName = "Mazda"
            });

            await context.SaveChangesAsync();

            var sut = new BrandService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Brands.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenBrandDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new BrandService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
