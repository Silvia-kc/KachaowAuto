using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAllAsync(KachaowAutoDbContext db)
        {
            await SeedRegionsAsync(db);
            await SeedWorkshopsAsync(db);

            await SeedBodyTypesAsync(db);
            await SeedEngineTypesAsync(db);

            await SeedServiceCategoriesAsync(db);
            await SeedServicesAsync(db);

            await SeedBrandsAndModelsAsync(db);

            await SeedWorkshopServicesAsync(db);

            await SeedAppointmentStatusesAsync(db);
            await SeedPartCategoriesAsync(db);

            await SeedPartsAsync(db);
        }

        private static async Task SeedBodyTypesAsync(KachaowAutoDbContext db)
        {
            if (await db.BodyTypes.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/bodyTypes.json");
            var bodyTypes = JsonSerializer.Deserialize<List<BodyType>>(json);

            if (bodyTypes == null || bodyTypes.Count == 0) return;

            await db.BodyTypes.AddRangeAsync(bodyTypes);
            await db.SaveChangesAsync();
        }

        private static async Task SeedEngineTypesAsync(KachaowAutoDbContext db)
        {
            if (await db.EngineTypes.AnyAsync()) return;

            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/engineTypes.json");
            var engineTypes = JsonSerializer.Deserialize<List<EngineType>>(json);

            if (engineTypes == null || engineTypes.Count == 0) return;

            await db.EngineTypes.AddRangeAsync(engineTypes);
            await db.SaveChangesAsync();
        }

        private static async Task SeedBrandsAndModelsAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/brandsModels.json");
            var brands = JsonSerializer.Deserialize<List<BrandSeed>>(json);

            if (brands == null || brands.Count == 0) return;

            var defaultBodyType = await db.BodyTypes.FirstOrDefaultAsync();
            if (defaultBodyType == null) return;

            var defaultEngineType = await db.EngineTypes.FirstOrDefaultAsync();
            if (defaultEngineType == null) return;

            foreach (var b in brands)
            {
                var brand = await db.Brands
                    .FirstOrDefaultAsync(x => x.BrandName == b.BrandName);

                if (brand == null)
                {
                    brand = new Brand
                    {
                        BrandName = b.BrandName
                    };

                    db.Brands.Add(brand);
                    await db.SaveChangesAsync();
                }

                foreach (var modelName in b.Models)
                {
                    bool exists = await db.Models.AnyAsync(m =>
                        m.ModelName == modelName && m.BrandId == brand.BrandId);

                    if (!exists)
                    {
                        db.Models.Add(new Model
                        {
                            ModelName = modelName,
                            BrandId = brand.BrandId,
                            BodyTypeId = defaultBodyType.BodyTypeId,
                            EngineTypeId = defaultEngineType.EngineTypeId
                        });
                    }
                }

                await db.SaveChangesAsync();
            }
        }
        private static async Task SeedServiceCategoriesAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/serviceCategories.json");
            var categories = JsonSerializer.Deserialize<List<ServiceCategorySeed>>(json);

            if (categories == null || categories.Count == 0) return;

            foreach (var c in categories)
            {
                if (string.IsNullOrWhiteSpace(c.Name))
                    continue;

                bool exists = await db.ServiceCategories.AnyAsync(x => x.CategoryName == c.Name);

                if (!exists)
                {
                    db.ServiceCategories.Add(new ServiceCategory
                    {
                        CategoryName = c.Name
                    });
                }
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedServicesAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/services.json");
            var services = JsonSerializer.Deserialize<List<ServiceSeed>>(json);

            if (services == null || services.Count == 0)
                return;

            foreach (var s in services)
            {
                if (string.IsNullOrWhiteSpace(s.ServiceName) || string.IsNullOrWhiteSpace(s.Category))
                    continue;

                bool exists = await db.Services.AnyAsync(x => x.ServiceName == s.ServiceName);

                if (exists)
                    continue;

                var category = await db.ServiceCategories
                    .FirstOrDefaultAsync(c => c.CategoryName == s.Category);

                if (category == null)
                    continue;

                db.Services.Add(new Service
                {
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    PriceFrom = s.PriceFrom,
                    PriceTo = s.PriceTo,
                    ServiceCategoryId = category.ServiceCategoryId
                });
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedRegionsAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/regions.json");
            var regions = JsonSerializer.Deserialize<List<RegionSeed>>(json);

            if (regions == null || regions.Count == 0) return;

            foreach (var r in regions)
            {
                if (string.IsNullOrWhiteSpace(r.RegionName))
                    continue;

                bool exists = await db.Regions.AnyAsync(x => x.RegionName == r.RegionName);

                if (!exists)
                {
                    db.Regions.Add(new Region
                    {
                        RegionName = r.RegionName
                    });
                }
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedWorkshopsAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/workshops.json");
            var workshops = JsonSerializer.Deserialize<List<WorkshopSeed>>(json);

            if (workshops == null || workshops.Count == 0) return;

            foreach (var w in workshops)
            {
                if (string.IsNullOrWhiteSpace(w.Name))
                    continue;

                bool exists = await db.Workshops.AnyAsync(x => x.Name == w.Name);

                if (exists)
                    continue;

                var region = await db.Regions.FirstOrDefaultAsync(r => r.RegionName == w.RegionName);

                if (region == null)
                    continue;

                db.Workshops.Add(new Workshop
                {
                    Name = w.Name,
                    RegionId = region.RegionId,
                    City = w.City,
                    Address = w.Address,
                    PhoneNumber = w.PhoneNumber,
                    Latitude = w.Latitude,
                    Longitude = w.Longitude,
                    IsActive = true
                });
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedWorkshopServicesAsync(KachaowAutoDbContext db)
        {
            var workshops = await db.Workshops.ToListAsync();
            var services = await db.Services.ToListAsync();

            foreach (var workshop in workshops)
            {
                foreach (var service in services)
                {
                    bool exists = await db.WorkshopServices.AnyAsync(ws =>
                        ws.WorkshopId == workshop.WorkshopId &&
                        ws.ServiceId == service.ServiceId);

                    if (!exists)
                    {
                        db.WorkshopServices.Add(new WorkshopService
                        {
                            WorkshopId = workshop.WorkshopId,
                            ServiceId = service.ServiceId
                        });
                    }
                }
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedAppointmentStatusesAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/appointmentStatuses.json");
            var statuses = JsonSerializer.Deserialize<List<AppointmentStatusSeed>>(json);

            if (statuses == null || statuses.Count == 0) return;

            foreach (var s in statuses)
            {
                if (string.IsNullOrWhiteSpace(s.StatusName))
                    continue;

                bool exists = await db.AppointmentStatuses.AnyAsync(x => x.StatusName == s.StatusName);

                if (!exists)
                {
                    db.AppointmentStatuses.Add(new AppointmentStatus
                    {
                        StatusName = s.StatusName
                    });
                }
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedPartCategoriesAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/partCategories.json");
            var categories = JsonSerializer.Deserialize<List<PartCategorySeed>>(json);

            if (categories == null || categories.Count == 0) return;

            var existingNames = await db.PartCategories
                .Select(x => x.Name.ToLower())
                .ToListAsync();

            foreach (var s in categories)
            {
                if (string.IsNullOrWhiteSpace(s.Name))
                    continue;

                if (!existingNames.Contains(s.Name.ToLower()))
                {
                    db.PartCategories.Add(new PartCategory
                    {
                        Name = s.Name
                    });
                }
            }

            await db.SaveChangesAsync();
        }
        private static async Task SeedPartsAsync(KachaowAutoDbContext db)
        {
            var json = await File.ReadAllTextAsync("../KachaowAuto.Data/Seeding/json/parts.json");
            var parts = JsonSerializer.Deserialize<List<PartSeed>>(json);

            if (parts == null || parts.Count == 0) return;

            var categories = await db.PartCategories.ToListAsync();

            foreach (var s in parts)
            {
                if (string.IsNullOrWhiteSpace(s.PartName) || string.IsNullOrWhiteSpace(s.CategoryName))
                    continue;

                var category = categories.FirstOrDefault(c => c.Name == s.CategoryName);
                if (category == null)
                    continue;

                bool exists = await db.Parts.AnyAsync(p =>
                    p.Manufacturer == s.Manufacturer &&
                    p.PartNumber == s.PartNumber);

                if (!exists)
                {
                    db.Parts.Add(new Part
                    {
                        PartName = s.PartName,
                        Manufacturer = s.Manufacturer,
                        PartNumber = s.PartNumber,
                        Description = s.Description,
                        UnitPrice = s.UnitPrice,
                        IsActive = s.IsActive,
                        PartCategoryId = category.PartCategoryId
                    });
                }
            }

            await db.SaveChangesAsync();
        }
        private class BrandSeed
        {
            public string BrandName { get; set; } = null!;
            public List<string> Models { get; set; } = new();
        }
        public class ServiceCategorySeed
        {
            public string Name { get; set; } = null!;
        }
        public class ServiceSeed
        {
            public string ServiceName { get; set; } = null!;
            public string Description { get; set; } = null!;
            public decimal? PriceFrom { get; set; }
            public decimal? PriceTo { get; set; }
            public string Category { get; set; } = null!;
        }
        public class RegionSeed
        {
            public string RegionName { get; set; } = null!;
        }
        public class WorkshopSeed
        {
            public string Name { get; set; } = null!;
            public string RegionName { get; set; } = null!;
            public string City { get; set; } = null!;
            public string Address { get; set; } = null!;
            public string? PhoneNumber { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
        }
        public class AppointmentStatusSeed
        {
            public string StatusName { get; set; } = null!;
        }
        public class PartCategorySeed
        {
            public string Name { get; set; } = null!;
        }
        public class PartSeed
        {
            public string PartName { get; set; } = null!;
            public string? Manufacturer { get; set; }
            public string? PartNumber { get; set; }
            public string? Description { get; set; }
            public decimal UnitPrice { get; set; }
            public bool IsActive { get; set; } = true;
            public string CategoryName { get; set; } = null!;
        }
    }
}




