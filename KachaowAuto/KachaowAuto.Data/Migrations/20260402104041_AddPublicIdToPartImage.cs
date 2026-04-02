using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KachaowAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicIdToPartImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "PartImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "PartImages");
        }
    }
}
