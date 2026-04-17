using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.RegionModels;
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
    public class RegionServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRegionsWithWorkshopsCount()
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
                Name = "Workshop 1",
                City = "Kazanlak",
                Address = "Center",
                RegionId = 1,
                Region = region1,
                IsActive = true
            };

            var workshop2 = new Workshop
            {
                WorkshopId = 2,
                Name = "Workshop 2",
                City = "Stara Zagora",
                Address = "Main Street",
                RegionId = 1,
                Region = region1,
                IsActive = true
            };

            var workshop3 = new Workshop
            {
                WorkshopId = 3,
                Name = "Workshop 3",
                City = "Plovdiv",
                Address = "Boulevard",
                RegionId = 2,
                Region = region2,
                IsActive = true
            };

            context.Regions.AddRange(region1, region2);
            context.Workshops.AddRange(workshop1, workshop2, workshop3);

            await context.SaveChangesAsync();

            var sut = new RegionService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var firstRegion = result.First(x => x.RegionName == "Stara Zagora");
            var secondRegion = result.First(x => x.RegionName == "Plovdiv");

            Assert.Equal(2, firstRegion.WorkshopsCount);
            Assert.Equal(1, secondRegion.WorkshopsCount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRegion_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Regions.Add(new Region
            {
                RegionId = 1,
                RegionName = "Sofia"
            });

            await context.SaveChangesAsync();

            var sut = new RegionService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.RegionId);
            Assert.Equal("Sofia", result.RegionName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenRegionDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new RegionService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenRegionExists()
        {
            using var context = DbContextFactory.Create();

            context.Regions.Add(new Region
            {
                RegionId = 1,
                RegionName = "Burgas"
            });

            await context.SaveChangesAsync();

            var sut = new RegionService(context);

            var result = await sut.GetEditByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.RegionId);
            Assert.Equal("Burgas", result.RegionName);
        }

        [Fact]
        public async Task GetEditByIdAsync_ShouldReturnNull_WhenRegionDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new RegionService(context);

            var result = await sut.GetEditByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewRegion()
        {
            using var context = DbContextFactory.Create();

            var sut = new RegionService(context);

            var model = new RegionCreateServiceModel
            {
                RegionName = "Varna"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Regions.CountAsync());

            var region = await context.Regions.FirstAsync();
            Assert.Equal("Varna", region.RegionName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRegion_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Regions.Add(new Region
            {
                RegionId = 1,
                RegionName = "Old Region"
            });

            await context.SaveChangesAsync();

            var sut = new RegionService(context);

            var model = new RegionEditServiceModel
            {
                RegionId = 1,
                RegionName = "New Region"
            };

            await sut.UpdateAsync(model);

            var region = await context.Regions.FirstAsync();
            Assert.Equal("New Region", region.RegionName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenRegionDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new RegionService(context);

            var model = new RegionEditServiceModel
            {
                RegionId = 999,
                RegionName = "Does Not Matter"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Regions.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveRegion_WhenExists()
        {
            using var context = DbContextFactory.Create();

            context.Regions.Add(new Region
            {
                RegionId = 1,
                RegionName = "ToDelete"
            });

            await context.SaveChangesAsync();

            var sut = new RegionService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Regions.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenRegionDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new RegionService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
