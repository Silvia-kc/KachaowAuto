using KachaowAuto.Core.Models.WorkshopModels;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WorkshopServiceImpl = KachaowAuto.Core.Implementations.WorkshopService;

namespace KachaowAuto.Tests.Services
{
    public class WorkshopServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllWorkshopsWithRegionData()
        {
            using var context = DbContextFactory.Create();

            var region1 = new Region
            {
                RegionId = 1,
                RegionName = "Stara Zagora"
            };

            var region2 = new Region
            {
                RegionId = 2,
                RegionName = "Plovdiv"
            };

            var workshop1 = new Workshop
            {
                WorkshopId = 1,
                Name = "Kachaow Auto 1",
                RegionId = 1,
                Region = region1,
                City = "Kazanlak",
                Address = "Center 1",
                PhoneNumber = "111111111",
                Latitude = 42.62m,
                Longitude = 25.39m,
                IsActive = true
            };

            var workshop2 = new Workshop
            {
                WorkshopId = 2,
                Name = "Kachaow Auto 2",
                RegionId = 2,
                Region = region2,
                City = "Plovdiv",
                Address = "Center 2",
                PhoneNumber = "222222222",
                Latitude = 42.14m,
                Longitude = 24.75m,
                IsActive = false
            };

            context.Regions.AddRange(region1, region2);
            context.Workshops.AddRange(workshop1, workshop2);
            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var first = result.First(x => x.WorkshopId == 1);
            var second = result.First(x => x.WorkshopId == 2);

            Assert.Equal("Kachaow Auto 1", first.Name);
            Assert.Equal("Stara Zagora", first.RegionName);
            Assert.Equal("Kazanlak", first.City);
            Assert.Equal("Center 1", first.Address);
            Assert.Equal("111111111", first.PhoneNumber);
            Assert.Equal(42.62m, first.Latitude);
            Assert.Equal(25.39m, first.Longitude);
            Assert.True(first.IsActive);

            Assert.Equal("Kachaow Auto 2", second.Name);
            Assert.Equal("Plovdiv", second.RegionName);
            Assert.False(second.IsActive);
        }

