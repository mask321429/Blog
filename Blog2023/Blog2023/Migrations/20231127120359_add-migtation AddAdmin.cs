using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations
{
    /// <inheritdoc />
    public partial class addmigtationAddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministratorDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CommunityModelId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministratorDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministratorDTO_Community_CommunityModelId",
                        column: x => x.CommunityModelId,
                        principalTable: "Community",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministratorDTO_CommunityModelId",
                table: "AdministratorDTO",
                column: "CommunityModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministratorDTO");
        }
    }
}
