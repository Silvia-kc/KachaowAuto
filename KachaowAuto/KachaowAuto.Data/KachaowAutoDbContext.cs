using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data
{
    public class KachaowAutoDbContext
	: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public KachaowAutoDbContext(DbContextOptions<KachaowAutoDbContext> options)
            : base(options)
        {
        }
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Workshop> Workshops => Set<Workshop>();

        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Model> Models => Set<Model>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<EngineType> EngineTypes => Set<EngineType>();
        public DbSet<BodyType> BodyTypes => Set<BodyType>();

        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<WorkshopService> WorkshopServices => Set<WorkshopService>();

        public DbSet<AppointmentStatus> AppointmentStatuses => Set<AppointmentStatus>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentMechanic> AppointmentMechanics => Set<AppointmentMechanic>();

        public DbSet<PartCategory> PartCategories => Set<PartCategory>();
        public DbSet<Part> Parts => Set<Part>();
        public DbSet<PartImage> PartImages => Set<PartImage>();
        public DbSet<AppointmentPart> AppointmentParts => Set<AppointmentPart>();
        public DbSet<PartRequest> PartRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppointmentMechanic>()
	.HasKey(am => new { am.AppointmentId, am.MechanicId });

			builder.Entity<AppointmentMechanic>()
				.HasOne(am => am.Appointment)
				.WithMany(a => a.AppointmentMechanics)
				.HasForeignKey(am => am.AppointmentId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<AppointmentMechanic>()
				.HasOne(am => am.Mechanic)
				.WithMany(u => u.AppointmentMechanics)
				.HasForeignKey(am => am.MechanicId)
				.OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PartRequest>(entity =>
            {
                entity.HasKey(pr => pr.PartRequestId);

                entity.Property(pr => pr.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(pr => pr.Note)
                    .HasMaxLength(500);

                entity.Property(pr => pr.AdminNote)
                    .HasMaxLength(500);

                entity.HasOne(pr => pr.Part)
                    .WithMany(p => p.PartRequests)
                    .HasForeignKey(pr => pr.PartId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pr => pr.Mechanic)
                    .WithMany(u => u.PartRequests)
                    .HasForeignKey(pr => pr.MechanicId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
