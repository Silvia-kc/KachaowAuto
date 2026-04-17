using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.CarModels;
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
    public class CarServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCarsWithAppointmentsCount()
        {
            using var context = DbContextFactory.Create();

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

            var engineType = new EngineType
            {
                EngineTypeId = 1,
                Name = "Diesel"
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
                EngineType = engineType
            };

            var model2 = new Model
            {
                ModelId = 2,
                ModelName = "X3",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car1 = new Car
            {
                CarId = 1,
                UserId = 1,
                ModelId = 1,
                Model = model1,
                Year = 2020,
                VIN = "VIN001"
            };

            var car2 = new Car
            {
                CarId = 2,
                UserId = 2,
                ModelId = 2,
                Model = model2,
                Year = 2021,
                VIN = "VIN002"
            };

            var appointmentStatus = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            };

            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Main Workshop",
                City = "Kazanlak",
                Address = "Center",
                RegionId = 1,
                IsActive = true
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.AddRange(model1, model2);
            context.Cars.AddRange(car1, car2);
            context.AppointmentStatuses.Add(appointmentStatus);
            context.Workshops.Add(workshop);

            context.Appointments.AddRange(
                new Appointment
                {
                    AppointmentId = 1,
                    CarId = 1,
                    Car = car1,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    AppointmentStatusId = 1,
                    Status = appointmentStatus,
                    ScheduledDate = new DateTime(2026, 4, 20),
                    CreatedAt = new DateTime(2026, 4, 1)
                },
                new Appointment
                {
                    AppointmentId = 2,
                    CarId = 1,
                    Car = car1,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    AppointmentStatusId = 1,
                    Status = appointmentStatus,
                    ScheduledDate = new DateTime(2026, 4, 21),
                    CreatedAt = new DateTime(2026, 4, 2)
                },
                new Appointment
                {
                    AppointmentId = 3,
                    CarId = 2,
                    Car = car2,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    AppointmentStatusId = 1,
                    Status = appointmentStatus,
                    ScheduledDate = new DateTime(2026, 4, 22),
                    CreatedAt = new DateTime(2026, 4, 3)
                });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);

            var firstCar = result.First(x => x.CarId == 1);
            var secondCar = result.First(x => x.CarId == 2);

            Assert.Equal("BMW", firstCar.BrandName);
            Assert.Equal("X5", firstCar.ModelName);
            Assert.Equal(2, firstCar.AppointmentsCount);

            Assert.Equal("BMW", secondCar.BrandName);
            Assert.Equal("X3", secondCar.ModelName);
            Assert.Equal(1, secondCar.AppointmentsCount);
        }

        [Fact]
        public async Task GetMyCarsAsync_ShouldReturnOnlyCarsForGivenUser()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Audi" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Sedan" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Petrol" };

            var model = new Model
            {
                ModelId = 1,
                ModelName = "A6",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car1 = new Car
            {
                CarId = 1,
                UserId = 5,
                ModelId = 1,
                Model = model,
                Year = 2022,
                VIN = "AUDI001"
            };

            var car2 = new Car
            {
                CarId = 2,
                UserId = 5,
                ModelId = 1,
                Model = model,
                Year = 2023,
                VIN = "AUDI002"
            };

            var car3 = new Car
            {
                CarId = 3,
                UserId = 9,
                ModelId = 1,
                Model = model,
                Year = 2021,
                VIN = "AUDI003"
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.AddRange(car1, car2, car3);

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetMyCarsAsync(5)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.VIN == "AUDI001");
            Assert.Contains(result, x => x.VIN == "AUDI002");
            Assert.DoesNotContain(result, x => x.VIN == "AUDI003");
        }

        [Fact]
        public async Task GetMyCarsAsync_ShouldReturnNoAppointments_WhenCarHasNoAppointments()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Mercedes" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Coupe" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Diesel" };

            var model = new Model
            {
                ModelId = 1,
                ModelName = "C220",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 7,
                ModelId = 1,
                Model = model,
                Year = 2020,
                VIN = "MERC001"
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetMyCarsAsync(7)).ToList();

            Assert.Single(result);
            Assert.Equal("No appointments", result[0].LatestStatus);
        }

        [Fact]
        public async Task GetMyCarsAsync_ShouldReturnCompleted_WhenLatestAppointmentIsCompleted()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "BMW" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "SUV" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Diesel" };
            var model = new Model
            {
                ModelId = 1,
                ModelName = "X6",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 1,
                ModelId = 1,
                Model = model,
                Year = 2022,
                VIN = "BMWCOMP"
            };

            var status = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Completed"
            };

            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop",
                City = "Sofia",
                Address = "Addr",
                RegionId = 1,
                IsActive = true
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);
            context.AppointmentStatuses.Add(status);
            context.Workshops.Add(workshop);

            context.Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                Car = car,
                WorkshopId = 1,
                Workshop = workshop,
                ServiceId = 1,
                AppointmentStatusId = 1,
                Status = status,
                ScheduledDate = DateTime.Now.AddDays(-1),
                CreatedAt = DateTime.Now.AddDays(-5),
                CompletedAt = DateTime.Now
            });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetMyCarsAsync(1)).ToList();

            Assert.Single(result);
            Assert.Equal("Completed", result[0].LatestStatus);
        }

        [Fact]
        public async Task GetMyCarsAsync_ShouldReturnUpcoming_WhenLatestAppointmentIsInFuture()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Audi" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Sedan" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Hybrid" };
            var model = new Model
            {
                ModelId = 1,
                ModelName = "A4",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 2,
                ModelId = 1,
                Model = model,
                Year = 2021,
                VIN = "AUDIFUT"
            };

            var status = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            };

            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop",
                City = "Plovdiv",
                Address = "Addr",
                RegionId = 1,
                IsActive = true
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);
            context.AppointmentStatuses.Add(status);
            context.Workshops.Add(workshop);

            context.Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                Car = car,
                WorkshopId = 1,
                Workshop = workshop,
                ServiceId = 1,
                AppointmentStatusId = 1,
                Status = status,
                ScheduledDate = DateTime.Now.AddDays(3),
                CreatedAt = DateTime.Now
            });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetMyCarsAsync(2)).ToList();

            Assert.Single(result);
            Assert.Equal("Upcoming", result[0].LatestStatus);
        }

        [Fact]
        public async Task GetMyCarsAsync_ShouldReturnInProgress_WhenLatestAppointmentIsPastAndNotCompleted()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "VW" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Hatchback" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Petrol" };
            var model = new Model
            {
                ModelId = 1,
                ModelName = "Golf",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 3,
                ModelId = 1,
                Model = model,
                Year = 2019,
                VIN = "VWPROG"
            };

            var status = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            };

            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop",
                City = "Varna",
                Address = "Addr",
                RegionId = 1,
                IsActive = true
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);
            context.AppointmentStatuses.Add(status);
            context.Workshops.Add(workshop);

            context.Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                Car = car,
                WorkshopId = 1,
                Workshop = workshop,
                ServiceId = 1,
                AppointmentStatusId = 1,
                Status = status,
                ScheduledDate = DateTime.Now.AddDays(-2),
                CreatedAt = DateTime.Now.AddDays(-5),
                CompletedAt = null
            });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = (await sut.GetMyCarsAsync(3)).ToList();

            Assert.Single(result);
            Assert.Equal("In Progress", result[0].LatestStatus);
        }

        [Fact]
        public async Task GetCreatePageModelAsync_ShouldReturnAllModelsWithBrands()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Toyota" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "SUV" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Hybrid" };

            var model1 = new Model
            {
                ModelId = 1,
                ModelName = "RAV4",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var model2 = new Model
            {
                ModelId = 2,
                ModelName = "Corolla",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.AddRange(model1, model2);

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = await sut.GetCreatePageModelAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Models.Count());
        }

        [Fact]
        public async Task CreateAsync_ShouldAddCar()
        {
            using var context = DbContextFactory.Create();

            var sut = new CarService(context);

            var model = new CarCreateServiceModel
            {
                UserId = 10,
                ModelId = 2,
                Year = 2024,
                VIN = "NEWCAR001"
            };

            await sut.CreateAsync(model);

            Assert.Equal(1, await context.Cars.CountAsync());

            var car = await context.Cars.FirstAsync();
            Assert.Equal(10, car.UserId);
            Assert.Equal(2, car.ModelId);
            Assert.Equal(2024, car.Year);
            Assert.Equal("NEWCAR001", car.VIN);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnModel_WhenCarExists()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Honda" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Sedan" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Petrol" };

            var model = new Model
            {
                ModelId = 1,
                ModelName = "Civic",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 2,
                ModelId = 1,
                Year = 2018,
                VIN = "HONDA001"
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = await sut.GetEditPageModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Car.CarId);
            Assert.Equal(2, result.Car.UserId);
            Assert.Equal(1, result.Car.ModelId);
            Assert.Equal(2018, result.Car.Year);
            Assert.Equal("HONDA001", result.Car.VIN);
            Assert.Single(result.Models);
        }

        [Fact]
        public async Task GetEditPageModelAsync_ShouldReturnNull_WhenCarDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new CarService(context);

            var result = await sut.GetEditPageModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCar_WhenCarExists()
        {
            using var context = DbContextFactory.Create();

            context.Cars.Add(new Car
            {
                CarId = 1,
                UserId = 1,
                ModelId = 1,
                Year = 2015,
                VIN = "OLDVIN"
            });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var model = new CarEditServiceModel
            {
                CarId = 1,
                UserId = 7,
                ModelId = 3,
                Year = 2022,
                VIN = "NEWVIN"
            };

            await sut.UpdateAsync(model);

            var car = await context.Cars.FirstAsync();
            Assert.Equal(7, car.UserId);
            Assert.Equal(3, car.ModelId);
            Assert.Equal(2022, car.Year);
            Assert.Equal("NEWVIN", car.VIN);
        }

        [Fact]
        public async Task UpdateAsync_ShouldDoNothing_WhenCarDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new CarService(context);

            var model = new CarEditServiceModel
            {
                CarId = 999,
                UserId = 1,
                ModelId = 1,
                Year = 2020,
                VIN = "TEST"
            };

            await sut.UpdateAsync(model);

            Assert.Equal(0, await context.Cars.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCarDetails_WhenCarExists()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Ford" };
            var bodyType = new BodyType { BodyTypeId = 1, Name = "Hatchback" };
            var engineType = new EngineType { EngineTypeId = 1, Name = "Diesel" };

            var model = new Model
            {
                ModelId = 1,
                ModelName = "Focus",
                BrandId = 1,
                Brand = brand,
                BodyTypeId = 1,
                BodyType = bodyType,
                EngineTypeId = 1,
                EngineType = engineType
            };

            var car = new Car
            {
                CarId = 1,
                UserId = 4,
                ModelId = 1,
                Model = model,
                Year = 2017,
                VIN = "FORD001"
            };

            context.Brands.Add(brand);
            context.BodyTypes.Add(bodyType);
            context.EngineTypes.Add(engineType);
            context.Models.Add(model);
            context.Cars.Add(car);

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = await sut.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.CarId);
            Assert.Equal("Ford", result.BrandName);
            Assert.Equal("Focus", result.ModelName);
            Assert.Equal(2017, result.Year);
            Assert.Equal("FORD001", result.VIN);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCarDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new CarService(context);

            var result = await sut.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndRemoveCar_WhenCarExists()
        {
            using var context = DbContextFactory.Create();

            context.Cars.Add(new Car
            {
                CarId = 1,
                UserId = 1,
                ModelId = 1,
                Year = 2020,
                VIN = "DELETE001"
            });

            await context.SaveChangesAsync();

            var sut = new CarService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Cars.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCarDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new CarService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }
    }
}
