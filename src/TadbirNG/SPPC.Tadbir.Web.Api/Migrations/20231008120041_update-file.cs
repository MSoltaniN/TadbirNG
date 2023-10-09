using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class updatefile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "ProductScope",
                table: "File");

            migrationBuilder.DropColumn(
                name: "BranchScope",
                schema: "ProductScope",
                table: "File");

            migrationBuilder.DropColumn(
                name: "FiscalPeriodId",
                schema: "ProductScope",
                table: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                schema: "ProductScope",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "BranchScope",
                schema: "ProductScope",
                table: "File",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "FiscalPeriodId",
                schema: "ProductScope",
                table: "File",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
