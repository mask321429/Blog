using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog2023.Migrations.ApplicationDbContextAddressMigrations
{
    /// <inheritdoc />
    public partial class addAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "as_addr_objs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Objectid = table.Column<long>(type: "bigint", nullable: false),
                    Objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    Changeid = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Typename = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Opertypeid = table.Column<int>(type: "integer", nullable: true),
                    Previd = table.Column<long>(type: "bigint", nullable: true),
                    Nextid = table.Column<long>(type: "bigint", nullable: true),
                    Updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    Startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    Isactual = table.Column<int>(type: "integer", nullable: true),
                    Isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_addr_objs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierarchys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Objectid = table.Column<long>(type: "bigint", nullable: true),
                    Parentobjid = table.Column<long>(type: "bigint", nullable: true),
                    Changeid = table.Column<long>(type: "bigint", nullable: true),
                    Regioncode = table.Column<string>(type: "text", nullable: true),
                    Areacode = table.Column<string>(type: "text", nullable: true),
                    Citycode = table.Column<string>(type: "text", nullable: true),
                    Placecode = table.Column<string>(type: "text", nullable: true),
                    Plancode = table.Column<string>(type: "text", nullable: true),
                    Streetcode = table.Column<string>(type: "text", nullable: true),
                    Previd = table.Column<long>(type: "bigint", nullable: true),
                    Nextid = table.Column<long>(type: "bigint", nullable: true),
                    Updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    Startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    Isactive = table.Column<int>(type: "integer", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_adm_hierarchys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "as_housess",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Objectid = table.Column<long>(type: "bigint", nullable: false),
                    Objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    Changeid = table.Column<long>(type: "bigint", nullable: true),
                    Housenum = table.Column<string>(type: "text", nullable: true),
                    Addnum1 = table.Column<string>(type: "text", nullable: true),
                    Addnum2 = table.Column<string>(type: "text", nullable: true),
                    Housetype = table.Column<int>(type: "integer", nullable: true),
                    Addtype1 = table.Column<int>(type: "integer", nullable: true),
                    Addtype2 = table.Column<int>(type: "integer", nullable: true),
                    Opertypeid = table.Column<int>(type: "integer", nullable: true),
                    Previd = table.Column<long>(type: "bigint", nullable: true),
                    Nextid = table.Column<long>(type: "bigint", nullable: true),
                    Updatedate = table.Column<DateOnly>(type: "date", nullable: true),
                    Startdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Enddate = table.Column<DateOnly>(type: "date", nullable: true),
                    Isactual = table.Column<int>(type: "integer", nullable: true),
                    Isactive = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_housess", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_objs");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchys");

            migrationBuilder.DropTable(
                name: "as_housess");
        }
    }
}
