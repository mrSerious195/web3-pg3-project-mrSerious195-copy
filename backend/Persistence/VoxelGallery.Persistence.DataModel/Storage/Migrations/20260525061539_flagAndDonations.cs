using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxelGallery.Persistence.DataModel.Storage.Migrations
{
    /// <inheritdoc />
    public partial class flagAndDonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flag",
                schema: "VoxData",
                table: "Users",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDonor",
                schema: "VoxData",
                table: "Users",
                type: "bit",
                maxLength: 1,
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                schema: "VoxData",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDonor",
                schema: "VoxData",
                table: "Users");
        }
    }
}
