using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.ServiceModels;
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
    public class ServiceServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllServicesWithCategoryName()
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
                Description = "Engine oil replacement",
                PriceFrom = 70,
                PriceTo = 100,
                ServiceCategoryId = 1,
                ServiceCategory = category1
            };

            var service2 = new Service
            {
                ServiceId = 2,
                ServiceName = "Computer Diagnostics",
                Description = "Full diagnostics",
                PriceFrom = 40,
                PriceTo = 60,
                ServiceCategoryId = 2,
                ServiceCategory = category2
            };

            context.ServiceCategories.AddRange(category1, category2);
            context.Services.AddRange(service1, service2);
            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var oilChange = result.First(x => x.ServiceName == "Oil Change");
            var diagnostics = result.First(x => x.ServiceName == "Computer Diagnostics");

            Assert.Equal("Engine", oilChange.CategoryName);
            Assert.Equal(70, oilChange.PriceFrom);
            Assert.Equal(100, oilChange.PriceTo);

            Assert.Equal("Diagnostics", diagnostics.CategoryName);
            Assert.Equal(40, diagnostics.PriceFrom);
            Assert.Equal(60, diagnostics.PriceTo);
        }

        [Fact]
        public async Task GetCreatePageModelAsync_ShouldReturnCategoriesOrderedByName()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.AddRange(
                new ServiceCategory
                {
                    ServiceCategoryId = 1,
                    CategoryName = "Transmission"
                },
                new ServiceCategory
                {
                    ServiceCategoryId = 2,
                    CategoryName = "Brakes"
                },
                new ServiceCategory
                {
                    ServiceCategoryId = 3,
                    CategoryName = "Engine"
                });

            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var result = await sut.GetCreatePageModelAsync();

            Assert.Equal(3, result.Categories.Count);
            Assert.Equal("Brakes", result.Categories[0].CategoryName);
            Assert.Equal("Engine", result.Categories[1].CategoryName);
            Assert.Equal("Transmission", result.Categories[2].CategoryName);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewService()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceService(context);

            var model = new ServiceCreateServiceModel
            {
                ServiceName = "Brake Repair",
                Description = "Repair brake system",
                PriceFrom = 120,
                PriceTo = 180,
                ServiceCategoryId = 1
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Services.CountAsync());

            var service = await context.Services.FirstAsync();
            Assert.Equal("Brake Repair", service.ServiceName);
            Assert.Equal("Repair brake system", service.Description);
            Assert.Equal(120, service.PriceFrom);
            Assert.Equal(180, service.PriceTo);
            Assert.Equal(1, service.ServiceCategoryId);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnEditPageModel_WhenServiceExists()
        {
            using var context = DbContextFactory.Create();

            context.ServiceCategories.AddRange(
                new ServiceCategory
                {
                    ServiceCategoryId = 1,
                    CategoryName = "Engine"
                },
                new ServiceCategory
                {
                    ServiceCategoryId = 2,
                    CategoryName = "Diagnostics"
                });

            context.Services.Add(new Service
            {
                ServiceId = 1,
                ServiceName = "Turbo Check",
                Description = "Check turbo",
                PriceFrom = 90,
                PriceTo = 140,
                ServiceCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var result = await sut.GetEditPageModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Service.ServiceId);
            Assert.Equal("Turbo Check", result.Service.ServiceName);
            Assert.Equal("Check turbo", result.Service.Description);
            Assert.Equal(90, result.Service.PriceFrom);
            Assert.Equal(140, result.Service.PriceTo);
            Assert.Equal(1, result.Service.ServiceCategoryId);
            Assert.Equal(2, result.Categories.Count);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnNull_WhenServiceDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceService(context);

            var result = await sut.GetEditPageModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateService_WhenServiceExists()
        {
            using var context = DbContextFactory.Create();

            context.Services.Add(new Service
            {
                ServiceId = 1,
                ServiceName = "Old Service",
                Description = "Old description",
                PriceFrom = 50,
                PriceTo = 80,
                ServiceCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var model = new ServiceEditServiceModel
            {
                ServiceId = 1,
                ServiceName = "New Service",
                Description = "New description",
                PriceFrom = 100,
                PriceTo = 150,
                ServiceCategoryId = 2
            };

            await sut.UpdateAsync(model);

            var service = await context.Services.FirstAsync();

            Assert.Equal("New Service", service.ServiceName);
            Assert.Equal("New description", service.Description);
            Assert.Equal(100, service.PriceFrom);
            Assert.Equal(150, service.PriceTo);
            Assert.Equal(2, service.ServiceCategoryId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenServiceDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceService(context);

            var model = new ServiceEditServiceModel
            {
                ServiceId = 999,
                ServiceName = "Test",
                Description = "Test",
                PriceFrom = 1,
                PriceTo = 2,
                ServiceCategoryId = 1
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Services.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnServiceDetails_WhenServiceExists()
        {
            using var context = DbContextFactory.Create();

            var category = new ServiceCategory
            {
                ServiceCategoryId = 1,
                CategoryName = "Suspension"
            };

            var service = new Service
            {
                ServiceId = 1,
                ServiceName = "Shock Absorber Replacement",
                Description = "Replace rear shock absorbers",
                PriceFrom = 130,
                PriceTo = 220,
                ServiceCategoryId = 1,
                ServiceCategory = category
            };

            context.ServiceCategories.Add(category);
            context.Services.Add(service);
            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.ServiceId);
            Assert.Equal("Shock Absorber Replacement", result.ServiceName);
            Assert.Equal("Replace rear shock absorbers", result.Description);
            Assert.Equal(130, result.PriceFrom);
            Assert.Equal(220, result.PriceTo);
            Assert.Equal("Suspension", result.CategoryName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenServiceDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveService_WhenServiceExists()
        {
            using var context = DbContextFactory.Create();

            context.Services.Add(new Service
            {
                ServiceId = 1,
                ServiceName = "To Delete",
                Description = "Delete me",
                PriceFrom = 20,
                PriceTo = 30,
                ServiceCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new ServiceService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Services.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenServiceDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new ServiceService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
