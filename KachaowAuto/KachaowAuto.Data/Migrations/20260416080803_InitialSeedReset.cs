using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KachaowAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentStatuses",
                columns: table => new
                {
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatuses", x => x.AppointmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BodyTypes",
                columns: table => new
                {
                    BodyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyTypes", x => x.BodyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    EngineTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.EngineTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    PartCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCategories", x => x.PartCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.ServiceCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EngineTypeId = table.Column<int>(type: "int", nullable: false),
                    EngineVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false),
                    BodyTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Models_BodyTypes_BodyTypeId",
                        column: x => x.BodyTypeId,
                        principalTable: "BodyTypes",
                        principalColumn: "BodyTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_EngineTypes_EngineTypeId",
                        column: x => x.EngineTypeId,
                        principalTable: "EngineTypes",
                        principalColumn: "EngineTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PartCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_PartCategories_PartCategoryId",
                        column: x => x.PartCategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "PartCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.WorkshopId);
                    table.ForeignKey(
                        name: "FK_Workshops_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceTo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "ServiceCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartImages",
                columns: table => new
                {
                    PartImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartImages", x => x.PartImageId);
                    table.ForeignKey(
                        name: "FK_PartImages_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartRequests",
                columns: table => new
                {
                    PartRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartRequests", x => x.PartRequestId);
                    table.ForeignKey(
                        name: "FK_PartRequests_AspNetUsers_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartRequests_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopServices",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopServices", x => new { x.WorkshopId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_WorkshopServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopServices_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "WorkshopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ProblemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                        column: x => x.AppointmentStatusId,
                        principalTable: "AppointmentStatuses",
                        principalColumn: "AppointmentStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "WorkshopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentMechanics",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    MechanicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentMechanics", x => new { x.AppointmentId, x.MechanicId });
                    table.ForeignKey(
                        name: "FK_AppointmentMechanics_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentMechanics_AspNetUsers_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentParts",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPriceAtTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentParts", x => new { x.AppointmentId, x.PartId });
                    table.ForeignKey(
                        name: "FK_AppointmentParts_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PartCategories",
                columns: new[] { "PartCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Braking system" },
                    { 2, "Wheel suspension" },
                    { 3, "Steering system" },
                    { 4, "Wheel drive" },
                    { 5, "Transmission" },
                    { 6, "Belt drive" },
                    { 7, "Engine parts" },
                    { 8, "Cylinder block parts" },
                    { 9, "Garnishes" },
                    { 10, "Cooling system" },
                    { 11, "Climate system" },
                    { 12, "Heating and ventilation" },
                    { 13, "Filters" },
                    { 14, "Oils and liquids" },
                    { 15, "Ignition system" },
                    { 16, "Sensors and gauges" },
                    { 17, "Fuel system" },
                    { 18, "Electrical system" },
                    { 19, "Starting system" },
                    { 20, "Window cleaning" },
                    { 21, "Exhausts and mufflers" },
                    { 22, "Large parts" },
                    { 23, "Lights" },
                    { 24, "Interior" }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "PartId", "Description", "IsActive", "Manufacturer", "PartCategoryId", "PartName", "PartNumber", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Front axle brake pads set", true, "Brembo", 1, "Front Brake Pads Set", "BRK-001", 46.00m },
                    { 2, "Rear ventilated brake disc", true, "Bosch", 1, "Rear Brake Disc", "BRK-002", 38.10m },
                    { 3, "Front right brake caliper", true, "ATE", 1, "Brake Caliper", "BRK-003", 74.15m },
                    { 4, "Gas front shock absorber", true, "Monroe", 2, "Front Shock Absorber", "SUS-001", 67.50m },
                    { 5, "Front suspension coil spring", true, "KYB", 2, "Coil Spring", "SUS-002", 34.50m },
                    { 6, "Front lower control arm", true, "Lemforder", 2, "Control Arm", "SUS-003", 60.80m },
                    { 7, "Outer tie rod end", true, "TRW", 3, "Tie Rod End", "STR-001", 18.50m },
                    { 8, "Protective steering rack boot", true, "Febi", 3, "Steering Rack Boot", "STR-002", 9.60m },
                    { 9, "Hydraulic power steering pump", true, "ZF", 3, "Power Steering Pump", "STR-003", 122.70m },
                    { 10, "Outer CV joint repair kit", true, "SKF", 4, "CV Joint Kit", "DRV-001", 43.30m },
                    { 11, "Front left drive shaft", true, "GKN", 4, "Drive Shaft", "DRV-002", 110.00m },
                    { 12, "Front wheel hub bearing", true, "FAG", 4, "Wheel Hub Bearing", "DRV-003", 40.90m },
                    { 13, "Complete clutch kit", true, "Sachs", 5, "Clutch Kit", "TRN-001", 147.80m },
                    { 14, "Transmission support mount", true, "Febi", 5, "Gearbox Mount", "TRN-002", 27.80m },
                    { 15, "Gearbox shaft oil seal", true, "Elring", 5, "Transmission Oil Seal", "TRN-003", 7.60m },
                    { 16, "Timing belt with tensioner", true, "Gates", 6, "Timing Belt Kit", "BLT-001", 81.80m },
                    { 17, "Multi-rib auxiliary belt", true, "Contitech", 6, "Accessory Belt", "BLT-002", 14.60m },
                    { 18, "Automatic belt tensioner", true, "INA", 6, "Belt Tensioner", "BLT-003", 37.80m },
                    { 19, "Engine piston ring set", true, "Mahle", 7, "Piston Ring Set", "ENG-001", 49.10m },
                    { 20, "Right engine support mount", true, "Corteco", 7, "Engine Mount", "ENG-002", 45.20m },
                    { 21, "Steel engine oil pan", true, "Vaico", 7, "Oil Pan", "ENG-003", 53.40m },
                    { 22, "Cylinder head gasket set", true, "Elring", 8, "Cylinder Head Gasket", "CBP-001", 32.20m },
                    { 23, "Intake engine valve", true, "AE", 8, "Engine Valve", "CBP-002", 10.90m },
                    { 24, "Exhaust camshaft", true, "Kolbenschmidt", 8, "Camshaft", "CBP-003", 168.70m },
                    { 25, "Rubber valve cover gasket", true, "Victor Reinz", 9, "Valve Cover Gasket", "GAR-001", 10.20m },
                    { 26, "Crankshaft oil seal", true, "Elring", 9, "Oil Seal Ring", "GAR-002", 5.70m },
                    { 27, "Intake manifold gasket set", true, "Ajusa", 9, "Intake Manifold Gasket", "GAR-003", 8.40m },
                    { 28, "Engine cooling radiator", true, "Nissens", 10, "Radiator", "COL-001", 89.50m },
                    { 29, "Coolant thermostat", true, "Wahler", 10, "Thermostat", "COL-002", 17.80m },
                    { 30, "Mechanical engine water pump", true, "HEPU", 10, "Water Pump", "COL-003", 47.50m },
                    { 31, "Air conditioning compressor", true, "Denso", 11, "AC Compressor", "CLI-001", 215.00m },
                    { 32, "AC condenser radiator", true, "NRF", 11, "Condenser", "CLI-002", 75.00m },
                    { 33, "Interior temperature sensor", true, "Valeo", 11, "Cabin Temperature Sensor", "CLI-003", 14.30m },
                    { 34, "Passenger cabin heater radiator", true, "Valeo", 12, "Heater Core", "HTV-001", 60.30m },
                    { 35, "Cabin ventilation blower motor", true, "Bosch", 12, "Blower Motor", "HTV-002", 70.80m },
                    { 36, "Ventilation fan resistor", true, "Hella", 12, "Blower Resistor", "HTV-003", 21.60m },
                    { 37, "Engine oil filter cartridge", true, "MANN", 13, "Oil Filter", "FIL-001", 7.20m },
                    { 38, "Engine intake air filter", true, "Bosch", 13, "Air Filter", "FIL-002", 9.10m },
                    { 39, "Pollen cabin filter", true, "Mahle", 13, "Cabin Filter", "FIL-003", 11.40m },
                    { 40, "Fully synthetic engine oil 1L", true, "Castrol", 14, "Engine Oil 5W-30", "OIL-001", 9.60m },
                    { 41, "Brake fluid 500ml", true, "ATE", 14, "Brake Fluid DOT 4", "OIL-002", 6.40m },
                    { 42, "Ready to use coolant 1L", true, "Febi", 14, "Coolant G12", "OIL-003", 5.00m },
                    { 43, "Standard ignition spark plug", true, "NGK", 15, "Spark Plug", "IGN-001", 6.10m },
                    { 44, "Pencil ignition coil", true, "Bosch", 15, "Ignition Coil", "IGN-002", 32.70m },
                    { 45, "Diesel engine glow plug", true, "Beru", 15, "Glow Plug", "IGN-003", 9.40m },
                    { 46, "Wheel speed ABS sensor", true, "Hella", 16, "ABS Sensor", "SEN-001", 20.20m },
                    { 47, "Engine coolant temperature sensor", true, "Facet", 16, "Coolant Temperature Sensor", "SEN-002", 11.10m },
                    { 48, "Oil pressure warning switch", true, "Febi", 16, "Oil Pressure Switch", "SEN-003", 9.20m },
                    { 49, "Electric in-tank fuel pump", true, "Pierburg", 17, "Fuel Pump", "FUE-001", 78.70m },
                    { 50, "Petrol fuel injector", true, "Bosch", 17, "Fuel Injector", "FUE-002", 49.30m },
                    { 51, "Diesel fuel filter housing", true, "Mahle", 17, "Fuel Filter Housing", "FUE-003", 42.00m },
                    { 52, "Vehicle charging alternator", true, "Valeo", 18, "Alternator", "ELE-001", 158.50m },
                    { 53, "Positive battery terminal clamp", true, "Bosch", 18, "Battery Terminal", "ELE-002", 6.90m },
                    { 54, "Main vehicle fuse box", true, "Hella", 18, "Fuse Box", "ELE-003", 66.00m },
                    { 55, "12V starter motor", true, "Bosch", 19, "Starter Motor", "STA-001", 135.50m },
                    { 56, "Starter solenoid switch", true, "Valeo", 19, "Starter Solenoid", "STA-002", 25.50m },
                    { 57, "Maintenance-free starter battery", true, "Varta", 19, "Battery 74Ah", "STA-003", 96.60m },
                    { 58, "Front flat wiper blade", true, "Bosch", 20, "Wiper Blade Front", "WND-001", 8.60m },
                    { 59, "Windshield washer pump", true, "Febi", 20, "Washer Pump", "WND-002", 12.60m },
                    { 60, "Bonnet washer spray nozzle", true, "Vemo", 20, "Washer Nozzle", "WND-003", 4.50m },
                    { 61, "Rear silencer muffler", true, "Walker", 21, "Rear Muffler", "EXH-001", 73.70m },
                    { 62, "Emission catalytic converter", true, "BM Catalysts", 21, "Catalytic Converter", "EXH-002", 199.40m },
                    { 63, "Steel exhaust clamp", true, "Bosal", 21, "Exhaust Pipe Clamp", "EXH-003", 5.80m },
                    { 64, "Primed front bumper", true, "TYC", 22, "Front Bumper", "LRG-001", 112.50m },
                    { 65, "Front bonnet hood panel", true, "BLIC", 22, "Engine Hood", "LRG-002", 161.00m },
                    { 66, "Left front wing panel", true, "Van Wezel", 22, "Front Fender", "LRG-003", 65.70m },
                    { 67, "Front left headlight", true, "Hella", 23, "Headlight Assembly", "LGT-001", 126.80m },
                    { 68, "Rear right tail lamp", true, "Depo", 23, "Tail Light", "LGT-002", 47.40m },
                    { 69, "Front bumper fog light", true, "Valeo", 23, "Fog Light", "LGT-003", 24.20m },
                    { 70, "Driver electric window switch", true, "Vemo", 24, "Window Switch", "INT-001", 19.90m },
                    { 71, "Manual gear lever knob", true, "Topran", 24, "Gear Shift Knob", "INT-002", 13.70m },
                    { 72, "Textile interior floor mat set", true, "Petex", 24, "Floor Mat Set", "INT-003", 22.80m }
                });

            migrationBuilder.InsertData(
                table: "PartImages",
                columns: new[] { "PartImageId", "ImageUrl", "PartId", "PublicId" },
                values: new object[,]
                {
                    { 1, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203068/kachaowauto/parts/rxdv9gqcsp49mzwnr8fm.jpg", 46, null },
                    { 2, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203091/kachaowauto/parts/dyaie8c7ynikcuuiiqej.jpg", 31, null },
                    { 3, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203105/kachaowauto/parts/thsbcczibnh8spsonmun.jpg", 17, null },
                    { 4, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203119/kachaowauto/parts/yithggzz3o2zkj5u9hnx.jpg", 38, null },
                    { 5, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203132/kachaowauto/parts/vndxtrtn3esyyjv7jhzf.jpg", 52, null },
                    { 6, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203146/kachaowauto/parts/ht6zvgispnz7boxh4wgu.jpg", 57, null },
                    { 7, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203160/kachaowauto/parts/sncuh48velbvyklkdq5v.jpg", 53, null },
                    { 8, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203178/kachaowauto/parts/tvmrxpwggen9jgknoslf.jpg", 18, null },
                    { 9, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203195/kachaowauto/parts/bujpr6ehhp0pbuh30ms9.webp", 35, null },
                    { 10, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203217/kachaowauto/parts/jdxc2llawr96q6ql5mdq.jpg", 36, null },
                    { 11, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203238/kachaowauto/parts/bfxmv7swtnw6w9erxcr9.jpg", 3, null },
                    { 12, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203272/kachaowauto/parts/zlts30rzbgs89u2ccud7.png", 41, null },
                    { 13, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203292/kachaowauto/parts/xt81fregkhoych98wyzp.jpg", 39, null },
                    { 14, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203308/kachaowauto/parts/qkfyhgtat87yugbepy2a.jpg", 33, null },
                    { 15, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203322/kachaowauto/parts/jeljogguchewmrbnaasz.jpg", 24, null },
                    { 16, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203343/kachaowauto/parts/nwkaczxwxkvxk5suavih.jpg", 62, null },
                    { 17, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203361/kachaowauto/parts/vumoqfreegd1hqbx0has.jpg", 13, null },
                    { 18, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203382/kachaowauto/parts/rn75chixkuglku9vpxdl.jpg", 5, null },
                    { 19, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203404/kachaowauto/parts/dmzvjdiumrrtr9gwirrg.png", 32, null },
                    { 20, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203420/kachaowauto/parts/nlrh5syefrsisrprwtjg.jpg", 6, null },
                    { 21, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203455/kachaowauto/parts/cdcnkcn66hmiahydsa6g.jpg", 42, null },
                    { 22, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203471/kachaowauto/parts/ssmujpnktrnwlumsiwdf.jpg", 47, null },
                    { 23, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203484/kachaowauto/parts/y2xwug8ei0bagohlmzih.jpg", 10, null },
                    { 24, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203501/kachaowauto/parts/ji3y6ckpqvkx0ncdeckb.jpg", 22, null },
                    { 25, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203517/kachaowauto/parts/wsdyppcwpq5qnhryzpep.jpg", 11, null },
                    { 26, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203530/kachaowauto/parts/dijymhgyehpdynxtxnf4.jpg", 65, null },
                    { 27, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203546/kachaowauto/parts/ouwvhhzwqoecfgzaqbcm.webp", 20, null },
                    { 28, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203563/kachaowauto/parts/ipfbfxodv2mzj1h4ehao.jpg", 40, null },
                    { 29, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203578/kachaowauto/parts/ytthk46e5d3cbt7b9akx.jpg", 23, null },
                    { 30, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203593/kachaowauto/parts/z8rvp2ms7ovhacwzmayy.jpg", 63, null },
                    { 31, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203612/kachaowauto/parts/ukzaersts5ipw470bi4h.jpg", 72, null },
                    { 32, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203627/kachaowauto/parts/ixprjnnvizvf4bvgnlme.jpg", 69, null },
                    { 33, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203644/kachaowauto/parts/t25efpowdpqqkxkqwood.jpg", 1, null },
                    { 34, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203689/kachaowauto/parts/y8f3sgbhfuvjfcdfpjcd.jpg", 64, null },
                    { 35, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203706/kachaowauto/parts/pdbutiihcffewgcdom4v.jpg", 66, null },
                    { 36, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203723/kachaowauto/parts/wjdtqluh76k393pbxnyn.png", 4, null },
                    { 37, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203738/kachaowauto/parts/dv2p6cqx1hg6i9qjw2tq.jpg", 51, null },
                    { 38, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203753/kachaowauto/parts/dfh1d5qsif2tkmsjkpza.jpg", 50, null },
                    { 39, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203768/kachaowauto/parts/oo44aglswf9a2cmdyj3m.jpg", 49, null },
                    { 40, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203785/kachaowauto/parts/yt3ymqwxtihnib6ifprt.webp", 54, null },
                    { 41, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203799/kachaowauto/parts/t6gcjntghjigyip1m69v.jpg", 71, null },
                    { 42, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203815/kachaowauto/parts/yenwkg39tofq4hk81grd.jpg", 14, null },
                    { 43, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203831/kachaowauto/parts/qmyxyichuxdtilwvgvlq.jpg", 45, null },
                    { 44, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203849/kachaowauto/parts/j8oiuzmfyyesv1fzug8t.jpg", 67, null },
                    { 45, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203865/kachaowauto/parts/xccjb68lzprsfwvlqeqb.jpg", 34, null },
                    { 46, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203879/kachaowauto/parts/rkj5jzvop0lwdfxiuysb.webp", 44, null },
                    { 47, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203893/kachaowauto/parts/siebcpnmhkhnymukondx.jpg", 27, null },
                    { 48, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203907/kachaowauto/parts/pdfgoupg7g27gqnhzkvg.jpg", 37, null },
                    { 49, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203919/kachaowauto/parts/qrvj9ozdxypwclm4eg1l.jpg", 21, null },
                    { 50, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203940/kachaowauto/parts/cdcpexft9oh6udsajtaj.jpg", 48, null },
                    { 51, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203953/kachaowauto/parts/g0xmmbicapjshtspmlwd.jpg", 26, null },
                    { 52, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203966/kachaowauto/parts/nfeadizk6x4hhwc2whow.jpg", 19, null },
                    { 53, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203983/kachaowauto/parts/re5htadyrnwksvbmbqsb.jpg", 9, null },
                    { 54, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204051/kachaowauto/parts/ult6oomfcv3obgzsl3vk.jpg", 28, null },
                    { 55, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204072/kachaowauto/parts/xllkggxe8ew0kj8fithj.webp", 2, null },
                    { 56, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204090/kachaowauto/parts/cbtdb5xy5uvigtpclko0.webp", 61, null },
                    { 57, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204107/kachaowauto/parts/j5jah8qey83t6sv4gobn.jpg", 43, null },
                    { 58, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204124/kachaowauto/parts/i7m7obk1znldiuuas7ug.jpg", 55, null },
                    { 59, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204141/kachaowauto/parts/kfxp6ovjfse2nui00u0f.jpg", 56, null },
                    { 60, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204154/kachaowauto/parts/s8ntegg3rijw3jc7cll4.jpg", 8, null },
                    { 61, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204166/kachaowauto/parts/z8kqhc1wabjcib69unga.jpg", 68, null },
                    { 62, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204177/kachaowauto/parts/kaz5x9yhzdjet69tbqm0.jpg", 29, null },
                    { 63, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204190/kachaowauto/parts/owhxlxurctitks4idvyt.jpg", 7, null },
                    { 64, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204206/kachaowauto/parts/s98imtmuiemnofk76xhc.webp", 16, null },
                    { 65, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204218/kachaowauto/parts/fwohy27jwr4pyryxbulm.jpg", 15, null },
                    { 66, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204233/kachaowauto/parts/mf080slgwwvxnqpgi2pl.jpg", 25, null },
                    { 67, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204244/kachaowauto/parts/jyzsutg9byobfgdrfowt.jpg", 60, null },
                    { 68, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204257/kachaowauto/parts/uajxfhby6rmd7sbz0j1m.jpg", 59, null },
                    { 69, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204277/kachaowauto/parts/eimtbih0nu4qpnzdqmsk.png", 30, null },
                    { 70, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204292/kachaowauto/parts/rtxlaqkrt64ozn3uk7ki.jpg", 12, null },
                    { 71, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204307/kachaowauto/parts/e9pws6zcdlensddztbxz.jpg", 70, null },
                    { 72, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204324/kachaowauto/parts/dmle30bgcgxbm5pjer9t.webp", 58, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentMechanics_MechanicId",
                table: "AppointmentMechanics",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParts_PartId",
                table: "AppointmentParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CarId",
                table: "Appointments",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_WorkshopId",
                table: "Appointments",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_BrandName",
                table: "Brands",
                column: "BrandName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_VIN",
                table: "Cars",
                column: "VIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BodyTypeId",
                table: "Models",
                column: "BodyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId_ModelName_EngineTypeId_EngineVolume_HorsePower_BodyTypeId",
                table: "Models",
                columns: new[] { "BrandId", "ModelName", "EngineTypeId", "EngineVolume", "HorsePower", "BodyTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_EngineTypeId",
                table: "Models",
                column: "EngineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartImages_PartId",
                table: "PartImages",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartRequests_MechanicId",
                table: "PartRequests",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_PartRequests_PartId",
                table: "PartRequests",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Manufacturer_PartNumber",
                table: "Parts",
                columns: new[] { "Manufacturer", "PartNumber" },
                unique: true,
                filter: "[Manufacturer] IS NOT NULL AND [PartNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartCategoryId",
                table: "Parts",
                column: "PartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCategoryId",
                table: "Services",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_RegionId_City",
                table: "Workshops",
                columns: new[] { "RegionId", "City" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopServices_ServiceId",
                table: "WorkshopServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentMechanics");

            migrationBuilder.DropTable(
                name: "AppointmentParts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PartImages");

            migrationBuilder.DropTable(
                name: "PartRequests");

            migrationBuilder.DropTable(
                name: "WorkshopServices");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "AppointmentStatuses");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Workshops");

            migrationBuilder.DropTable(
                name: "PartCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "BodyTypes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "EngineTypes");
        }
    }
}
