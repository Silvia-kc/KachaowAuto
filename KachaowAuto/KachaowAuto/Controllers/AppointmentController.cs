using KachaowAuto.Data;
using KachaowAuto.Data.Models;
using KachaowAuto.ViewModels;
using KachaowAuto.ViewModels.Appointment;
using KachaowAuto.Core.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KachaowAuto.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentController(
            IAppointmentService _appointmentService,
            UserManager<ApplicationUser> _userManager)
        {
            appointmentService = _appointmentService;
            userManager = _userManager;
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Index(int? statusId)
        {
            ViewBag.Statuses = await appointmentService.GetStatusesAsync();
            ViewBag.SelectedStatusId = statusId;

            var serviceModels = await appointmentService.GetAllForIndexAsync(statusId);

            var viewModels = serviceModels.Select(a => new AppointmentIndexViewModel
            {
                AppointmentId = a.AppointmentId,
                CarModelName = a.CarModelName,
                VIN = a.VIN,
                WorkshopName = a.WorkshopName,
                ServiceName = a.ServiceName,
                StatusName = a.StatusName,
                ScheduledDate = a.ScheduledDate,
                CreatedAt = a.CreatedAt,
                CompletedAt = a.CompletedAt,
                ProblemDescription = a.ProblemDescription
            }).ToList();

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var serviceModel = await appointmentService.GetDetailsAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new AppointmentDetailsViewModel
            {
                AppointmentId = serviceModel.AppointmentId,
                CarModelName = serviceModel.CarModelName,
                VIN = serviceModel.VIN,
                Year = serviceModel.Year,
                WorkshopName = serviceModel.WorkshopName,
                ServiceName = serviceModel.ServiceName,
                StatusName = serviceModel.StatusName,
                ScheduledDate = serviceModel.ScheduledDate,
                CreatedAt = serviceModel.CreatedAt,
                CompletedAt = serviceModel.CompletedAt,
                ProblemDescription = serviceModel.ProblemDescription
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create()
        {
            var data = await appointmentService.GetCreatePageDataAsync();

            ViewBag.BrandsCount = data.BrandsCount;
            ViewBag.ModelsCount = data.ModelsCount;
            ViewBag.ServicesCount = data.ServicesCount;
            ViewBag.WorkshopsCount = data.WorkshopsCount;

            ViewBag.Brands = data.Brands;
            ViewBag.Models = data.Models;
            ViewBag.Services = data.Services;
            ViewBag.Workshops = data.Workshops;
            ViewBag.EngineTypes = data.EngineTypes;
            ViewBag.BodyTypes = data.BodyTypes;

            return View(new BookAppointmentViewModel
            {
                Year = DateTime.Now.Year,
                ScheduledDate = DateTime.Now.AddDays(1)
            });
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookAppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var pageData = await appointmentService.GetCreatePageDataAsync(viewModel.BrandId);

                ViewBag.BrandsCount = pageData.BrandsCount;
                ViewBag.ModelsCount = pageData.ModelsCount;
                ViewBag.ServicesCount = pageData.ServicesCount;
                ViewBag.WorkshopsCount = pageData.WorkshopsCount;

                ViewBag.Brands = pageData.Brands;
                ViewBag.Models = pageData.Models;
                ViewBag.Services = pageData.Services;
                ViewBag.Workshops = pageData.Workshops;
                ViewBag.EngineTypes = pageData.EngineTypes;
                ViewBag.BodyTypes = pageData.BodyTypes;

                return View(viewModel);
            }

            var userIdStr = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userIdStr))
            {
                return Forbid();
            }

            var serviceModel = new AppointmentCreateServiceModel
            {
                BrandId = viewModel.BrandId,
                ModelId = viewModel.ModelId,
                Year = viewModel.Year,
                VIN = viewModel.VIN,
                ServiceId = viewModel.ServiceId,
                WorkshopId = viewModel.WorkshopId,
                ScheduledDate = viewModel.ScheduledDate,
                ProblemDescription = viewModel.ProblemDescription,
                UserId = int.Parse(userIdStr)
            };

            var result = await appointmentService.CreateAsync(serviceModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage!);

                var pageData = await appointmentService.GetCreatePageDataAsync(viewModel.BrandId);

                ViewBag.BrandsCount = pageData.BrandsCount;
                ViewBag.ModelsCount = pageData.ModelsCount;
                ViewBag.ServicesCount = pageData.ServicesCount;
                ViewBag.WorkshopsCount = pageData.WorkshopsCount;

                ViewBag.Brands = pageData.Brands;
                ViewBag.Models = pageData.Models;
                ViewBag.Services = pageData.Services;
                ViewBag.Workshops = pageData.Workshops;
                ViewBag.EngineTypes = pageData.EngineTypes;
                ViewBag.BodyTypes = pageData.BodyTypes;

                return View(viewModel);
            }

            return RedirectToAction("Client", "Dashboard");
        }

        [Authorize(Roles = "Admin,Mechanic")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pageData = await appointmentService.GetEditPageDataAsync(id);

            if (pageData == null)
            {
                return NotFound();
            }

            ViewBag.Cars = pageData.Cars;
            ViewBag.Workshops = pageData.Workshops;
            ViewBag.Services = pageData.Services;
            ViewBag.Statuses = pageData.Statuses;

            var viewModel = new AppointmentEditViewModel
            {
                AppointmentId = pageData.Appointment.AppointmentId,
                CarId = pageData.Appointment.CarId,
                WorkshopId = pageData.Appointment.WorkshopId,
                ServiceId = pageData.Appointment.ServiceId,
                AppointmentStatusId = pageData.Appointment.AppointmentStatusId,
                ScheduledDate = pageData.Appointment.ScheduledDate,
                ProblemDescription = pageData.Appointment.ProblemDescription,
                CompletedAt = pageData.Appointment.CompletedAt
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Mechanic")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var pageData = await appointmentService.GetEditPageDataAsync(viewModel.AppointmentId);

                if (pageData != null)
                {
                    ViewBag.Cars = pageData.Cars;
                    ViewBag.Workshops = pageData.Workshops;
                    ViewBag.Services = pageData.Services;
                    ViewBag.Statuses = pageData.Statuses;
                }

                return View(viewModel);
            }

            var serviceModel = new AppointmentEditServiceModel
            {
                AppointmentId = viewModel.AppointmentId,
                CarId = viewModel.CarId,
                WorkshopId = viewModel.WorkshopId,
                ServiceId = viewModel.ServiceId,
                AppointmentStatusId = viewModel.AppointmentStatusId,
                ScheduledDate = viewModel.ScheduledDate,
                ProblemDescription = viewModel.ProblemDescription,
                CompletedAt = viewModel.CompletedAt
            };

            var success = await appointmentService.EditAsync(serviceModel);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Mechanic")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceModel = await appointmentService.GetDeleteDataAsync(id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            var viewModel = new AppointmentDetailsViewModel
            {
                AppointmentId = serviceModel.AppointmentId,
                CarModelName = serviceModel.CarModelName,
                VIN = serviceModel.VIN,
                Year = serviceModel.Year,
                WorkshopName = serviceModel.WorkshopName,
                ServiceName = serviceModel.ServiceName,
                StatusName = serviceModel.StatusName,
                ScheduledDate = serviceModel.ScheduledDate,
                CreatedAt = serviceModel.CreatedAt,
                CompletedAt = serviceModel.CompletedAt,
                ProblemDescription = serviceModel.ProblemDescription
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin,Mechanic")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await appointmentService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Mechanic")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, int statusId)
        {
            var success = await appointmentService.ChangeStatusAsync(id, statusId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> GetModelsByBrand(int brandId)
        {
            var serviceModels = await appointmentService.GetModelsByBrandAsync(brandId);

            var result = serviceModels.Select(m => new
            {
                id = m.Id,
                name = m.Name
            });

            return Json(result);
        }
    }
}
