using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.Appointment;
using KachaowAuto.Data.Models;
using KachaowAuto.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KachaowAuto.Tests.Services
{
    public class AppointmentServiceTests
    {
        [Fact]
        public async Task GetAllForIndexAsync_ShouldReturnAllAppointments_WhenNoFilter()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "BMW" };
            var model = new Model { ModelId = 1, ModelName = "320d", BrandId = 1, Brand = brand };
            var service = new Service { ServiceId = 1, ServiceName = "Oil Change" };
            var status = new AppointmentStatus { AppointmentStatusId = 1, StatusName = "Pending" };
            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Kachaow Auto",
                City = "Kazanlak",
                Address = "Center",
                RegionId = 1,
                IsActive = true
            };
            var car = new Car
            {
                CarId = 1,
                ModelId = 1,
                Model = model,
                VIN = "VIN123456789",
                Year = 2020,
                UserId = 1
            };

            var appointment = new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                Car = car,
                WorkshopId = 1,
                Workshop = workshop,
                ServiceId = 1,
                Service = service,
                AppointmentStatusId = 1,
                Status = status,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 10),
                ProblemDescription = "Engine noise"
            };

            context.Brands.Add(brand);
            context.Models.Add(model);
            context.Services.Add(service);
            context.AppointmentStatuses.Add(status);
            context.Workshops.Add(workshop);
            context.Cars.Add(car);
            context.Appointments.Add(appointment);

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var filter = new AppointmentIndexFilterServiceModel();

            var result = await sut.GetAllForIndexAsync(filter);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].AppointmentId);
            Assert.Equal("320d", result[0].CarModelName);
            Assert.Equal("VIN123456789", result[0].VIN);
            Assert.Equal("Kachaow Auto", result[0].WorkshopName);
            Assert.Equal("Oil Change", result[0].ServiceName);
            Assert.Equal("Pending", result[0].StatusName);
            Assert.Equal("Engine noise", result[0].ProblemDescription);
        }

        [Fact]
        public async Task GetAllForIndexAsync_ShouldFilterByStatusId()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Audi" };
            var model = new Model { ModelId = 1, ModelName = "A4", BrandId = 1, Brand = brand };
            var service = new Service { ServiceId = 1, ServiceName = "Diagnostics" };
            var pending = new AppointmentStatus { AppointmentStatusId = 1, StatusName = "Pending" };
            var completed = new AppointmentStatus { AppointmentStatusId = 2, StatusName = "Completed" };
            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop 1",
                City = "Sofia",
                Address = "Street 1",
                RegionId = 1,
                IsActive = true
            };
            var car = new Car
            {
                CarId = 1,
                ModelId = 1,
                Model = model,
                VIN = "WAUZZZ123",
                Year = 2021,
                UserId = 1
            };

            context.AddRange(brand, model, service, pending, completed, workshop, car);

            context.Appointments.AddRange(
                new Appointment
                {
                    AppointmentId = 1,
                    CarId = 1,
                    Car = car,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    Service = service,
                    AppointmentStatusId = 1,
                    Status = pending,
                    ScheduledDate = new DateTime(2026, 4, 18),
                    CreatedAt = new DateTime(2026, 4, 10)
                },
                new Appointment
                {
                    AppointmentId = 2,
                    CarId = 1,
                    Car = car,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    Service = service,
                    AppointmentStatusId = 2,
                    Status = completed,
                    ScheduledDate = new DateTime(2026, 4, 19),
                    CreatedAt = new DateTime(2026, 4, 11)
                });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var filter = new AppointmentIndexFilterServiceModel
            {
                StatusId = 2
            };

            var result = await sut.GetAllForIndexAsync(filter);

            Assert.Single(result);
            Assert.Equal(2, result[0].AppointmentId);
            Assert.Equal("Completed", result[0].StatusName);
        }

        [Fact]
        public async Task GetAllForIndexAsync_ShouldFilterBySearchTerm()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "Mercedes" };
            var model1 = new Model { ModelId = 1, ModelName = "C220", BrandId = 1, Brand = brand };
            var model2 = new Model { ModelId = 2, ModelName = "E200", BrandId = 1, Brand = brand };
            var service = new Service { ServiceId = 1, ServiceName = "Inspection" };
            var status = new AppointmentStatus { AppointmentStatusId = 1, StatusName = "Pending" };
            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Workshop",
                City = "Plovdiv",
                Address = "Addr",
                RegionId = 1,
                IsActive = true
            };

            var car1 = new Car
            {
                CarId = 1,
                ModelId = 1,
                Model = model1,
                VIN = "VIN-C220-111",
                Year = 2020,
                UserId = 1
            };

            var car2 = new Car
            {
                CarId = 2,
                ModelId = 2,
                Model = model2,
                VIN = "VIN-E200-222",
                Year = 2021,
                UserId = 2
            };

            context.AddRange(brand, model1, model2, service, status, workshop, car1, car2);

            context.Appointments.AddRange(
                new Appointment
                {
                    AppointmentId = 1,
                    CarId = 1,
                    Car = car1,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    Service = service,
                    AppointmentStatusId = 1,
                    Status = status,
                    ScheduledDate = new DateTime(2026, 4, 20),
                    CreatedAt = new DateTime(2026, 4, 10)
                },
                new Appointment
                {
                    AppointmentId = 2,
                    CarId = 2,
                    Car = car2,
                    WorkshopId = 1,
                    Workshop = workshop,
                    ServiceId = 1,
                    Service = service,
                    AppointmentStatusId = 1,
                    Status = status,
                    ScheduledDate = new DateTime(2026, 4, 21),
                    CreatedAt = new DateTime(2026, 4, 11)
                });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var filter = new AppointmentIndexFilterServiceModel
            {
                SearchTerm = "c220"
            };

            var result = await sut.GetAllForIndexAsync(filter);

            Assert.Single(result);
            Assert.Equal("C220", result[0].CarModelName);
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnAppointmentDetails_WhenAppointmentExists()
        {
            using var context = DbContextFactory.Create();

            var brand = new Brand { BrandId = 1, BrandName = "BMW" };
            var model = new Model { ModelId = 1, ModelName = "X5", BrandId = 1, Brand = brand };
            var service = new Service { ServiceId = 1, ServiceName = "Brake Service" };
            var status = new AppointmentStatus { AppointmentStatusId = 1, StatusName = "Pending" };
            var workshop = new Workshop
            {
                WorkshopId = 1,
                Name = "Main Workshop",
                City = "Stara Zagora",
                Address = "Center",
                RegionId = 1,
                IsActive = true
            };
            var car = new Car
            {
                CarId = 1,
                ModelId = 1,
                Model = model,
                VIN = "BMWX5VIN",
                Year = 2022,
                UserId = 1
            };

            var appointment = new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                Car = car,
                WorkshopId = 1,
                Workshop = workshop,
                ServiceId = 1,
                Service = service,
                AppointmentStatusId = 1,
                Status = status,
                ScheduledDate = new DateTime(2026, 5, 1),
                CreatedAt = new DateTime(2026, 4, 1),
                ProblemDescription = "Brake noise"
            };

            context.AddRange(brand, model, service, status, workshop, car, appointment);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.GetDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.AppointmentId);
            Assert.Equal(1, result.CarId);
            Assert.Equal("X5", result.CarModelName);
            Assert.Equal("BMWX5VIN", result.VIN);
            Assert.Equal(2022, result.Year);
            Assert.Equal("Main Workshop", result.WorkshopName);
            Assert.Equal("Brake Service", result.ServiceName);
            Assert.Equal("Pending", result.StatusName);
            Assert.Equal("Brake noise", result.ProblemDescription);
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnNull_WhenAppointmentDoesNotExist()
        {
            using var context = DbContextFactory.Create();
            var sut = new AppointmentService(context);

            var result = await sut.GetDetailsAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAppointmentAndCar_WhenPendingStatusExists()
        {
            using var context = DbContextFactory.Create();

            var pendingStatus = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            };

            context.AppointmentStatuses.Add(pendingStatus);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var model = new AppointmentCreateServiceModel
            {
                UserId = 5,
                ModelId = 2,
                Year = 2021,
                VIN = "NEWVIN123",
                ServiceId = 3,
                WorkshopId = 4,
                ScheduledDate = new DateTime(2026, 5, 10),
                ProblemDescription = "Oil leak"
            };

            var result = await sut.CreateAsync(model);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);

            Assert.Equal(1, await context.Cars.CountAsync());
            Assert.Equal(1, await context.Appointments.CountAsync());

            var car = await context.Cars.FirstAsync();
            Assert.Equal(5, car.UserId);
            Assert.Equal(2, car.ModelId);
            Assert.Equal(2021, car.Year);
            Assert.Equal("NEWVIN123", car.VIN);

            var appointment = await context.Appointments.FirstAsync();
            Assert.Equal(3, appointment.ServiceId);
            Assert.Equal(4, appointment.WorkshopId);
            Assert.Equal(1, appointment.AppointmentStatusId);
            Assert.Equal("Oil leak", appointment.ProblemDescription);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenPendingStatusDoesNotExist()
        {
            using var context = DbContextFactory.Create();
            var sut = new AppointmentService(context);

            var model = new AppointmentCreateServiceModel
            {
                UserId = 1,
                ModelId = 1,
                Year = 2020,
                VIN = "VIN001",
                ServiceId = 1,
                WorkshopId = 1,
                ScheduledDate = new DateTime(2026, 5, 12),
                ProblemDescription = "Test"
            };

            var result = await sut.CreateAsync(model);

            Assert.False(result.Success);
            Assert.Equal("Appointment status 'Pending' was not found.", result.ErrorMessage);
            Assert.Equal(0, await context.Cars.CountAsync());
            Assert.Equal(0, await context.Appointments.CountAsync());
        }

        [Fact]
        public async Task EditAsync_ShouldUpdateAppointment_WhenAppointmentExists()
        {
            using var context = DbContextFactory.Create();

            var appointment = new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                WorkshopId = 1,
                ServiceId = 1,
                AppointmentStatusId = 1,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 1),
                ProblemDescription = "Old problem"
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var model = new AppointmentEditServiceModel
            {
                AppointmentId = 1,
                CarId = 2,
                WorkshopId = 3,
                ServiceId = 4,
                AppointmentStatusId = 2,
                ScheduledDate = new DateTime(2026, 4, 30),
                ProblemDescription = "New problem",
                CompletedAt = new DateTime(2026, 5, 1)
            };

            var result = await sut.EditAsync(model);

            Assert.True(result);

            var updated = await context.Appointments.FirstAsync();
            Assert.Equal(2, updated.CarId);
            Assert.Equal(3, updated.WorkshopId);
            Assert.Equal(4, updated.ServiceId);
            Assert.Equal(2, updated.AppointmentStatusId);
            Assert.Equal(new DateTime(2026, 4, 30), updated.ScheduledDate);
            Assert.Equal("New problem", updated.ProblemDescription);
            Assert.Equal(new DateTime(2026, 5, 1), updated.CompletedAt);
        }

        [Fact]
        public async Task EditAsync_ShouldReturnFalse_WhenAppointmentDoesNotExist()
        {
            using var context = DbContextFactory.Create();
            var sut = new AppointmentService(context);

            var model = new AppointmentEditServiceModel
            {
                AppointmentId = 999
            };

            var result = await sut.EditAsync(model);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveAppointment_WhenAppointmentExists()
        {
            using var context = DbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                WorkshopId = 1,
                ServiceId = 1,
                AppointmentStatusId = 1,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 1)
            });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.DeleteAsync(1);

            Assert.True(result);
            Assert.Equal(0, await context.Appointments.CountAsync());
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenAppointmentDoesNotExist()
        {
            using var context = DbContextFactory.Create();
            var sut = new AppointmentService(context);

            var result = await sut.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldSetCompletedAt_WhenStatusIsCompleted()
        {
            using var context = DbContextFactory.Create();

            var appointment = new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                WorkshopId = 1,
                ServiceId = 1,
                AppointmentStatusId = 1,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 1)
            };

            var completedStatus = new AppointmentStatus
            {
                AppointmentStatusId = 2,
                StatusName = "Completed"
            };

            context.Appointments.Add(appointment);
            context.AppointmentStatuses.Add(completedStatus);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.ChangeStatusAsync(1, 2);

            Assert.True(result);

            var updated = await context.Appointments.FirstAsync();
            Assert.Equal(2, updated.AppointmentStatusId);
            Assert.NotNull(updated.CompletedAt);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldClearCompletedAt_WhenStatusIsNotCompleted()
        {
            using var context = DbContextFactory.Create();

            var appointment = new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                WorkshopId = 1,
                ServiceId = 1,
                AppointmentStatusId = 2,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 1),
                CompletedAt = new DateTime(2026, 4, 21)
            };

            var pendingStatus = new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            };

            context.Appointments.Add(appointment);
            context.AppointmentStatuses.Add(pendingStatus);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.ChangeStatusAsync(1, 1);

            Assert.True(result);

            var updated = await context.Appointments.FirstAsync();
            Assert.Equal(1, updated.AppointmentStatusId);
            Assert.Null(updated.CompletedAt);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldReturnFalse_WhenAppointmentDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            context.AppointmentStatuses.Add(new AppointmentStatus
            {
                AppointmentStatusId = 1,
                StatusName = "Pending"
            });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.ChangeStatusAsync(999, 1);

            Assert.False(result);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldReturnFalse_WhenStatusDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                AppointmentId = 1,
                CarId = 1,
                WorkshopId = 1,
                ServiceId = 1,
                AppointmentStatusId = 1,
                ScheduledDate = new DateTime(2026, 4, 20),
                CreatedAt = new DateTime(2026, 4, 1)
            });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.ChangeStatusAsync(1, 999);

            Assert.False(result);
        }

        [Fact]
        public async Task GetModelsByBrandAsync_ShouldReturnOnlyModelsForGivenBrand()
        {
            using var context = DbContextFactory.Create();

            var brand1 = new Brand { BrandId = 1, BrandName = "BMW" };
            var brand2 = new Brand { BrandId = 2, BrandName = "Audi" };

            var model1 = new Model { ModelId = 1, ModelName = "X3", BrandId = 1, Brand = brand1 };
            var model2 = new Model { ModelId = 2, ModelName = "X5", BrandId = 1, Brand = brand1 };
            var model3 = new Model { ModelId = 3, ModelName = "A6", BrandId = 2, Brand = brand2 };

            context.Brands.AddRange(brand1, brand2);
            context.Models.AddRange(model1, model2, model3);
            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.GetModelsByBrandAsync(1);

            Assert.Equal(2, result.Count);
            Assert.All(result, x => Assert.Contains(x.Name, new[] { "X3", "X5" }));
            Assert.DoesNotContain(result, x => x.Name == "A6");
        }

        [Fact]
        public async Task GetStatusesAsync_ShouldReturnStatusesOrderedById()
        {
            using var context = DbContextFactory.Create();

            context.AppointmentStatuses.AddRange(
                new AppointmentStatus { AppointmentStatusId = 5, StatusName = "Completed" },
                new AppointmentStatus { AppointmentStatusId = 1, StatusName = "Pending" },
                new AppointmentStatus { AppointmentStatusId = 3, StatusName = "In Progress" });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.GetStatusesAsync();

            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].AppointmentStatusId);
            Assert.Equal(3, result[1].AppointmentStatusId);
            Assert.Equal(5, result[2].AppointmentStatusId);
        }

        [Fact]
        public async Task GetServicesAsync_ShouldReturnServicesOrderedByName()
        {
            using var context = DbContextFactory.Create();

            context.Services.AddRange(
                new Service { ServiceId = 1, ServiceName = "Wheel Alignment" },
                new Service { ServiceId = 2, ServiceName = "Brake Repair" },
                new Service { ServiceId = 3, ServiceName = "Oil Change" });

            await context.SaveChangesAsync();

            var sut = new AppointmentService(context);

            var result = await sut.GetServicesAsync();

            Assert.Equal(3, result.Count);
            Assert.Equal("Brake Repair", result[0].ServiceName);
            Assert.Equal("Oil Change", result[1].ServiceName);
            Assert.Equal("Wheel Alignment", result[2].ServiceName);
        }
    }
}