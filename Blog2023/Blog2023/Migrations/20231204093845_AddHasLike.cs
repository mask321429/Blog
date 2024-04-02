using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations
{
    /// <inheritdoc />
    public partial class AddHasLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostModelId",
                table: "Tag",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostModelId",
                table: "Tag",
                column: "PostModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Posts_PostModelId",
                table: "Tag",
                column: "PostModelId",
                principalTable: "Posts",
                 principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Posts_PostModelId",
                table: "Tag");

            migrationBuilder.DropTable(
                name: "UserLikes");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostModelId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "Tag");
        }
    }
}
