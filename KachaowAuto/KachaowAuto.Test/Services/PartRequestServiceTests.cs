using KachaowAuto.Core.Implementations;
using KachaowAuto.Core.Models.PartRequest;
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
    public class PartRequestServiceTests
    {
        [Fact]
        public async Task GetCreateModelAsync_ShouldReturnModel_WhenPartExists()
        {
            using var context = DbContextFactory.Create();

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Brake Pads",
                PartCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = await sut.GetCreateModelAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.PartId);
            Assert.Equal("Brake Pads", result.PartName);
            Assert.Equal(1, result.Quantity);
        }

        [Fact]
        public async Task GetCreateModelAsync_ShouldReturnNull_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartRequestService(context);

            var result = await sut.GetCreateModelAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFalse_WhenPartDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartRequestService(context);

            var model = new PartRequestCreateServiceModel
            {
                PartId = 999,
                Quantity = 2,
                Note = "Urgent"
            };

            var result = await sut.CreateAsync(model, mechanicId: 5);

            Assert.False(result);
            Assert.Equal(0, await context.PartRequests.CountAsync());
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateRequest_WhenPartExists()
        {
            using var context = DbContextFactory.Create();

            context.Parts.Add(new Part
            {
                PartId = 1,
                PartName = "Oil Filter",
                PartCategoryId = 1
            });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var model = new PartRequestCreateServiceModel
            {
                PartId = 1,
                Quantity = 3,
                Note = "Need it today"
            };

            var result = await sut.CreateAsync(model, mechanicId: 7);

            Assert.True(result);
            Assert.Equal(1, await context.PartRequests.CountAsync());

            var request = await context.PartRequests.FirstAsync();
            Assert.Equal(1, request.PartId);
            Assert.Equal(7, request.MechanicId);
            Assert.Equal(3, request.Quantity);
            Assert.Equal("Need it today", request.Note);
            Assert.Equal("Pending", request.Status);
            Assert.NotEqual(default, request.RequestedAt);
        }

        [Fact]
        public async Task GetMyRequestsAsync_ShouldReturnOnlyRequestsForGivenMechanic_OrderedDescending()
        {
            using var context = DbContextFactory.Create();

            var part1 = new Part
            {
                PartId = 1,
                PartName = "Brake Disc",
                PartCategoryId = 1
            };

            var part2 = new Part
            {
                PartId = 2,
                PartName = "Shock Absorber",
                PartCategoryId = 1
            };

            context.Parts.AddRange(part1, part2);

            context.PartRequests.AddRange(
                new PartRequest
                {
                    PartRequestId = 1,
                    PartId = 1,
                    Part = part1,
                    MechanicId = 3,
                    Quantity = 1,
                    Status = "Pending",
                    RequestedAt = new DateTime(2026, 4, 10)
                },
                new PartRequest
                {
                    PartRequestId = 2,
                    PartId = 2,
                    Part = part2,
                    MechanicId = 3,
                    Quantity = 2,
                    Status = "Approved",
                    RequestedAt = new DateTime(2026, 4, 12)
                },
                new PartRequest
                {
                    PartRequestId = 3,
                    PartId = 1,
                    Part = part1,
                    MechanicId = 9,
                    Quantity = 5,
                    Status = "Rejected",
                    RequestedAt = new DateTime(2026, 4, 15)
                });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = (await sut.GetMyRequestsAsync(3)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[0].PartRequestId);
            Assert.Equal(1, result[1].PartRequestId);
            Assert.DoesNotContain(result, r => r.PartRequestId == 3);
            Assert.Equal("Shock Absorber", result[0].PartName);
            Assert.Equal("Brake Disc", result[1].PartName);
        }

        [Fact]
        public async Task GetAllRequestsAsync_ShouldReturnAllRequests_OrderedDescending()
        {
            using var context = DbContextFactory.Create();

            var part1 = new Part
            {
                PartId = 1,
                PartName = "Filter",
                PartCategoryId = 1
            };

            var part2 = new Part
            {
                PartId = 2,
                PartName = "Belt",
                PartCategoryId = 1
            };

            var mechanic1 = new ApplicationUser
            {
                Id = 1,
                UserName = "mechanic1",
                FullName = "Mechanic One"
            };

            var mechanic2 = new ApplicationUser
            {
                Id = 2,
                UserName = "mechanic2",
                FullName = "Mechanic Two"
            };

            context.Parts.AddRange(part1, part2);
            context.Users.AddRange(mechanic1, mechanic2);

            context.PartRequests.AddRange(
                new PartRequest
                {
                    PartRequestId = 1,
                    PartId = 1,
                    Part = part1,
                    MechanicId = 1,
                    Mechanic = mechanic1,
                    Quantity = 1,
                    Status = "Pending",
                    RequestedAt = new DateTime(2026, 4, 10)
                },
                new PartRequest
                {
                    PartRequestId = 2,
                    PartId = 2,
                    Part = part2,
                    MechanicId = 2,
                    Mechanic = mechanic2,
                    Quantity = 4,
                    Status = "Approved",
                    RequestedAt = new DateTime(2026, 4, 11)
                });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = (await sut.GetAllRequestsAsync()).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[0].PartRequestId);
            Assert.Equal(1, result[1].PartRequestId);
            Assert.Equal("Belt", result[0].PartName);
            Assert.Equal("mechanic2", result[0].MechanicName);
            Assert.Equal("Filter", result[1].PartName);
            Assert.Equal("mechanic1", result[1].MechanicName);
        }

        [Fact]
        public async Task ApproveAsync_ShouldReturnFalse_WhenRequestDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartRequestService(context);

            var result = await sut.ApproveAsync(999, "Approved by admin");

            Assert.False(result);
        }

        [Fact]
        public async Task ApproveAsync_ShouldSetApprovedStatus_WhenRequestExists()
        {
            using var context = DbContextFactory.Create();

            context.PartRequests.Add(new PartRequest
            {
                PartRequestId = 1,
                PartId = 1,
                MechanicId = 1,
                Quantity = 2,
                Status = "Pending",
                RequestedAt = new DateTime(2026, 4, 10)
            });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = await sut.ApproveAsync(1, "Looks good");

            Assert.True(result);

            var request = await context.PartRequests.FirstAsync();
            Assert.Equal("Approved", request.Status);
            Assert.Equal("Looks good", request.AdminNote);
            Assert.NotNull(request.ProcessedAt);
        }

        [Fact]
        public async Task RejectAsync_ShouldReturnFalse_WhenRequestDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartRequestService(context);

            var result = await sut.RejectAsync(999, "Rejected");

            Assert.False(result);
        }

        [Fact]
        public async Task RejectAsync_ShouldSetRejectedStatus_WhenRequestExists()
        {
            using var context = DbContextFactory.Create();

            context.PartRequests.Add(new PartRequest
            {
                PartRequestId = 1,
                PartId = 1,
                MechanicId = 1,
                Quantity = 2,
                Status = "Pending",
                RequestedAt = new DateTime(2026, 4, 10)
            });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = await sut.RejectAsync(1, "Not available");

            Assert.True(result);

            var request = await context.PartRequests.FirstAsync();
            Assert.Equal("Rejected", request.Status);
            Assert.Equal("Not available", request.AdminNote);
            Assert.NotNull(request.ProcessedAt);
        }

        [Fact]
        public async Task MarkAsSentAsync_ShouldReturnFalse_WhenRequestDoesNotExist()
        {
            using var context = DbContextFactory.Create();

            var sut = new PartRequestService(context);

            var result = await sut.MarkAsSentAsync(999, "Sent to workshop");

            Assert.False(result);
        }

        [Fact]
        public async Task MarkAsSentAsync_ShouldSetSentStatus_WhenRequestExists()
        {
            using var context = DbContextFactory.Create();

            context.PartRequests.Add(new PartRequest
            {
                PartRequestId = 1,
                PartId = 1,
                MechanicId = 1,
                Quantity = 2,
                Status = "Approved",
                RequestedAt = new DateTime(2026, 4, 10)
            });

            await context.SaveChangesAsync();

            var sut = new PartRequestService(context);

            var result = await sut.MarkAsSentAsync(1, "Delivered");

            Assert.True(result);

            var request = await context.PartRequests.FirstAsync();
            Assert.Equal("Sent", request.Status);
            Assert.Equal("Delivered", request.AdminNote);
            Assert.NotNull(request.ProcessedAt);
        }
    }
}
