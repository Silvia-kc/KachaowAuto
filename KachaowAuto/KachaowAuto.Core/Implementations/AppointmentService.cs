using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models.Appointment;
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
    public class AppointmentService : IAppointmentService
    {
        private readonly KachaowAutoDbContext context;

        public AppointmentService(KachaowAutoDbContext _context)
        {
            context = _context;
        }

        public async Task<List<AppointmentListServiceModel>> GetAllForIndexAsync(int? statusId)
        {
            var query = context.Appointments
                .Include(a => a.Car)
                    .ThenInclude(c => c.Model)
                .Include(a => a.Workshop)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .AsQueryable();

            if (statusId.HasValue)
            {
                query = query.Where(a => a.AppointmentStatusId == statusId.Value);
            }

            return await query
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AppointmentListServiceModel
                {
                    AppointmentId = a.AppointmentId,
                    CarModelName = a.Car.Model.ModelName,
                    VIN = a.Car.VIN,
                    WorkshopName = a.Workshop.Name,
                    ServiceName = a.Service.ServiceName,
                    StatusName = a.Status.StatusName,
                    AppointmentStatusId = a.AppointmentStatusId,
                    ScheduledDate = a.ScheduledDate,
                    CreatedAt = a.CreatedAt,
                    CompletedAt = a.CompletedAt,
                    ProblemDescription = a.ProblemDescription
                })
                .ToListAsync();
        }

        public async Task<List<AppointmentStatus>> GetStatusesAsync()
        {
            return await context.AppointmentStatuses.ToListAsync();
        }

        public async Task<AppointmentDetailsServiceModel?> GetDetailsAsync(int id)
        {
            return await context.Appointments
                .Include(a => a.Car)
                    .ThenInclude(c => c.Model)
                .Include(a => a.Workshop)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .Where(a => a.AppointmentId == id)
                .Select(a => new AppointmentDetailsServiceModel
                {
                    AppointmentId = a.AppointmentId,
                    CarId = a.CarId,
                    CarModelName = a.Car.Model.ModelName,
                    VIN = a.Car.VIN,
                    Year = a.Car.Year,
                    WorkshopId = a.WorkshopId,
                    WorkshopName = a.Workshop.Name,
                    ServiceId = a.ServiceId,
                    ServiceName = a.Service.ServiceName,
                    AppointmentStatusId = a.AppointmentStatusId,
                    StatusName = a.Status.StatusName,
                    ScheduledDate = a.ScheduledDate,
                    CreatedAt = a.CreatedAt,
                    CompletedAt = a.CompletedAt,
                    ProblemDescription = a.ProblemDescription
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AppointmentCreatePageServiceModel> GetCreatePageDataAsync(int? brandId = null)
        {
            var result = new AppointmentCreatePageServiceModel
            {
                BrandsCount = await context.Brands.CountAsync(),
                ModelsCount = await context.Models.CountAsync(),
                ServicesCount = await context.Services.CountAsync(),
                WorkshopsCount = await context.Workshops.CountAsync(),

                Brands = await context.Brands
                    .OrderBy(b => b.BrandName)
                    .ToListAsync(),

                Services = await context.Services
                    .OrderBy(s => s.ServiceName)
                    .ToListAsync(),

                Workshops = await context.Workshops
                    .Include(w => w.Region)
                    .OrderBy(w => w.Name)
                    .ToListAsync(),

                EngineTypes = await context.EngineTypes
                    .OrderBy(e => e.Name)
                    .ToListAsync(),

                BodyTypes = await context.BodyTypes
                    .OrderBy(b => b.Name)
                    .ToListAsync()
            };

            if (brandId.HasValue)
            {
                result.Models = await context.Models
                    .Where(m => m.BrandId == brandId.Value)
                    .OrderBy(m => m.ModelName)
                    .ToListAsync();
            }

            return result;
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateAsync(AppointmentCreateServiceModel model)
        {
            var pendingStatus = await context.AppointmentStatuses
                .FirstOrDefaultAsync(s => s.StatusName == "Pending");

            if (pendingStatus == null)
            {
                return (false, "Appointment status 'Pending' was not found.");
            }

            var car = new Car
            {
                UserId = model.UserId,
                ModelId = model.ModelId,
                Year = model.Year,
                VIN = model.VIN
            };

            context.Cars.Add(car);

            var appointment = new Appointment
            {
                Car = car,
                ServiceId = model.ServiceId,
                WorkshopId = model.WorkshopId,
                AppointmentStatusId = pendingStatus.AppointmentStatusId,
                CreatedAt = DateTime.UtcNow,
                ScheduledDate = model.ScheduledDate,
                ProblemDescription = model.ProblemDescription
            };

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<AppointmentEditPageServiceModel?> GetEditPageDataAsync(int id)
        {
            var appointment = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentEditPageServiceModel
            {
                Appointment = new AppointmentEditServiceModel
                {
                    AppointmentId = appointment.AppointmentId,
                    CarId = appointment.CarId,
                    WorkshopId = appointment.WorkshopId,
                    ServiceId = appointment.ServiceId,
                    AppointmentStatusId = appointment.AppointmentStatusId,
                    ScheduledDate = appointment.ScheduledDate,
                    ProblemDescription = appointment.ProblemDescription,
                    CompletedAt = appointment.CompletedAt
                },
                Cars = await context.Cars
                    .Include(c => c.Model)
                    .OrderBy(c => c.VIN)
                    .ToListAsync(),
                Workshops = await context.Workshops
                    .OrderBy(w => w.Name)
                    .ToListAsync(),
                Services = await context.Services
                    .OrderBy(s => s.ServiceName)
                    .ToListAsync(),
                Statuses = await context.AppointmentStatuses
                    .OrderBy(s => s.AppointmentStatusId)
                    .ToListAsync()
            };
        }

        public async Task<bool> EditAsync(AppointmentEditServiceModel model)
        {
            var appointment = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == model.AppointmentId);

            if (appointment == null)
            {
                return false;
            }

            appointment.CarId = model.CarId;
            appointment.WorkshopId = model.WorkshopId;
            appointment.ServiceId = model.ServiceId;
            appointment.AppointmentStatusId = model.AppointmentStatusId;
            appointment.ScheduledDate = model.ScheduledDate;
            appointment.ProblemDescription = model.ProblemDescription;
            appointment.CompletedAt = model.CompletedAt;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<AppointmentDetailsServiceModel?> GetDeleteDataAsync(int id)
        {
            return await GetDetailsAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return false;
            }

            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int id, int statusId)
        {
            var appointment = await context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return false;
            }

            var status = await context.AppointmentStatuses
                .FirstOrDefaultAsync(s => s.AppointmentStatusId == statusId);

            if (status == null)
            {
                return false;
            }

            appointment.AppointmentStatusId = statusId;

            if (status.StatusName == "Completed")
            {
                appointment.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                appointment.CompletedAt = null;
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ModelLookupServiceModel>> GetModelsByBrandAsync(int brandId)
        {
            return await context.Models
                .Where(m => m.BrandId == brandId)
                .OrderBy(m => m.ModelName)
                .Select(m => new ModelLookupServiceModel
                {
                    Id = m.ModelId,
                    Name = m.ModelName
                })
                .ToListAsync();
        }
    }
}
