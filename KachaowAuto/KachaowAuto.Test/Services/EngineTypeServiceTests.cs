using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.EngineTypeModels;
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
    public class EngineTypeServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEngineTypesWithModelsCount()
        {
            using var context = DbContextFactory.Create();

            var engineType1 = new EngineType
            {
                EngineTypeId = 1,
                Name = "Diesel"
            };

            var engineType2 = new EngineType
            {
                EngineTypeId = 2,
                Name = "Petrol"
            };

            var brand = new Brand
            {
                BrandId = 1,
                BrandName = "BMW"
            };

            var bodyType = new BodyType
            {
                BodyTypeId = 1,
                Name = "SUV"
            };

            var model1 = new Model
            {
                ModelId = 1,
                ModelName = "X5",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType1
            };

            var model2 = new Model
            {
                ModelId = 2,
                ModelName = "320i",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 2,
                EngineType = engineType2
            };

            var model3 = new Model
            {
                ModelId = 3,
                ModelName = "X3",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 2,
                EngineType = engineType2
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.AddRange(engineType1, engineType2);
            context.Models.AddRange(model1, model2, model3);

            await context.SaveChangesAsync();

            var sut = new EngineTypeService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var diesel = result.FirstOrDefault(x => x.Name == "Diesel");
            var petrol = result.FirstOrDefault(x => x.Name == "Petrol");

            Assert.NotNull(diesel);
            Assert.NotNull(petrol);
            Assert.Equal(1, diesel!.ModelsCount);
            Assert.Equal(2, petrol!.ModelsCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEngineType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.EngineTypes.Add(new EngineType
            {
                EngineTypeId = 1,
                Name = "Hybrid"
            });

            await context.SaveChangesAsync();

            var sut = new EngineTypeService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.EngineTypeId);
            Assert.Equal("Hybrid", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new EngineTypeService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.EngineTypes.Add(new EngineType
            {
                EngineTypeId = 1,
                Name = "Electric"
            });

            await context.SaveChangesAsync();

            var sut = new EngineTypeService(context);

            var result = await sut.GetEditByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.EngineTypeId);
            Assert.Equal("Electric", result.Name);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new EngineTypeService(context);

            var result = await sut.GetEditByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewEngineType()
        {
            using var context = DbContextFactory.Create();

            var sut = new EngineTypeService(context);

            var model = new EngineTypeCreateServiceModel
            {
                Name = "Gas"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.EngineTypes.CountAsync());

            var engineType = await context.EngineTypes.FirstAsync();
            Assert.Equal("Gas", engineType.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEngineType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.EngineTypes.Add(new EngineType
            {
                EngineTypeId = 1,
                Name = "Old Name"
            });

            await context.SaveChangesAsync();

            var sut = new EngineTypeService(context);

            var model = new EngineTypeEditServiceModel
            {
                EngineTypeId = 1,
                Name = "New Name"
            };

            await sut.UpdateAsync(model);

            var engineType = await context.EngineTypes.FirstAsync();
            Assert.Equal("New Name", engineType.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenEngineTypeDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new EngineTypeService(context);

            var model = new EngineTypeEditServiceModel
            {
                EngineTypeId = 999,
                Name = "Does Not Matter"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.EngineTypes.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveEngineType_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.EngineTypes.Add(new EngineType
            {
                EngineTypeId = 1,
                Name = "LPG"
            });

            await context.SaveChangesAsync();

            var sut = new EngineTypeService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.EngineTypes.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenEngineTypeDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new EngineTypeService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