        [Fact]
        public async Task GetCreatePageModelAsync_ShouldReturnRegionsOrderedByName()
        {
            using var context = DbContextFactory.Create();

            context.Regions.AddRange(
                new Region { RegionId = 1, RegionName = "Varna" },
                new Region { RegionId = 2, RegionName = "Burgas" },
                new Region { RegionId = 3, RegionName = "Sofia" });

            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.GetCreatePageModelAsync();

            Assert.Equal(3, result.Regions.Count);
            Assert.Equal("Burgas", result.Regions[0].RegionName);
            Assert.Equal("Sofia", result.Regions[1].RegionName);
            Assert.Equal("Varna", result.Regions[2].RegionName);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewWorkshop()
        {
            using var context = DbContextFactory.Create();

            var sut = new WorkshopServiceImpl(context);

            var model = new WorkshopCreateServiceModel
            {
                Name = "New Workshop",
                RegionId = 1,
                City = "Kazanlak",
                Address = "Test Address",
                PhoneNumber = "0899999999",
                Latitude = 42.62m,
                Longitude = 25.39m,
                IsActive = true
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Workshops.CountAsync());

            var workshop = await context.Workshops.FirstAsync();
            Assert.Equal("New Workshop", workshop.Name);
            Assert.Equal(1, workshop.RegionId);
            Assert.Equal("Kazanlak", workshop.City);
            Assert.Equal("Test Address", workshop.Address);
            Assert.Equal("0899999999", workshop.PhoneNumber);
            Assert.Equal(42.62m, workshop.Latitude);
            Assert.Equal(25.39m, workshop.Longitude);
            Assert.True(workshop.IsActive);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnEditPageModel_WhenWorkshopExists()
        {
            using var context = DbContextFactory.Create();

            context.Regions.AddRange(
                new Region { RegionId = 1, RegionName = "Stara Zagora" },
                new Region { RegionId = 2, RegionName = "Plovdiv" });

            context.Workshops.Add(new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop Test",
                RegionId = 1,
                City = "Kazanlak",
                Address = "Address 1",
                PhoneNumber = "0888000000",
                Latitude = 42.60m,
                Longitude = 25.40m,
                IsActive = true
            });

            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.GetEditPageModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Workshop.WorkshopId);
            Assert.Equal("Workshop Test", result.Workshop.Name);
            Assert.Equal(1, result.Workshop.RegionId);
            Assert.Equal("Kazanlak", result.Workshop.City);
            Assert.Equal("Address 1", result.Workshop.Address);
            Assert.Equal("0888000000", result.Workshop.PhoneNumber);
            Assert.Equal(42.60m, result.Workshop.Latitude);
            Assert.Equal(25.40m, result.Workshop.Longitude);
            Assert.True(result.Workshop.IsActive);
            Assert.Equal(2, result.Regions.Count);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnNull_WhenWorkshopDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.GetEditPageModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateWorkshop_WhenWorkshopExists()
        {
            using var context = DbContextFactory.Create();

            context.Workshops.Add(new Workshop
            {
                WorkshopId = 1,
                Name = "Old Workshop",
                RegionId = 1,
                City = "Old City",
                Address = "Old Address",
                PhoneNumber = "0000",
                Latitude = 1.11m,
                Longitude = 2.22m,
                IsActive = false
            });

            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var model = new WorkshopEditServiceModel
            {
                WorkshopId = 1,
                Name = "New Workshop",
                RegionId = 2,
                City = "New City",
                Address = "New Address",
                PhoneNumber = "9999",
                Latitude = 42.50m,
                Longitude = 25.50m,
                IsActive = true
            };

            await sut.UpdateAsync(model);

            var workshop = await context.Workshops.FirstAsync();

            Assert.Equal("New Workshop", workshop.Name);
            Assert.Equal(2, workshop.RegionId);
            Assert.Equal("New City", workshop.City);
            Assert.Equal("New Address", workshop.Address);
            Assert.Equal("9999", workshop.PhoneNumber);
            Assert.Equal(42.50m, workshop.Latitude);
            Assert.Equal(25.50m, workshop.Longitude);
            Assert.True(workshop.IsActive);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenWorkshopDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new WorkshopServiceImpl(context);

            var model = new WorkshopEditServiceModel
            {
                WorkshopId = 999,
                Name = "Test",
                RegionId = 1,
                City = "City",
                Address = "Address",
                PhoneNumber = "000",
                Latitude = 1m,
                Longitude = 1m,
                IsActive = true
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Workshops.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnWorkshopDetails_WhenWorkshopExists()
        {
            using var context = DbContextFactory.Create();

            var region = new Region
            {
                RegionId = 1,
                RegionName = "Sofia"
            };

            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Sofia Workshop",
                RegionId = 1,
                Region = region,
                City = "Sofia",
                Address = "Boulevard 1",
                PhoneNumber = "0877000000",
                Latitude = 42.6977m,
                Longitude = 23.3219m,
                IsActive = true
            };

            context.Regions.Add(region);
            context.Workshops.Add(workshop);
            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.WorkshopId);
            Assert.Equal("Sofia Workshop", result.Name);
            Assert.Equal(1, result.RegionId);
            Assert.Equal("Sofia", result.RegionName);
            Assert.Equal("Sofia", result.City);
            Assert.Equal("Boulevard 1", result.Address);
            Assert.Equal("0877000000", result.PhoneNumber);
            Assert.Equal(42.6977m, result.Latitude);
            Assert.Equal(23.3219m, result.Longitude);
            Assert.True(result.IsActive);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenWorkshopDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveWorkshop_WhenWorkshopExists()
        {
            using var context = DbContextFactory.Create();

            context.Workshops.Add(new Workshop
            {
                WorkshopId = 1,
                Name = "Delete Workshop",
                RegionId = 1,
                City = "City",
                Address = "Address",
                PhoneNumber = "123",
                Latitude = 1m,
                Longitude = 2m,
                IsActive = true
            });

            await context.SaveChangesAsync();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Workshops.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenWorkshopDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new WorkshopServiceImpl(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}