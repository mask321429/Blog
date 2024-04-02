using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations
{
    /// <inheritdoc />
    public partial class AddTagPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostModelId1",
                table: "Tag",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CommunityModelId",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tagPost",
                columns: table => new
                {
                    IdPost = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTeg = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tagPost", x => new { x.IdPost, x.IdTeg });
                    table.ForeignKey(
                        name: "FK_tagPost_Posts_IdPost",
                        column: x => x.IdPost,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tagPost_Tag_IdTeg",
                        column: x => x.IdTeg,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostModelId1",
                table: "Tag",
                column: "PostModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommunityModelId",
                table: "Posts",
                column: "CommunityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tagPost_IdTeg",
                table: "tagPost",
                column: "IdTeg");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Community_CommunityModelId",
                table: "Posts",
                column: "CommunityModelId",
                principalTable: "Community",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Posts_PostModelId1",
                table: "Tag",
                column: "PostModelId1",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Community_CommunityModelId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Posts_PostModelId1",
                table: "Tag");

            migrationBuilder.DropTable(
                name: "tagPost");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostModelId1",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CommunityModelId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostModelId1",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CommunityModelId",
                table: "Posts");
        }
    }
}
