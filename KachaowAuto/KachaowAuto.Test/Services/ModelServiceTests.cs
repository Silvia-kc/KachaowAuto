using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.ModelModels;
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
    public class ModelServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllModelsWithRelatedData()
        {
            using var context = DbContextFactory.Create();

            var brand1 = new Brand { BrandId = 1, BrandName = "BMW" };
            var brand2 = new Brand { BrandId = 2, BrandName = "Audi" };

            var engineType1 = new EngineType { EngineTypeId = 1, Name = "Diesel" };
            var engineType2 = new EngineType { EngineTypeId = 2, Name = "Petrol" };

            var bodyType1 = new BodyType { BodyTypeId = 1, Name = "SUV" };
            var bodyType2 = new BodyType { BodyTypeId = 2, Name = "Sedan" };

            var model1 = new Model
            {
                ModelId = 1,
                BrandId = 1,
                Brand = brand1,
                ModelName = "X5",
                EngineTypeId = 1,
                EngineType = engineType1,
                EngineVolume = 3.0m,
                HorsePower = 286,
                BodyTypeId = 1,
                BodyType = bodyType1
            };

            var model2 = new Model
            {
                ModelId = 2,
                BrandId = 2,
                Brand = brand2,
                ModelName = "A6",
                EngineTypeId = 2,
                EngineType = engineType2,
                EngineVolume = 2.0m,
                HorsePower = 245,
                BodyTypeId = 2,
                BodyType = bodyType2
            };

            context.Brands.AddRange(brand1, brand2);
            context.EngineTypes.AddRange(engineType1, engineType2);
            context.BodyTypes.AddRange(bodyType1, bodyType2);
            context.Models.AddRange(model1, model2);

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var x5 = result.First(x => x.ModelName == "X5");
            var a6 = result.First(x => x.ModelName == "A6");

            Assert.Equal("BMW", x5.BrandName);
            Assert.Equal("Diesel", x5.EngineTypeName);
            Assert.Equal(3.0m, x5.EngineVolume);
            Assert.Equal(286, x5.HorsePower);
            Assert.Equal("SUV", x5.BodyTypeName);

            Assert.Equal("Audi", a6.BrandName);
            Assert.Equal("Petrol", a6.EngineTypeName);
            Assert.Equal(2.0m, a6.EngineVolume);
            Assert.Equal(245, a6.HorsePower);
            Assert.Equal("Sedan", a6.BodyTypeName);
        }

        [Fact]
        public async Task GetCreatePageModelAsync_ShouldReturnBrandsEngineTypesAndBodyTypes()
        {
            using var context = DbContextFactory.Create();

            context.Brands.AddRange(
                new Brand { BrandId = 1, BrandName = "BMW" },
                new Brand { BrandId = 2, BrandName = "Audi" });

            context.EngineTypes.AddRange(
                new EngineType { EngineTypeId = 1, Name = "Diesel" },
                new EngineType { EngineTypeId = 2, Name = "Hybrid" });

            context.BodyTypes.AddRange(
                new BodyType { BodyTypeId = 1, Name = "SUV" },
                new BodyType { BodyTypeId = 2, Name = "Coupe" });

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var result = await sut.GetCreatePageModelAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Brands.Count());
            Assert.Equal(2, result.EngineTypes.Count());
            Assert.Equal(2, result.BodyTypes.Count());
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewModel()
        {
            using var context = DbContextFactory.Create();

            var sut = new ModelService(context);

            var model = new ModelCreateServiceModel
            {
                BrandId = 1,
                ModelName = "Q7",
                EngineTypeId = 2,
                EngineVolume = 3.0m,
                HorsePower = 340,
                BodyTypeId = 1
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Models.CountAsync());

            var entity = await context.Models.FirstAsync();
            Assert.Equal(1, entity.BrandId);
            Assert.Equal("Q7", entity.ModelName);
            Assert.Equal(2, entity.EngineTypeId);
            Assert.Equal(3.0m, entity.EngineVolume);
            Assert.Equal(340, entity.HorsePower);
            Assert.Equal(1, entity.BodyTypeId);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnEditPageModel_WhenModelExists()
        {
            using var context = DbContextFactory.Create();

            context.Brands.AddRange(
                new Brand { BrandId = 1, BrandName = "BMW" },
                new Brand { BrandId = 2, BrandName = "Audi" });

            context.EngineTypes.AddRange(
                new EngineType { EngineTypeId = 1, Name = "Diesel" },
                new EngineType { EngineTypeId = 2, Name = "Petrol" });

            context.BodyTypes.AddRange(
                new BodyType { BodyTypeId = 1, Name = "SUV" },
                new BodyType { BodyTypeId = 2, Name = "Sedan" });

            context.Models.Add(new Model
            {
                ModelId = 1,
                BrandId = 1,
                ModelName = "X3",
                EngineTypeId = 1,
                EngineVolume = 2.0m,
                HorsePower = 190,
                BodyTypeId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var result = await sut.GetEditPageModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Model.ModelId);
            Assert.Equal(1, result.Model.BrandId);
            Assert.Equal("X3", result.Model.ModelName);
            Assert.Equal(1, result.Model.EngineTypeId);
            Assert.Equal(2.0m, result.Model.EngineVolume);
            Assert.Equal(190, result.Model.HorsePower);
            Assert.Equal(1, result.Model.BodyTypeId);

            Assert.Equal(2, result.Brands.Count());
            Assert.Equal(2, result.EngineTypes.Count());
            Assert.Equal(2, result.BodyTypes.Count());
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnNull_WhenModelDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ModelService(context);

            var result = await sut.GetEditPageModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Models.Add(new Model
            {
                ModelId = 1,
                BrandId = 1,
                ModelName = "Old Name",
                EngineTypeId = 1,
                EngineVolume = 2.0m,
                HorsePower = 150,
                BodyTypeId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var model = new ModelEditServiceModel
            {
                ModelId = 1,
                BrandId = 2,
                ModelName = "New Name",
                EngineTypeId = 3,
                EngineVolume = 3.0m,
                HorsePower = 300,
                BodyTypeId = 2
            };

            await sut.UpdateAsync(model);

            var entity = await context.Models.FirstAsync();

            Assert.Equal(2, entity.BrandId);
            Assert.Equal("New Name", entity.ModelName);
            Assert.Equal(3, entity.EngineTypeId);
            Assert.Equal(3.0m, entity.EngineVolume);
            Assert.Equal(300, entity.HorsePower);
            Assert.Equal(2, entity.BodyTypeId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenModelDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ModelService(context);

            var model = new ModelEditServiceModel
            {
                ModelId = 999,
                BrandId = 1,
                ModelName = "Test",
                EngineTypeId = 1,
                EngineVolume = 2.0m,
                HorsePower = 100,
                BodyTypeId = 1
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Models.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnModelDetails_WhenExists()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Mercedes" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Diesel" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Coupe" };

            var model = new Model
            {
                ModelId = 1,
                BrandId = 1,
                Brand = brand,
                ModelName = "C220",
                EngineTypeId = 1,
                EngineType = engineType,
                EngineVolume = 2.2m,
                HorsePower = 200,
                BodyTypeId = 1,
                BodyType = bodyType
            };

            context.Brands.Add(brand);
            context.EngineTypes.Add(engineType);
            context.BodyTypes.Add(bodyType);
            context.Models.Add(model);

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.ModelId);
            Assert.Equal("Mercedes", result.BrandName);
            Assert.Equal("C220", result.ModelName);
            Assert.Equal("Diesel", result.EngineTypeName);
            Assert.Equal(2.2m, result.EngineVolume);
            Assert.Equal(200, result.HorsePower);
            Assert.Equal("Coupe", result.BodyTypeName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenModelDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ModelService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Models.Add(new Model
            {
                ModelId = 1,
                BrandId = 1,
                ModelName = "ToDelete",
                EngineTypeId = 1,
                EngineVolume = 1.6m,
                HorsePower = 120,
                BodyTypeId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ModelService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Models.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenModelDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ModelService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
