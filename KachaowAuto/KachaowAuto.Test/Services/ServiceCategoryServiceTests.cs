using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.ServiceCategoryModels;
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
    public class ServiceCategoryServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCategoriesWithServicesCount()
        {
            using var context = DbContextFactory.Create();

            var category1 = new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "Engine"
            };

            var category2 = new ServiceCategory
            {
                ServiceCategoryId = 2,
                CategoryName = "Diagnostics"
            };

            var service1 = new Service
            {
                ServiceId = 1,
                ServiceName = "Oil Change",
                Description = "Engine oil change",
                PriceFrom = 80,
                PriceTo = 120,
                ServiceCategoryId = 1,
                ServiceCategory = category1
            };

            var service2 = new Service
            {
                ServiceId = 2,
                ServiceName = "Spark Plug Replacement",
                Description = "Replace spark plugs",
                PriceFrom = 100,
                PriceTo = 150,
                ServiceCategoryId = 1,
                ServiceCategory = category1
            };

            var service3 = new Service
            {
                ServiceId = 3,
                ServiceName = "Computer Diagnostics",
                Description = "Full diagnostics",
                PriceFrom = 50,
                PriceTo = 90,
                ServiceCategoryId = 2,
                ServiceCategory = category2
            };

            context.ServiceCategories.AddRange(category1, category2);
            context.Services.AddRange(service1, service2, service3);

            await context.SaveChangesAsync();

            var sut = new ServiceCategoryService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var engine = result.First(x => x.CategoryName == "Engine");
            var diagnostics = result.First(x => x.CategoryName == "Diagnostics");

            Assert.Equal(2, engine.ServicesCount);
            Assert.Equal(1, diagnostics.ServicesCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.Add(new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "Suspension"
            });

            await context.SaveChangesAsync();

            var sut = new ServiceCategoryService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.ServiceCategoryId);
            Assert.Equal("Suspension", result.CategoryName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceCategoryService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.Add(new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "Electrical"
            });

            await context.SaveChangesAsync();

            var sut = new ServiceCategoryService(context);

            var result = await sut.GetEditByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.ServiceCategoryId);
            Assert.Equal("Electrical", result.CategoryName);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceCategoryService(context);

            var result = await sut.GetEditByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewCategory()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceCategoryService(context);

            var model = new ServiceCategoryCreateServiceModel
            {
                CategoryName = "Transmission"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.ServiceCategories.CountAsync());

            var category = await context.ServiceCategories.FirstAsync();
            Assert.Equal("Transmission", category.CategoryName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCategory_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.Add(new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "Old Category"
            });

            await context.SaveChangesAsync();

            var sut = new ServiceCategoryService(context);

            var model = new ServiceCategoryEditServiceModel
            {
                ServiceCategoryId = 1,
                CategoryName = "New Category"
            };

            await sut.UpdateAsync(model);

            var category = await context.ServiceCategories.FirstAsync();
            Assert.Equal("New Category", category.CategoryName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenCategoryDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceCategoryService(context);

            var model = new ServiceCategoryEditServiceModel
            {
                ServiceCategoryId = 999,
                CategoryName = "Does Not Matter"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.ServiceCategories.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveCategory_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.Add(new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "ToDelete"
            });

            await context.SaveChangesAsync();

            var sut = new ServiceCategoryService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.ServiceCategories.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceCategoryService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
