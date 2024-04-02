using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations
{
    /// <inheritdoc />
    public partial class idForAuthorPk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");
        }
    }
}
