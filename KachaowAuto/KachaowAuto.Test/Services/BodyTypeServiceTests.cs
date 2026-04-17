using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.BodyType;
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
    public class BodyTypeServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllBodyTypesWithModelsCount()
        {
            using var context = DbContextFactory.Create();

            var bodyType1 = new BodyType
            {
                BodyTypeId = 1,
                Name = "Sedan"
            };

            var bodyType2 = new BodyType
            {
                BodyTypeId = 2,
                Name = "SUV"
            };

            var brand = new Brand
            {
                BrandId = 1,
                BrandName = "BMW"
            };

            var engineType = new EngineType
            {
                EngineTypeId = 1,
                Name = "Diesel"
            };

            var model1 = new Model
            {
                ModelId = 1,
                ModelName = "320d",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType1,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var model2 = new Model
            {
                ModelId = 2,
                ModelName = "X5",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 2,
                BodyType = bodyType2,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var model3 = new Model
            {
                ModelId = 3,
                ModelName = "X6",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 2,
                BodyType = bodyType2,
                EngineTypeId = 1,
                EngineType = engineType
            };

            context.Brands.Add(brand);
            context.EngineTypes.Add(engineType);
            context.BodyTypes.AddRange(bodyType1, bodyType2);
            context.Models.AddRange(model1, model2, model3);

            await context.SaveChangesAsync();

            var sut = new BodyTypeService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var sedan = result.FirstOrDefault(x => x.Name == "Sedan");
            var suv = result.FirstOrDefault(x => x.Name == "SUV");

            Assert.NotNull(sedan);
            Assert.NotNull(suv);
            Assert.Equal(1, sedan!.ModelsCount);
            Assert.Equal(2, suv!.ModelsCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBodyType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.BodyTypes.Add(new BodyType
            {
                BodyTypeId = 1,
                Name = "Coupe"
            });

            await context.SaveChangesAsync();

            var sut = new BodyTypeService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.BodyTypeId);
            Assert.Equal("Coupe", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new BodyTypeService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewBodyType()
        {
            using var context = DbContextFactory.Create();

            var sut = new BodyTypeService(context);

            var model = new BodyTypeCreateServiceModel
            {
                Name = "Hatchback"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.BodyTypes.CountAsync());

            var bodyType = await context.BodyTypes.FirstAsync();
            Assert.Equal("Hatchback", bodyType.Name);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.BodyTypes.Add(new BodyType
            {
                BodyTypeId = 1,
                Name = "Van"
            });

            await context.SaveChangesAsync();

            var sut = new BodyTypeService(context);

            var result = await sut.GetEditByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.BodyTypeId);
            Assert.Equal("Van", result.Name);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new BodyTypeService(context);

            var result = await sut.GetEditByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBodyType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.BodyTypes.Add(new BodyType
            {
                BodyTypeId = 1,
                Name = "Old Name"
            });

            await context.SaveChangesAsync();

            var sut = new BodyTypeService(context);

            var model = new BodyTypeEditServiceModel
            {
                BodyTypeId = 1,
                Name = "New Name"
            };

            await sut.UpdateAsync(model);

            var bodyType = await context.BodyTypes.FirstAsync();
            Assert.Equal("New Name", bodyType.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenBodyTypeDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new BodyTypeService(context);

            var model = new BodyTypeEditServiceModel
            {
                BodyTypeId = 999,
                Name = "Does Not Matter"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.BodyTypes.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveBodyType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.BodyTypes.Add(new BodyType
            {
                BodyTypeId = 1,
                Name = "Pickup"
            });

            await context.SaveChangesAsync();

            var sut = new BodyTypeService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.BodyTypes.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenBodyTypeDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new BodyTypeService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
