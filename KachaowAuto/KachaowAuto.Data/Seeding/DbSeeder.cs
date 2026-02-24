using KachaowAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAllAsync(KachaowAutoDbContext db)
        {
            await SeedBodyTypesAsync(db);
            await SeedEngineTypesAsync(db);
            await SeedBrandsAndModelsAsync(db);
        }

        private static async Task SeedBodyTypesAsync(KachaowAutoDbContext db)
        {
            if (await db.BodyTypes.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("Data/Seeding/bodyTypes.json");
            var bodyTypes = JsonSerializer.Deserialize<List<BodyType>>(json);

            if (bodyTypes == null || bodyTypes.Count == 0) return;

            await db.BodyTypes.AddRangeAsync(bodyTypes);
            await db.SaveChangesAsync();
        }
        private static async Task SeedWorkshopsAsync(KachaowAutoDbContext db)
        {
            if (await db.Workshops.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("Data/SeedData/services.json");
            var workshops = JsonSerializer.Deserialize<List<Workshop>>(json);

            if (workshops == null || workshops.Count == 0) return;

            await db.Workshops.AddRangeAsync(workshops);
            await db.SaveChangesAsync();
        }

        private static async Task SeedEngineTypesAsync(KachaowAutoDbContext db)
        {
            if (await db.EngineTypes.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("Data/Seeding/engineTypes.json");
            var engineTypes = JsonSerializer.Deserialize<List<EngineType>>(json);

            if (engineTypes == null || engineTypes.Count == 0) return;

            await db.EngineTypes.AddRangeAsync(engineTypes);
            await db.SaveChangesAsync();
        }

        private static async Task SeedBrandsAndModelsAsync(KachaowAutoDbContext db)
        {
            if (await db.Brands.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("Data/Seeding/brandsModels.json");
            var brands = JsonSerializer.Deserialize<List<BrandSeed>>(json);

            if (brands == null || brands.Count == 0) return;

            foreach (var b in brands)
            {
                var brand = new Brand
                {
                    BrandName = b.BrandName
                };

                db.Brands.Add(brand);
                await db.SaveChangesAsync(); 

                foreach (var modelName in b.Models)
                {
                    db.Models.Add(new Model
                    {
                        ModelName = modelName,
                        BrandId = brand.BrandId
                    });
                }

                await db.SaveChangesAsync();
            }
        }
        private class BrandSeed
        {
            public string BrandName { get; set; } = null!;
            public List<string> Models { get; set; } = new();
        }
    }
}




