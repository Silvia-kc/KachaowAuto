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
            builder.Entity<PartCategory>().HasData(
        new PartCategory { PartCategoryId = 1, Name = "Braking system" },
        new PartCategory { PartCategoryId = 2, Name = "Wheel suspension" },
        new PartCategory { PartCategoryId = 3, Name = "Steering system" },
        new PartCategory { PartCategoryId = 4, Name = "Wheel drive" },
        new PartCategory { PartCategoryId = 5, Name = "Transmission" },
        new PartCategory { PartCategoryId = 6, Name = "Belt drive" },
        new PartCategory { PartCategoryId = 7, Name = "Engine parts" },
        new PartCategory { PartCategoryId = 8, Name = "Cylinder block parts" },
        new PartCategory { PartCategoryId = 9, Name = "Garnishes" },
        new PartCategory { PartCategoryId = 10, Name = "Cooling system" },
        new PartCategory { PartCategoryId = 11, Name = "Climate system" },
        new PartCategory { PartCategoryId = 12, Name = "Heating and ventilation" },
        new PartCategory { PartCategoryId = 13, Name = "Filters" },
        new PartCategory { PartCategoryId = 14, Name = "Oils and liquids" },
        new PartCategory { PartCategoryId = 15, Name = "Ignition system" },
        new PartCategory { PartCategoryId = 16, Name = "Sensors and gauges" },
        new PartCategory { PartCategoryId = 17, Name = "Fuel system" },
        new PartCategory { PartCategoryId = 18, Name = "Electrical system" },
        new PartCategory { PartCategoryId = 19, Name = "Starting system" },
        new PartCategory { PartCategoryId = 20, Name = "Window cleaning" },
        new PartCategory { PartCategoryId = 21, Name = "Exhausts and mufflers" },
        new PartCategory { PartCategoryId = 22, Name = "Large parts" },
        new PartCategory { PartCategoryId = 23, Name = "Lights" },
        new PartCategory { PartCategoryId = 24, Name = "Interior" }
    );
            builder.Entity<Part>().HasData(
    new Part { PartId = 1, PartName = "Front Brake Pads Set", Manufacturer = "Brembo", PartNumber = "BRK-001", Description = "Front axle brake pads set", UnitPrice = 46.00m, IsActive = true, PartCategoryId = 1 },
    new Part { PartId = 2, PartName = "Rear Brake Disc", Manufacturer = "Bosch", PartNumber = "BRK-002", Description = "Rear ventilated brake disc", UnitPrice = 38.10m, IsActive = true, PartCategoryId = 1 },
    new Part { PartId = 3, PartName = "Brake Caliper", Manufacturer = "ATE", PartNumber = "BRK-003", Description = "Front right brake caliper", UnitPrice = 74.15m, IsActive = true, PartCategoryId = 1 },

    new Part { PartId = 4, PartName = "Front Shock Absorber", Manufacturer = "Monroe", PartNumber = "SUS-001", Description = "Gas front shock absorber", UnitPrice = 67.50m, IsActive = true, PartCategoryId = 2 },
    new Part { PartId = 5, PartName = "Coil Spring", Manufacturer = "KYB", PartNumber = "SUS-002", Description = "Front suspension coil spring", UnitPrice = 34.50m, IsActive = true, PartCategoryId = 2 },
    new Part { PartId = 6, PartName = "Control Arm", Manufacturer = "Lemforder", PartNumber = "SUS-003", Description = "Front lower control arm", UnitPrice = 60.80m, IsActive = true, PartCategoryId = 2 },

    new Part { PartId = 7, PartName = "Tie Rod End", Manufacturer = "TRW", PartNumber = "STR-001", Description = "Outer tie rod end", UnitPrice = 18.50m, IsActive = true, PartCategoryId = 3 },
    new Part { PartId = 8, PartName = "Steering Rack Boot", Manufacturer = "Febi", PartNumber = "STR-002", Description = "Protective steering rack boot", UnitPrice = 9.60m, IsActive = true, PartCategoryId = 3 },
    new Part { PartId = 9, PartName = "Power Steering Pump", Manufacturer = "ZF", PartNumber = "STR-003", Description = "Hydraulic power steering pump", UnitPrice = 122.70m, IsActive = true, PartCategoryId = 3 },

    new Part { PartId = 10, PartName = "CV Joint Kit", Manufacturer = "SKF", PartNumber = "DRV-001", Description = "Outer CV joint repair kit", UnitPrice = 43.30m, IsActive = true, PartCategoryId = 4 },
    new Part { PartId = 11, PartName = "Drive Shaft", Manufacturer = "GKN", PartNumber = "DRV-002", Description = "Front left drive shaft", UnitPrice = 110.00m, IsActive = true, PartCategoryId = 4 },
    new Part { PartId = 12, PartName = "Wheel Hub Bearing", Manufacturer = "FAG", PartNumber = "DRV-003", Description = "Front wheel hub bearing", UnitPrice = 40.90m, IsActive = true, PartCategoryId = 4 },

    new Part { PartId = 13, PartName = "Clutch Kit", Manufacturer = "Sachs", PartNumber = "TRN-001", Description = "Complete clutch kit", UnitPrice = 147.80m, IsActive = true, PartCategoryId = 5 },
    new Part { PartId = 14, PartName = "Gearbox Mount", Manufacturer = "Febi", PartNumber = "TRN-002", Description = "Transmission support mount", UnitPrice = 27.80m, IsActive = true, PartCategoryId = 5 },
    new Part { PartId = 15, PartName = "Transmission Oil Seal", Manufacturer = "Elring", PartNumber = "TRN-003", Description = "Gearbox shaft oil seal", UnitPrice = 7.60m, IsActive = true, PartCategoryId = 5 },

    new Part { PartId = 16, PartName = "Timing Belt Kit", Manufacturer = "Gates", PartNumber = "BLT-001", Description = "Timing belt with tensioner", UnitPrice = 81.80m, IsActive = true, PartCategoryId = 6 },
    new Part { PartId = 17, PartName = "Accessory Belt", Manufacturer = "Contitech", PartNumber = "BLT-002", Description = "Multi-rib auxiliary belt", UnitPrice = 14.60m, IsActive = true, PartCategoryId = 6 },
    new Part { PartId = 18, PartName = "Belt Tensioner", Manufacturer = "INA", PartNumber = "BLT-003", Description = "Automatic belt tensioner", UnitPrice = 37.80m, IsActive = true, PartCategoryId = 6 },

    new Part { PartId = 19, PartName = "Piston Ring Set", Manufacturer = "Mahle", PartNumber = "ENG-001", Description = "Engine piston ring set", UnitPrice = 49.10m, IsActive = true, PartCategoryId = 7 },
    new Part { PartId = 20, PartName = "Engine Mount", Manufacturer = "Corteco", PartNumber = "ENG-002", Description = "Right engine support mount", UnitPrice = 45.20m, IsActive = true, PartCategoryId = 7 },
    new Part { PartId = 21, PartName = "Oil Pan", Manufacturer = "Vaico", PartNumber = "ENG-003", Description = "Steel engine oil pan", UnitPrice = 53.40m, IsActive = true, PartCategoryId = 7 },

    new Part { PartId = 22, PartName = "Cylinder Head Gasket", Manufacturer = "Elring", PartNumber = "CBP-001", Description = "Cylinder head gasket set", UnitPrice = 32.20m, IsActive = true, PartCategoryId = 8 },
    new Part { PartId = 23, PartName = "Engine Valve", Manufacturer = "AE", PartNumber = "CBP-002", Description = "Intake engine valve", UnitPrice = 10.90m, IsActive = true, PartCategoryId = 8 },
    new Part { PartId = 24, PartName = "Camshaft", Manufacturer = "Kolbenschmidt", PartNumber = "CBP-003", Description = "Exhaust camshaft", UnitPrice = 168.70m, IsActive = true, PartCategoryId = 8 },

    new Part { PartId = 25, PartName = "Valve Cover Gasket", Manufacturer = "Victor Reinz", PartNumber = "GAR-001", Description = "Rubber valve cover gasket", UnitPrice = 10.20m, IsActive = true, PartCategoryId = 9 },
    new Part { PartId = 26, PartName = "Oil Seal Ring", Manufacturer = "Elring", PartNumber = "GAR-002", Description = "Crankshaft oil seal", UnitPrice = 5.70m, IsActive = true, PartCategoryId = 9 },
    new Part { PartId = 27, PartName = "Intake Manifold Gasket", Manufacturer = "Ajusa", PartNumber = "GAR-003", Description = "Intake manifold gasket set", UnitPrice = 8.40m, IsActive = true, PartCategoryId = 9 },

    new Part { PartId = 28, PartName = "Radiator", Manufacturer = "Nissens", PartNumber = "COL-001", Description = "Engine cooling radiator", UnitPrice = 89.50m, IsActive = true, PartCategoryId = 10 },
    new Part { PartId = 29, PartName = "Thermostat", Manufacturer = "Wahler", PartNumber = "COL-002", Description = "Coolant thermostat", UnitPrice = 17.80m, IsActive = true, PartCategoryId = 10 },
    new Part { PartId = 30, PartName = "Water Pump", Manufacturer = "HEPU", PartNumber = "COL-003", Description = "Mechanical engine water pump", UnitPrice = 47.50m, IsActive = true, PartCategoryId = 10 },

    new Part { PartId = 31, PartName = "AC Compressor", Manufacturer = "Denso", PartNumber = "CLI-001", Description = "Air conditioning compressor", UnitPrice = 215.00m, IsActive = true, PartCategoryId = 11 },
    new Part { PartId = 32, PartName = "Condenser", Manufacturer = "NRF", PartNumber = "CLI-002", Description = "AC condenser radiator", UnitPrice = 75.00m, IsActive = true, PartCategoryId = 11 },
    new Part { PartId = 33, PartName = "Cabin Temperature Sensor", Manufacturer = "Valeo", PartNumber = "CLI-003", Description = "Interior temperature sensor", UnitPrice = 14.30m, IsActive = true, PartCategoryId = 11 },

    new Part { PartId = 34, PartName = "Heater Core", Manufacturer = "Valeo", PartNumber = "HTV-001", Description = "Passenger cabin heater radiator", UnitPrice = 60.30m, IsActive = true, PartCategoryId = 12 },
    new Part { PartId = 35, PartName = "Blower Motor", Manufacturer = "Bosch", PartNumber = "HTV-002", Description = "Cabin ventilation blower motor", UnitPrice = 70.80m, IsActive = true, PartCategoryId = 12 },
    new Part { PartId = 36, PartName = "Blower Resistor", Manufacturer = "Hella", PartNumber = "HTV-003", Description = "Ventilation fan resistor", UnitPrice = 21.60m, IsActive = true, PartCategoryId = 12 },

    new Part { PartId = 37, PartName = "Oil Filter", Manufacturer = "MANN", PartNumber = "FIL-001", Description = "Engine oil filter cartridge", UnitPrice = 7.20m, IsActive = true, PartCategoryId = 13 },
    new Part { PartId = 38, PartName = "Air Filter", Manufacturer = "Bosch", PartNumber = "FIL-002", Description = "Engine intake air filter", UnitPrice = 9.10m, IsActive = true, PartCategoryId = 13 },
    new Part { PartId = 39, PartName = "Cabin Filter", Manufacturer = "Mahle", PartNumber = "FIL-003", Description = "Pollen cabin filter", UnitPrice = 11.40m, IsActive = true, PartCategoryId = 13 },

    new Part { PartId = 40, PartName = "Engine Oil 5W-30", Manufacturer = "Castrol", PartNumber = "OIL-001", Description = "Fully synthetic engine oil 1L", UnitPrice = 9.60m, IsActive = true, PartCategoryId = 14 },
    new Part { PartId = 41, PartName = "Brake Fluid DOT 4", Manufacturer = "ATE", PartNumber = "OIL-002", Description = "Brake fluid 500ml", UnitPrice = 6.40m, IsActive = true, PartCategoryId = 14 },
    new Part { PartId = 42, PartName = "Coolant G12", Manufacturer = "Febi", PartNumber = "OIL-003", Description = "Ready to use coolant 1L", UnitPrice = 5.00m, IsActive = true, PartCategoryId = 14 },

    new Part { PartId = 43, PartName = "Spark Plug", Manufacturer = "NGK", PartNumber = "IGN-001", Description = "Standard ignition spark plug", UnitPrice = 6.10m, IsActive = true, PartCategoryId = 15 },
    new Part { PartId = 44, PartName = "Ignition Coil", Manufacturer = "Bosch", PartNumber = "IGN-002", Description = "Pencil ignition coil", UnitPrice = 32.70m, IsActive = true, PartCategoryId = 15 },
    new Part { PartId = 45, PartName = "Glow Plug", Manufacturer = "Beru", PartNumber = "IGN-003", Description = "Diesel engine glow plug", UnitPrice = 9.40m, IsActive = true, PartCategoryId = 15 },

    new Part { PartId = 46, PartName = "ABS Sensor", Manufacturer = "Hella", PartNumber = "SEN-001", Description = "Wheel speed ABS sensor", UnitPrice = 20.20m, IsActive = true, PartCategoryId = 16 },
    new Part { PartId = 47, PartName = "Coolant Temperature Sensor", Manufacturer = "Facet", PartNumber = "SEN-002", Description = "Engine coolant temperature sensor", UnitPrice = 11.10m, IsActive = true, PartCategoryId = 16 },
    new Part { PartId = 48, PartName = "Oil Pressure Switch", Manufacturer = "Febi", PartNumber = "SEN-003", Description = "Oil pressure warning switch", UnitPrice = 9.20m, IsActive = true, PartCategoryId = 16 },

    new Part { PartId = 49, PartName = "Fuel Pump", Manufacturer = "Pierburg", PartNumber = "FUE-001", Description = "Electric in-tank fuel pump", UnitPrice = 78.70m, IsActive = true, PartCategoryId = 17 },
    new Part { PartId = 50, PartName = "Fuel Injector", Manufacturer = "Bosch", PartNumber = "FUE-002", Description = "Petrol fuel injector", UnitPrice = 49.30m, IsActive = true, PartCategoryId = 17 },
    new Part { PartId = 51, PartName = "Fuel Filter Housing", Manufacturer = "Mahle", PartNumber = "FUE-003", Description = "Diesel fuel filter housing", UnitPrice = 42.00m, IsActive = true, PartCategoryId = 17 },

    new Part { PartId = 52, PartName = "Alternator", Manufacturer = "Valeo", PartNumber = "ELE-001", Description = "Vehicle charging alternator", UnitPrice = 158.50m, IsActive = true, PartCategoryId = 18 },
    new Part { PartId = 53, PartName = "Battery Terminal", Manufacturer = "Bosch", PartNumber = "ELE-002", Description = "Positive battery terminal clamp", UnitPrice = 6.90m, IsActive = true, PartCategoryId = 18 },
    new Part { PartId = 54, PartName = "Fuse Box", Manufacturer = "Hella", PartNumber = "ELE-003", Description = "Main vehicle fuse box", UnitPrice = 66.00m, IsActive = true, PartCategoryId = 18 },

    new Part { PartId = 55, PartName = "Starter Motor", Manufacturer = "Bosch", PartNumber = "STA-001", Description = "12V starter motor", UnitPrice = 135.50m, IsActive = true, PartCategoryId = 19 },
    new Part { PartId = 56, PartName = "Starter Solenoid", Manufacturer = "Valeo", PartNumber = "STA-002", Description = "Starter solenoid switch", UnitPrice = 25.50m, IsActive = true, PartCategoryId = 19 },
    new Part { PartId = 57, PartName = "Battery 74Ah", Manufacturer = "Varta", PartNumber = "STA-003", Description = "Maintenance-free starter battery", UnitPrice = 96.60m, IsActive = true, PartCategoryId = 19 },

    new Part { PartId = 58, PartName = "Wiper Blade Front", Manufacturer = "Bosch", PartNumber = "WND-001", Description = "Front flat wiper blade", UnitPrice = 8.60m, IsActive = true, PartCategoryId = 20 },
    new Part { PartId = 59, PartName = "Washer Pump", Manufacturer = "Febi", PartNumber = "WND-002", Description = "Windshield washer pump", UnitPrice = 12.60m, IsActive = true, PartCategoryId = 20 },
    new Part { PartId = 60, PartName = "Washer Nozzle", Manufacturer = "Vemo", PartNumber = "WND-003", Description = "Bonnet washer spray nozzle", UnitPrice = 4.50m, IsActive = true, PartCategoryId = 20 },

    new Part { PartId = 61, PartName = "Rear Muffler", Manufacturer = "Walker", PartNumber = "EXH-001", Description = "Rear silencer muffler", UnitPrice = 73.70m, IsActive = true, PartCategoryId = 21 },
    new Part { PartId = 62, PartName = "Catalytic Converter", Manufacturer = "BM Catalysts", PartNumber = "EXH-002", Description = "Emission catalytic converter", UnitPrice = 199.40m, IsActive = true, PartCategoryId = 21 },
    new Part { PartId = 63, PartName = "Exhaust Pipe Clamp", Manufacturer = "Bosal", PartNumber = "EXH-003", Description = "Steel exhaust clamp", UnitPrice = 5.80m, IsActive = true, PartCategoryId = 21 },

    new Part { PartId = 64, PartName = "Front Bumper", Manufacturer = "TYC", PartNumber = "LRG-001", Description = "Primed front bumper", UnitPrice = 112.50m, IsActive = true, PartCategoryId = 22 },
    new Part { PartId = 65, PartName = "Engine Hood", Manufacturer = "BLIC", PartNumber = "LRG-002", Description = "Front bonnet hood panel", UnitPrice = 161.00m, IsActive = true, PartCategoryId = 22 },
    new Part { PartId = 66, PartName = "Front Fender", Manufacturer = "Van Wezel", PartNumber = "LRG-003", Description = "Left front wing panel", UnitPrice = 65.70m, IsActive = true, PartCategoryId = 22 },

    new Part { PartId = 67, PartName = "Headlight Assembly", Manufacturer = "Hella", PartNumber = "LGT-001", Description = "Front left headlight", UnitPrice = 126.80m, IsActive = true, PartCategoryId = 23 },
    new Part { PartId = 68, PartName = "Tail Light", Manufacturer = "Depo", PartNumber = "LGT-002", Description = "Rear right tail lamp", UnitPrice = 47.40m, IsActive = true, PartCategoryId = 23 },
    new Part { PartId = 69, PartName = "Fog Light", Manufacturer = "Valeo", PartNumber = "LGT-003", Description = "Front bumper fog light", UnitPrice = 24.20m, IsActive = true, PartCategoryId = 23 },

    new Part { PartId = 70, PartName = "Window Switch", Manufacturer = "Vemo", PartNumber = "INT-001", Description = "Driver electric window switch", UnitPrice = 19.90m, IsActive = true, PartCategoryId = 24 },
    new Part { PartId = 71, PartName = "Gear Shift Knob", Manufacturer = "Topran", PartNumber = "INT-002", Description = "Manual gear lever knob", UnitPrice = 13.70m, IsActive = true, PartCategoryId = 24 },
    new Part { PartId = 72, PartName = "Floor Mat Set", Manufacturer = "Petex", PartNumber = "INT-003", Description = "Textile interior floor mat set", UnitPrice = 22.80m, IsActive = true, PartCategoryId = 24 }
);  
            builder.Entity<PartImage>().HasData(
                     new PartImage { PartImageId = 1, PartId = 46, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203068/kachaowauto/parts/rxdv9gqcsp49mzwnr8fm.jpg" },
                     new PartImage { PartImageId = 2, PartId = 31, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203091/kachaowauto/parts/dyaie8c7ynikcuuiiqej.jpg" },
                     new PartImage { PartImageId = 3, PartId = 17, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203105/kachaowauto/parts/thsbcczibnh8spsonmun.jpg" },
                     new PartImage { PartImageId = 4, PartId = 38, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203119/kachaowauto/parts/yithggzz3o2zkj5u9hnx.jpg" },
                     new PartImage { PartImageId = 5, PartId = 52, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203132/kachaowauto/parts/vndxtrtn3esyyjv7jhzf.jpg" },
                     new PartImage { PartImageId = 6, PartId = 57, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203146/kachaowauto/parts/ht6zvgispnz7boxh4wgu.jpg" },
                     new PartImage { PartImageId = 7, PartId = 53, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203160/kachaowauto/parts/sncuh48velbvyklkdq5v.jpg" },
                     new PartImage { PartImageId = 8, PartId = 18, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203178/kachaowauto/parts/tvmrxpwggen9jgknoslf.jpg" },
                     new PartImage { PartImageId = 9, PartId = 35, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203195/kachaowauto/parts/bujpr6ehhp0pbuh30ms9.webp" },
                     new PartImage { PartImageId = 10, PartId = 36, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203217/kachaowauto/parts/jdxc2llawr96q6ql5mdq.jpg" },
                     new PartImage { PartImageId = 11, PartId = 3, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203238/kachaowauto/parts/bfxmv7swtnw6w9erxcr9.jpg" },
                     new PartImage { PartImageId = 12, PartId = 41, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203272/kachaowauto/parts/zlts30rzbgs89u2ccud7.png" },
                     new PartImage { PartImageId = 13, PartId = 39, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203292/kachaowauto/parts/xt81fregkhoych98wyzp.jpg" },
                     new PartImage { PartImageId = 14, PartId = 33, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203308/kachaowauto/parts/qkfyhgtat87yugbepy2a.jpg" },
                     new PartImage { PartImageId = 15, PartId = 24, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203322/kachaowauto/parts/jeljogguchewmrbnaasz.jpg" },
                     new PartImage { PartImageId = 16, PartId = 62, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203343/kachaowauto/parts/nwkaczxwxkvxk5suavih.jpg" },
                     new PartImage { PartImageId = 17, PartId = 13, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203361/kachaowauto/parts/vumoqfreegd1hqbx0has.jpg" },
                     new PartImage { PartImageId = 18, PartId = 5, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203382/kachaowauto/parts/rn75chixkuglku9vpxdl.jpg" },
                     new PartImage { PartImageId = 19, PartId = 32, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203404/kachaowauto/parts/dmzvjdiumrrtr9gwirrg.png" },
                     new PartImage { PartImageId = 20, PartId = 6, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203420/kachaowauto/parts/nlrh5syefrsisrprwtjg.jpg" },
                     new PartImage { PartImageId = 21, PartId = 42, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203455/kachaowauto/parts/cdcnkcn66hmiahydsa6g.jpg" },
                     new PartImage { PartImageId = 22, PartId = 47, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203471/kachaowauto/parts/ssmujpnktrnwlumsiwdf.jpg" },
                     new PartImage { PartImageId = 23, PartId = 10, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203484/kachaowauto/parts/y2xwug8ei0bagohlmzih.jpg" },
                     new PartImage { PartImageId = 24, PartId = 22, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203501/kachaowauto/parts/ji3y6ckpqvkx0ncdeckb.jpg" },
                     new PartImage { PartImageId = 25, PartId = 11, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203517/kachaowauto/parts/wsdyppcwpq5qnhryzpep.jpg" },
                     new PartImage { PartImageId = 26, PartId = 65, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203530/kachaowauto/parts/dijymhgyehpdynxtxnf4.jpg" },
                     new PartImage { PartImageId = 27, PartId = 20, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203546/kachaowauto/parts/ouwvhhzwqoecfgzaqbcm.webp" },
                     new PartImage { PartImageId = 28, PartId = 40, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203563/kachaowauto/parts/ipfbfxodv2mzj1h4ehao.jpg" },
                     new PartImage { PartImageId = 29, PartId = 23, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203578/kachaowauto/parts/ytthk46e5d3cbt7b9akx.jpg" },
                     new PartImage { PartImageId = 30, PartId = 63, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203593/kachaowauto/parts/z8rvp2ms7ovhacwzmayy.jpg" },
                     new PartImage { PartImageId = 31, PartId = 72, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203612/kachaowauto/parts/ukzaersts5ipw470bi4h.jpg" },
                     new PartImage { PartImageId = 32, PartId = 69, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203627/kachaowauto/parts/ixprjnnvizvf4bvgnlme.jpg" },
                     new PartImage { PartImageId = 33, PartId = 1, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203644/kachaowauto/parts/t25efpowdpqqkxkqwood.jpg" },
                     new PartImage { PartImageId = 34, PartId = 64, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203689/kachaowauto/parts/y8f3sgbhfuvjfcdfpjcd.jpg" },
                     new PartImage { PartImageId = 35, PartId = 66, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203706/kachaowauto/parts/pdbutiihcffewgcdom4v.jpg" },
                     new PartImage { PartImageId = 36, PartId = 4, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203723/kachaowauto/parts/wjdtqluh76k393pbxnyn.png" },
                     new PartImage { PartImageId = 37, PartId = 51, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203738/kachaowauto/parts/dv2p6cqx1hg6i9qjw2tq.jpg" },
                     new PartImage { PartImageId = 38, PartId = 50, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203753/kachaowauto/parts/dfh1d5qsif2tkmsjkpza.jpg" },
                     new PartImage { PartImageId = 39, PartId = 49, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203768/kachaowauto/parts/oo44aglswf9a2cmdyj3m.jpg" },
                     new PartImage { PartImageId = 40, PartId = 54, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203785/kachaowauto/parts/yt3ymqwxtihnib6ifprt.webp" },
                     new PartImage { PartImageId = 41, PartId = 71, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203799/kachaowauto/parts/t6gcjntghjigyip1m69v.jpg" },
                     new PartImage { PartImageId = 42, PartId = 14, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203815/kachaowauto/parts/yenwkg39tofq4hk81grd.jpg" },
                     new PartImage { PartImageId = 43, PartId = 45, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203831/kachaowauto/parts/qmyxyichuxdtilwvgvlq.jpg" },
                     new PartImage { PartImageId = 44, PartId = 67, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203849/kachaowauto/parts/j8oiuzmfyyesv1fzug8t.jpg" },
                     new PartImage { PartImageId = 45, PartId = 34, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203865/kachaowauto/parts/xccjb68lzprsfwvlqeqb.jpg" },
                     new PartImage { PartImageId = 46, PartId = 44, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203879/kachaowauto/parts/rkj5jzvop0lwdfxiuysb.webp" },
                     new PartImage { PartImageId = 47, PartId = 27, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203893/kachaowauto/parts/siebcpnmhkhnymukondx.jpg" },
                     new PartImage { PartImageId = 48, PartId = 37, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203907/kachaowauto/parts/pdfgoupg7g27gqnhzkvg.jpg" },
                     new PartImage { PartImageId = 49, PartId = 21, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203919/kachaowauto/parts/qrvj9ozdxypwclm4eg1l.jpg" },
                     new PartImage { PartImageId = 50, PartId = 48, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203940/kachaowauto/parts/cdcpexft9oh6udsajtaj.jpg" },
                     new PartImage { PartImageId = 51, PartId = 26, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203953/kachaowauto/parts/g0xmmbicapjshtspmlwd.jpg" },
                     new PartImage { PartImageId = 52, PartId = 19, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203966/kachaowauto/parts/nfeadizk6x4hhwc2whow.jpg" },
                     new PartImage { PartImageId = 53, PartId = 9, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203983/kachaowauto/parts/re5htadyrnwksvbmbqsb.jpg" },
                     new PartImage { PartImageId = 54, PartId = 28, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204051/kachaowauto/parts/ult6oomfcv3obgzsl3vk.jpg" },
                     new PartImage { PartImageId = 55, PartId = 2, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204072/kachaowauto/parts/xllkggxe8ew0kj8fithj.webp" },
                     new PartImage { PartImageId = 56, PartId = 61, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204090/kachaowauto/parts/cbtdb5xy5uvigtpclko0.webp" },
                     new PartImage { PartImageId = 57, PartId = 43, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204107/kachaowauto/parts/j5jah8qey83t6sv4gobn.jpg" },
                     new PartImage { PartImageId = 58, PartId = 55, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204124/kachaowauto/parts/i7m7obk1znldiuuas7ug.jpg" },
                     new PartImage { PartImageId = 59, PartId = 56, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204141/kachaowauto/parts/kfxp6ovjfse2nui00u0f.jpg" },
                     new PartImage { PartImageId = 60, PartId = 8, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204154/kachaowauto/parts/s8ntegg3rijw3jc7cll4.jpg" },
                     new PartImage { PartImageId = 61, PartId = 68, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204166/kachaowauto/parts/z8kqhc1wabjcib69unga.jpg" },
                     new PartImage { PartImageId = 62, PartId = 29, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204177/kachaowauto/parts/kaz5x9yhzdjet69tbqm0.jpg" },
                     new PartImage { PartImageId = 63, PartId = 7, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204190/kachaowauto/parts/owhxlxurctitks4idvyt.jpg" },
                     new PartImage { PartImageId = 64, PartId = 16, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204206/kachaowauto/parts/s98imtmuiemnofk76xhc.webp" },
                     new PartImage { PartImageId = 65, PartId = 15, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204218/kachaowauto/parts/fwohy27jwr4pyryxbulm.jpg" },
                     new PartImage { PartImageId = 66, PartId = 25, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204233/kachaowauto/parts/mf080slgwwvxnqpgi2pl.jpg" },
                     new PartImage { PartImageId = 67, PartId = 60, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204244/kachaowauto/parts/jyzsutg9byobfgdrfowt.jpg" },
                     new PartImage { PartImageId = 68, PartId = 59, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204257/kachaowauto/parts/uajxfhby6rmd7sbz0j1m.jpg" },
                     new PartImage { PartImageId = 69, PartId = 30, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204277/kachaowauto/parts/eimtbih0nu4qpnzdqmsk.png" },
                     new PartImage { PartImageId = 70, PartId = 12, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204292/kachaowauto/parts/rtxlaqkrt64ozn3uk7ki.jpg" },
                     new PartImage { PartImageId = 71, PartId = 70, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204307/kachaowauto/parts/e9pws6zcdlensddztbxz.jpg" },
                     new PartImage { PartImageId = 72, PartId = 58, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204324/kachaowauto/parts/dmle30bgcgxbm5pjer9t.webp" }
            );

        }

    }
}
