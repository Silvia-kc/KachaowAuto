using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.CarModels;
using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Core.Implementations
{
    public class CarService : ICarService
    {
        private readonly KachaowAutoDbContext context;

        public CarService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<CarListServiceModel>> GetAllAsync()
        {
            return await context.Cars
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Appointments)
                .Select(c => new CarListServiceModel
                {
                    CarId = c.CarId,
                    BrandName = c.Model.Brand.BrandName,
                    ModelName = c.Model.ModelName,
                    Year = c.Year,
                    VIN = c.VIN,
                    AppointmentsCount = c.Appointments.Count
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MyCarServiceModel>> GetMyCarsAsync(int userId)
        {
            return await context.Cars
                .Where(c => c.UserId == userId)
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Appointments)
                .Select(c => new MyCarServiceModel
                {
                    CarId = c.CarId,
                    BrandName = c.Model.Brand.BrandName,
                    ModelName = c.Model.ModelName,
                    Year = c.Year,
                    VIN = c.VIN,
                    LatestStatus = c.Appointments
                        .OrderByDescending(a => a.ScheduledDate)
                        .Select(a =>
                            a.CompletedAt != null
                                ? "Completed"
                                : a.ScheduledDate > DateTime.Now
                                    ? "Upcoming"
                                    : "In Progress")
                        .FirstOrDefault() ?? "No appointments"
                })
                .ToListAsync();
        }

        public async Task<CarCreatePageServiceModel> GetCreatePageModelAsync()
        {
            return new CarCreatePageServiceModel
            {
                Models = await context.Models
                    .Include(m => m.Brand)
                    .ToListAsync()
            };
        }

        public async Task CreateAsync(CarCreateServiceModel model)
        {
            var car = new Car
            {
                UserId = model.UserId,
                ModelId = model.ModelId,
                Year = model.Year,
                VIN = model.VIN
            };

            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
        }

        public async Task<CarEditPageServiceModel?> GetEditPageModelAsync(int id)
        {
            var car = await context.Cars
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null)
            {
                return null;
            }

            return new CarEditPageServiceModel
            {
                Car = new CarEditServiceModel
                {
                    CarId = car.CarId,
                    UserId = car.UserId,
                    ModelId = car.ModelId,
                    Year = car.Year,
                    VIN = car.VIN
                },
                Models = await context.Models
                    .Include(m => m.Brand)
                    .ToListAsync()
            };
        }

        public async Task UpdateAsync(CarEditServiceModel model)
        {
            var car = await context.Cars
                .FirstOrDefaultAsync(c => c.CarId == model.CarId);

            if (car == null)
            {
                return;
            }

            car.UserId = model.UserId;
            car.ModelId = model.ModelId;
            car.Year = model.Year;
            car.VIN = model.VIN;

            await context.SaveChangesAsync();
        }

        public async Task<CarDetailsServiceModel?> GetByIdAsync(int id)
        {
            return await context.Cars
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Where(c => c.CarId == id)
                .Select(c => new CarDetailsServiceModel
                {
                    CarId = c.CarId,
                    BrandName = c.Model.Brand.BrandName,
                    ModelName = c.Model.ModelName,
                    Year = c.Year,
                    VIN = c.VIN
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await context.Cars
                .FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null)
            {
                return false;
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
