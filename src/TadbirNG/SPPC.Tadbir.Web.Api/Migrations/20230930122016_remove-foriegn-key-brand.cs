using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class removeforiegnkeybrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_FiscalPeriod_FiscalPeriodId",
                schema: "ProductScope",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_FiscalPeriodId",
                schema: "ProductScope",
                table: "Brand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Brand_FiscalPeriodId",
                schema: "ProductScope",
                table: "Brand",
                column: "FiscalPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_FiscalPeriod_FiscalPeriodId",
                schema: "ProductScope",
                table: "Brand",
                column: "FiscalPeriodId",
                principalSchema: "Finance",
                principalTable: "FiscalPeriod",
                principalColumn: "FiscalPeriodID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
