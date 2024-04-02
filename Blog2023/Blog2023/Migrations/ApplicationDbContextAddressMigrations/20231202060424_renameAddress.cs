using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations.ApplicationDbContextAddressMigrations
{
    /// <inheritdoc />
    public partial class renameAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_as_housess",
                table: "as_housess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_adm_hierarchys",
                table: "as_adm_hierarchys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_addr_objs",
                table: "as_addr_objs");

            migrationBuilder.RenameTable(
                name: "as_housess",
                newName: "as_houses");

            migrationBuilder.RenameTable(
                name: "as_adm_hierarchys",
                newName: "as_adm_hierarchy");

            migrationBuilder.RenameTable(
                name: "as_addr_objs",
                newName: "as_addr_obj");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_houses",
                table: "as_houses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_adm_hierarchy",
                table: "as_adm_hierarchy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_addr_obj",
                table: "as_addr_obj",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_as_houses",
                table: "as_houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_adm_hierarchy",
                table: "as_adm_hierarchy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_addr_obj",
                table: "as_addr_obj");

            migrationBuilder.RenameTable(
                name: "as_houses",
                newName: "as_housess");

            migrationBuilder.RenameTable(
                name: "as_adm_hierarchy",
                newName: "as_adm_hierarchys");

            migrationBuilder.RenameTable(
                name: "as_addr_obj",
                newName: "as_addr_objs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_housess",
                table: "as_housess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_adm_hierarchys",
                table: "as_adm_hierarchys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_addr_objs",
                table: "as_addr_objs",
                column: "Id");
        }
    }
}
