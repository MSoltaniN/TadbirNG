using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class removeforignKeyfiscalperiodunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_FiscalPeriod_FiscalPeriodId",
                schema: "ProductScope",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_FiscalPeriodId",
                schema: "ProductScope",
                table: "Unit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unit_FiscalPeriodId",
                schema: "ProductScope",
                table: "Unit",
                column: "FiscalPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_FiscalPeriod_FiscalPeriodId",
                schema: "ProductScope",
                table: "Unit",
                column: "FiscalPeriodId",
                principalSchema: "Finance",
                principalTable: "FiscalPeriod",
                principalColumn: "FiscalPeriodID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
