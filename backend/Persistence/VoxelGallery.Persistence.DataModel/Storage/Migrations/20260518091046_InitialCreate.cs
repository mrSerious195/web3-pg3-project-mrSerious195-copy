using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxelGallery.Persistence.DataModel.Storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VoxData");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "VoxData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityServerId = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scenes",
                schema: "VoxData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraPosX = table.Column<int>(type: "int", nullable: false),
                    CameraPosY = table.Column<int>(type: "int", nullable: false),
                    CameraPosZ = table.Column<int>(type: "int", nullable: false),
                    CameraLookatX = table.Column<int>(type: "int", nullable: false),
                    CameraLookatY = table.Column<int>(type: "int", nullable: false),
                    CameraLookatZ = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploaderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scenes_Users_UploaderId",
                        column: x => x.UploaderId,
                        principalSchema: "VoxData",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "VoxData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SceneID = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Scenes_SceneID",
                        column: x => x.SceneID,
                        principalSchema: "VoxData",
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "VoxData",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                schema: "VoxData",
                columns: table => new
                {
                    FavoritesId = table.Column<long>(type: "bigint", nullable: false),
                    FavoritesId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.FavoritesId, x.FavoritesId1 });
                    table.ForeignKey(
                        name: "FK_Favorites_Scenes_FavoritesId",
                        column: x => x.FavoritesId,
                        principalSchema: "VoxData",
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_FavoritesId1",
                        column: x => x.FavoritesId1,
                        principalSchema: "VoxData",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                schema: "VoxData",
                columns: table => new
                {
                    LikesId = table.Column<long>(type: "bigint", nullable: false),
                    LikesId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.LikesId, x.LikesId1 });
                    table.ForeignKey(
                        name: "FK_Likes_Scenes_LikesId",
                        column: x => x.LikesId,
                        principalSchema: "VoxData",
                        principalTable: "Scenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_LikesId1",
                        column: x => x.LikesId1,
                        principalSchema: "VoxData",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SceneID",
                schema: "VoxData",
                table: "Comments",
                column: "SceneID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                schema: "VoxData",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FavoritesId1",
                schema: "VoxData",
                table: "Favorites",
                column: "FavoritesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikesId1",
                schema: "VoxData",
                table: "Likes",
                column: "LikesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_UploaderId",
                schema: "VoxData",
                table: "Scenes",
                column: "UploaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "VoxData");

            migrationBuilder.DropTable(
                name: "Favorites",
                schema: "VoxData");

            migrationBuilder.DropTable(
                name: "Likes",
                schema: "VoxData");

            migrationBuilder.DropTable(
                name: "Scenes",
                schema: "VoxData");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "VoxData");
        }
    }
}
