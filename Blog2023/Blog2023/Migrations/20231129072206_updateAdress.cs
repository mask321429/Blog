using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations
{
    /// <inheritdoc />
    public partial class updateAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Posts_PostModelId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Posts_PostModelId1",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostModelId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostModelId1",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "PostModelId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "PostModelId1",
                table: "Tag");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Posts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PostModelTagModel",
                columns: table => new
                {
                    PostsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostModelTagModel", x => new { x.PostsId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostModelTagModel_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostModelTagModel_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostModelTagModel_TagId",
                table: "PostModelTagModel",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostModelTagModel");

            migrationBuilder.AddColumn<Guid>(
                name: "PostModelId",
                table: "Tag",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PostModelId1",
                table: "Tag",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Posts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostModelId",
                table: "Tag",
                column: "PostModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostModelId1",
                table: "Tag",
                column: "PostModelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Posts_PostModelId",
                table: "Tag",
                column: "PostModelId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Posts_PostModelId1",
                table: "Tag",
                column: "PostModelId1",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
