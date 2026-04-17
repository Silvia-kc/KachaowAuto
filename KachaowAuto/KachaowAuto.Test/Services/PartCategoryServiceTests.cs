using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.PartCategoryModels;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PartCategoryServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCategoriesWithPartsCount()
    {
        using var context = DbContextFactory.Create();

        var category1 = new PartCategory
        {
            PartCategoryId = 1,
            Name = "Brakes"
        };

        var category2 = new PartCategory
        {
            PartCategoryId = 2,
            Name = "Suspension"
        };

        var part1 = new Part
        {
            PartId = 1,
            PartName = "Brake Pads",
            PartCategoryId = 1,
            Category = category1
        };

        var part2 = new Part
        {
            PartId = 2,
            PartName = "Brake Disc",
            PartCategoryId = 1,
            Category = category1
        };

        var part3 = new Part
        {
            PartId = 3,
            PartName = "Shock Absorber",
            PartCategoryId = 2,
            Category = category2
        };

        context.PartCategories.AddRange(category1, category2);
        context.Parts.AddRange(part1, part2, part3);

        await context.SaveChangesAsync();

        var sut = new PartCategoryService(context);

        var result = (await sut.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);

        var brakes = result.First(x => x.Name == "Brakes");
        var suspension = result.First(x => x.Name == "Suspension");

        Assert.Equal(2, brakes.PartsCount);
        Assert.Equal(1, suspension.PartsCount);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenExists()
    {
        using var context = DbContextFactory.Create();

        context.PartCategories.Add(new PartCategory
        {
            PartCategoryId = 1,
            Name = "Engine"
        });

        await context.SaveChangesAsync();

        var sut = new PartCategoryService(context);

        var result = await sut.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.PartCategoryId);
        Assert.Equal("Engine", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        using var context = DbContextFactory.Create();

        var sut = new PartCategoryService(context);

        var result = await sut.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetEditByIdAsync_ShouldReturnEditModel_WhenExists()
    {
        using var context = DbContextFactory.Create();

        context.PartCategories.Add(new PartCategory
        {
            PartCategoryId = 1,
            Name = "Cooling"
        });

        await context.SaveChangesAsync();

        var sut = new PartCategoryService(context);

        var result = await sut.GetEditByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.PartCategoryId);
        Assert.Equal("Cooling", result.Name);
    }

    [Fact]
    public async Task GetEditByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        using var context = DbContextFactory.Create();

        var sut = new PartCategoryService(context);

        var result = await sut.GetEditByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddNewCategory()
    {
        using var context = DbContextFactory.Create();

        var sut = new PartCategoryService(context);

        var model = new PartCategoryCreateServiceModel
        {
            Name = "Electrical"
        };

        await sut.CreateAsync(model);

        Assert.Equal(1, await context.PartCategories.CountAsync());

        var category = await context.PartCategories.FirstAsync();
        Assert.Equal("Electrical", category.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory_WhenExists()
    {
        using var context = DbContextFactory.Create();

        context.PartCategories.Add(new PartCategory
        {
            PartCategoryId = 1,
            Name = "Old Name"
        });

        await context.SaveChangesAsync();

        var sut = new PartCategoryService(context);

        var model = new PartCategoryEditServiceModel
        {
            PartCategoryId = 1,
            Name = "New Name"
        };

        await sut.UpdateAsync(model);

        var category = await context.PartCategories.FirstAsync();
        Assert.Equal("New Name", category.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldDoNothing_WhenCategoryDoesNotExist()
    {
        using var context = DbContextFactory.Create();

        var sut = new PartCategoryService(context);

        var model = new PartCategoryEditServiceModel
        {
            PartCategoryId = 999,
            Name = "DoesNotMatter"
        };

        await sut.UpdateAsync(model);

        Assert.Equal(0, await context.PartCategories.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrueAndRemoveCategory_WhenExists()
    {
        using var context = DbContextFactory.Create();

        context.PartCategories.Add(new PartCategory
        {
            PartCategoryId = 1,
            Name = "ToDelete"
        });

        await context.SaveChangesAsync();

        var sut = new PartCategoryService(context);

        var result = await sut.DeleteAsync(1);

        Assert.True(result);
        Assert.Equal(0, await context.PartCategories.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        using var context = DbContextFactory.Create();

        var sut = new PartCategoryService(context);

        var result = await sut.DeleteAsync(999);

        Assert.False(result);
    }
}