using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KachaowAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPartRequestConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_AspNetUsers_MechanicId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_AspNetUsers_MechanicId",
                table: "PartRequests",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_AspNetUsers_MechanicId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_AspNetUsers_MechanicId",
                table: "PartRequests",
                column: "MechanicId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
