using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog2023.Migrations.ApplicationDbContextAddressMigrations
{
    /// <inheritdoc />
    public partial class renameAddressNumberTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updatedate",
                table: "as_houses",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "Startdate",
                table: "as_houses",
                newName: "startdate");

            migrationBuilder.RenameColumn(
                name: "Previd",
                table: "as_houses",
                newName: "previd");

            migrationBuilder.RenameColumn(
                name: "Opertypeid",
                table: "as_houses",
                newName: "opertypeid");

            migrationBuilder.RenameColumn(
                name: "Objectid",
                table: "as_houses",
                newName: "objectid");

            migrationBuilder.RenameColumn(
                name: "Objectguid",
                table: "as_houses",
                newName: "objectguid");

            migrationBuilder.RenameColumn(
                name: "Nextid",
                table: "as_houses",
                newName: "nextid");

            migrationBuilder.RenameColumn(
                name: "Isactual",
                table: "as_houses",
                newName: "isactual");

            migrationBuilder.RenameColumn(
                name: "Isactive",
                table: "as_houses",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "Housetype",
                table: "as_houses",
                newName: "housetype");

            migrationBuilder.RenameColumn(
                name: "Housenum",
                table: "as_houses",
                newName: "housenum");

            migrationBuilder.RenameColumn(
                name: "Enddate",
                table: "as_houses",
                newName: "enddate");

            migrationBuilder.RenameColumn(
                name: "Changeid",
                table: "as_houses",
                newName: "changeid");

            migrationBuilder.RenameColumn(
                name: "Addtype2",
                table: "as_houses",
                newName: "addtype2");

            migrationBuilder.RenameColumn(
                name: "Addtype1",
                table: "as_houses",
                newName: "addtype1");

            migrationBuilder.RenameColumn(
                name: "Addnum2",
                table: "as_houses",
                newName: "addnum2");

            migrationBuilder.RenameColumn(
                name: "Addnum1",
                table: "as_houses",
                newName: "addnum1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "as_houses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Updatedate",
                table: "as_adm_hierarchy",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "Streetcode",
                table: "as_adm_hierarchy",
                newName: "streetcode");

            migrationBuilder.RenameColumn(
                name: "Startdate",
                table: "as_adm_hierarchy",
                newName: "startdate");

            migrationBuilder.RenameColumn(
                name: "Regioncode",
                table: "as_adm_hierarchy",
                newName: "regioncode");

            migrationBuilder.RenameColumn(
                name: "Previd",
                table: "as_adm_hierarchy",
                newName: "previd");

            migrationBuilder.RenameColumn(
                name: "Plancode",
                table: "as_adm_hierarchy",
                newName: "plancode");

            migrationBuilder.RenameColumn(
                name: "Placecode",
                table: "as_adm_hierarchy",
                newName: "placecode");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "as_adm_hierarchy",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "Parentobjid",
                table: "as_adm_hierarchy",
                newName: "parentobjid");

            migrationBuilder.RenameColumn(
                name: "Objectid",
                table: "as_adm_hierarchy",
                newName: "objectid");

            migrationBuilder.RenameColumn(
                name: "Nextid",
                table: "as_adm_hierarchy",
                newName: "nextid");

            migrationBuilder.RenameColumn(
                name: "Isactive",
                table: "as_adm_hierarchy",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "Enddate",
                table: "as_adm_hierarchy",
                newName: "enddate");

            migrationBuilder.RenameColumn(
                name: "Citycode",
                table: "as_adm_hierarchy",
                newName: "citycode");

            migrationBuilder.RenameColumn(
                name: "Changeid",
                table: "as_adm_hierarchy",
                newName: "changeid");

            migrationBuilder.RenameColumn(
                name: "Areacode",
                table: "as_adm_hierarchy",
                newName: "areacode");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "as_adm_hierarchy",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Updatedate",
                table: "as_addr_obj",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "Typename",
                table: "as_addr_obj",
                newName: "typename");

            migrationBuilder.RenameColumn(
                name: "Startdate",
                table: "as_addr_obj",
                newName: "startdate");

            migrationBuilder.RenameColumn(
                name: "Previd",
                table: "as_addr_obj",
                newName: "previd");

            migrationBuilder.RenameColumn(
                name: "Opertypeid",
                table: "as_addr_obj",
                newName: "opertypeid");

            migrationBuilder.RenameColumn(
                name: "Objectid",
                table: "as_addr_obj",
                newName: "objectid");

            migrationBuilder.RenameColumn(
                name: "Objectguid",
                table: "as_addr_obj",
                newName: "objectguid");

            migrationBuilder.RenameColumn(
                name: "Nextid",
                table: "as_addr_obj",
                newName: "nextid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "as_addr_obj",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "as_addr_obj",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "Isactual",
                table: "as_addr_obj",
                newName: "isactual");

            migrationBuilder.RenameColumn(
                name: "Isactive",
                table: "as_addr_obj",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "Enddate",
                table: "as_addr_obj",
                newName: "enddate");

            migrationBuilder.RenameColumn(
                name: "Changeid",
                table: "as_addr_obj",
                newName: "changeid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "as_addr_obj",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "as_houses",
                newName: "Updatedate");

            migrationBuilder.RenameColumn(
                name: "startdate",
                table: "as_houses",
                newName: "Startdate");

            migrationBuilder.RenameColumn(
                name: "previd",
                table: "as_houses",
                newName: "Previd");

            migrationBuilder.RenameColumn(
                name: "opertypeid",
                table: "as_houses",
                newName: "Opertypeid");

            migrationBuilder.RenameColumn(
                name: "objectid",
                table: "as_houses",
                newName: "Objectid");

            migrationBuilder.RenameColumn(
                name: "objectguid",
                table: "as_houses",
                newName: "Objectguid");

            migrationBuilder.RenameColumn(
                name: "nextid",
                table: "as_houses",
                newName: "Nextid");

            migrationBuilder.RenameColumn(
                name: "isactual",
                table: "as_houses",
                newName: "Isactual");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "as_houses",
                newName: "Isactive");

            migrationBuilder.RenameColumn(
                name: "housetype",
                table: "as_houses",
                newName: "Housetype");

            migrationBuilder.RenameColumn(
                name: "housenum",
                table: "as_houses",
                newName: "Housenum");

            migrationBuilder.RenameColumn(
                name: "enddate",
                table: "as_houses",
                newName: "Enddate");

            migrationBuilder.RenameColumn(
                name: "changeid",
                table: "as_houses",
                newName: "Changeid");

            migrationBuilder.RenameColumn(
                name: "addtype2",
                table: "as_houses",
                newName: "Addtype2");

            migrationBuilder.RenameColumn(
                name: "addtype1",
                table: "as_houses",
                newName: "Addtype1");

            migrationBuilder.RenameColumn(
                name: "addnum2",
                table: "as_houses",
                newName: "Addnum2");

            migrationBuilder.RenameColumn(
                name: "addnum1",
                table: "as_houses",
                newName: "Addnum1");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "as_houses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "as_adm_hierarchy",
                newName: "Updatedate");

            migrationBuilder.RenameColumn(
                name: "streetcode",
                table: "as_adm_hierarchy",
                newName: "Streetcode");

            migrationBuilder.RenameColumn(
                name: "startdate",
                table: "as_adm_hierarchy",
                newName: "Startdate");

            migrationBuilder.RenameColumn(
                name: "regioncode",
                table: "as_adm_hierarchy",
                newName: "Regioncode");

            migrationBuilder.RenameColumn(
                name: "previd",
                table: "as_adm_hierarchy",
                newName: "Previd");

            migrationBuilder.RenameColumn(
                name: "plancode",
                table: "as_adm_hierarchy",
                newName: "Plancode");

            migrationBuilder.RenameColumn(
                name: "placecode",
                table: "as_adm_hierarchy",
                newName: "Placecode");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "as_adm_hierarchy",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "parentobjid",
                table: "as_adm_hierarchy",
                newName: "Parentobjid");

            migrationBuilder.RenameColumn(
                name: "objectid",
                table: "as_adm_hierarchy",
                newName: "Objectid");

            migrationBuilder.RenameColumn(
                name: "nextid",
                table: "as_adm_hierarchy",
                newName: "Nextid");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "as_adm_hierarchy",
                newName: "Isactive");

            migrationBuilder.RenameColumn(
                name: "enddate",
                table: "as_adm_hierarchy",
                newName: "Enddate");

            migrationBuilder.RenameColumn(
                name: "citycode",
                table: "as_adm_hierarchy",
                newName: "Citycode");

            migrationBuilder.RenameColumn(
                name: "changeid",
                table: "as_adm_hierarchy",
                newName: "Changeid");

            migrationBuilder.RenameColumn(
                name: "areacode",
                table: "as_adm_hierarchy",
                newName: "Areacode");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "as_adm_hierarchy",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "as_addr_obj",
                newName: "Updatedate");

            migrationBuilder.RenameColumn(
                name: "typename",
                table: "as_addr_obj",
                newName: "Typename");

            migrationBuilder.RenameColumn(
                name: "startdate",
                table: "as_addr_obj",
                newName: "Startdate");

            migrationBuilder.RenameColumn(
                name: "previd",
                table: "as_addr_obj",
                newName: "Previd");

            migrationBuilder.RenameColumn(
                name: "opertypeid",
                table: "as_addr_obj",
                newName: "Opertypeid");

            migrationBuilder.RenameColumn(
                name: "objectid",
                table: "as_addr_obj",
                newName: "Objectid");

            migrationBuilder.RenameColumn(
                name: "objectguid",
                table: "as_addr_obj",
                newName: "Objectguid");

            migrationBuilder.RenameColumn(
                name: "nextid",
                table: "as_addr_obj",
                newName: "Nextid");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "as_addr_obj",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "as_addr_obj",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "isactual",
                table: "as_addr_obj",
                newName: "Isactual");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "as_addr_obj",
                newName: "Isactive");

            migrationBuilder.RenameColumn(
                name: "enddate",
                table: "as_addr_obj",
                newName: "Enddate");

            migrationBuilder.RenameColumn(
                name: "changeid",
                table: "as_addr_obj",
                newName: "Changeid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "as_addr_obj",
                newName: "Id");
        }
    }
}
