using KachaowAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(KachaowAutoDbContext db)
        {
            await db.Database.MigrateAsync();

            var engineTypes = await db.EngineTypes.ToListAsync();
            if (!await db.EngineTypes.AnyAsync())
            {
                db.EngineTypes.AddRange(
                    new EngineType { Name = "Petrol" },
                    new EngineType { Name = "Diesel" },
                    new EngineType { Name = "Hybrid" },
                    new EngineType { Name = "Electric" }
                );

                await db.SaveChangesAsync();
            }

            if (!await db.BodyTypes.AnyAsync())
            {
                db.BodyTypes.AddRange(
                    new BodyType { Name = "Sedan" },
                    new BodyType { Name = "Hatchback" },
                    new BodyType { Name = "SUV" },
                    new BodyType { Name = "Coupe" },
                    new BodyType { Name = "Cabriolet" },
                    new BodyType { Name = "Wagon" },
                    new BodyType { Name = "Combi" },
                    new BodyType { Name = "Minivan" },
                    new BodyType { Name = "Van" },
                    new BodyType { Name = "Pickup" },
                    new BodyType { Name = "Limousine" },
                    new BodyType { Name = "Microcar" }
                );

                await db.SaveChangesAsync();
            }

            if (!await db.Brands.AnyAsync())
            {
                db.Brands.AddRange(
                    new Brand { BrandName = "BMW" },
                    new Brand { BrandName = "Audi" },
                    new Brand { BrandName = "Mercedes-Benz" },
                    new Brand { BrandName = "Toyota" },
                    new Brand { BrandName = "Volkswagen" },
                    new Brand { BrandName = "Ford" },
                    new Brand { BrandName = "Honda" },
                    new Brand { BrandName = "Nissan" },
                    new Brand { BrandName = "Hyundai" },
                    new Brand { BrandName = "Kia" },
                    new Brand { BrandName = "Peugeot" },
                    new Brand { BrandName = "Renault" },
                    new Brand { BrandName = "Skoda" },
                    new Brand { BrandName = "Mazda" },
                    new Brand { BrandName = "Volvo" },
                    new Brand { BrandName = "Porsche" },
                    new Brand { BrandName = "Chevrolet" },
                    new Brand { BrandName = "Lexus" },
                    new Brand { BrandName = "Subaru" }
                );

                await db.SaveChangesAsync();
            }
            if (!await db.Models.AnyAsync())
            {
                var petrol = await db.EngineTypes.FirstAsync(e => e.Name == "Petrol");
                var diesel = await db.EngineTypes.FirstAsync(e => e.Name == "Diesel");

                var sedan = await db.BodyTypes.FirstAsync(b => b.Name == "Sedan");
                var suv = await db.BodyTypes.FirstAsync(b => b.Name == "SUV");
                var hatch = await db.BodyTypes.FirstAsync(b => b.Name == "Hatchback");
                var coupe = await db.BodyTypes.FirstAsync(b => b.Name == "Coupe");

                var bmw = await db.Brands.FirstAsync(b => b.BrandName == "BMW");
                var audi = await db.Brands.FirstAsync(b => b.BrandName == "Audi");
                var mercedes = await db.Brands.FirstAsync(b => b.BrandName == "Mercedes-Benz");
                var toyota = await db.Brands.FirstAsync(b => b.BrandName == "Toyota");
                var vw = await db.Brands.FirstAsync(b => b.BrandName == "Volkswagen");
                var ford = await db.Brands.FirstAsync(b => b.BrandName == "Ford");
                var porsche = await db.Brands.FirstAsync(b => b.BrandName == "Porsche");
                var lexus = await db.Brands.FirstAsync(b => b.BrandName == "Lexus");

                db.Models.AddRange(

                    new Model { BrandId = bmw.BrandId, ModelName = "3 Series", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.0m, HorsePower = 184, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = bmw.BrandId, ModelName = "5 Series", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 265, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = bmw.BrandId, ModelName = "X5", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 286, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = audi.BrandId, ModelName = "A4", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.0m, HorsePower = 190, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = audi.BrandId, ModelName = "A6", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 245, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = audi.BrandId, ModelName = "Q7", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 272, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = mercedes.BrandId, ModelName = "C-Class", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.0m, HorsePower = 204, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = mercedes.BrandId, ModelName = "E-Class", EngineTypeId = diesel.EngineTypeId, EngineVolume = 2.0m, HorsePower = 194, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = mercedes.BrandId, ModelName = "GLE", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 330, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = toyota.BrandId, ModelName = "Corolla", EngineTypeId = petrol.EngineTypeId, EngineVolume = 1.8m, HorsePower = 140, BodyTypeId = hatch.BodyTypeId },
                    new Model { BrandId = toyota.BrandId, ModelName = "Camry", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.5m, HorsePower = 178, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = toyota.BrandId, ModelName = "RAV4", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.5m, HorsePower = 222, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = vw.BrandId, ModelName = "Golf", EngineTypeId = petrol.EngineTypeId, EngineVolume = 1.5m, HorsePower = 150, BodyTypeId = hatch.BodyTypeId },
                    new Model { BrandId = vw.BrandId, ModelName = "Passat", EngineTypeId = diesel.EngineTypeId, EngineVolume = 2.0m, HorsePower = 200, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = vw.BrandId, ModelName = "Touareg", EngineTypeId = diesel.EngineTypeId, EngineVolume = 3.0m, HorsePower = 286, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = ford.BrandId, ModelName = "Focus", EngineTypeId = petrol.EngineTypeId, EngineVolume = 1.5m, HorsePower = 150, BodyTypeId = hatch.BodyTypeId },
                    new Model { BrandId = ford.BrandId, ModelName = "Mondeo", EngineTypeId = diesel.EngineTypeId, EngineVolume = 2.0m, HorsePower = 190, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = ford.BrandId, ModelName = "Kuga", EngineTypeId = diesel.EngineTypeId, EngineVolume = 2.0m, HorsePower = 180, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = porsche.BrandId, ModelName = "911", EngineTypeId = petrol.EngineTypeId, EngineVolume = 3.0m, HorsePower = 385, BodyTypeId = coupe.BodyTypeId },
                    new Model { BrandId = porsche.BrandId, ModelName = "Cayenne", EngineTypeId = petrol.EngineTypeId, EngineVolume = 3.0m, HorsePower = 340, BodyTypeId = suv.BodyTypeId },

                    new Model { BrandId = lexus.BrandId, ModelName = "IS", EngineTypeId = petrol.EngineTypeId, EngineVolume = 2.0m, HorsePower = 241, BodyTypeId = sedan.BodyTypeId },
                    new Model { BrandId = lexus.BrandId, ModelName = "RX", EngineTypeId = petrol.EngineTypeId, EngineVolume = 3.5m, HorsePower = 295, BodyTypeId = suv.BodyTypeId }
                );

                await db.SaveChangesAsync();
            }

        }

    }

}
