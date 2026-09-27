using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxelGallery.Persistence.DataModel.Storage.Migrations
{
    /// <inheritdoc />
    public partial class CountryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "VoxData",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                schema: "VoxData",
                table: "Users");
        }
    }
}
