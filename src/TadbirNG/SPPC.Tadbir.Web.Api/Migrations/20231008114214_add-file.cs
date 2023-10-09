using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPPC.Tadbir.Web.Api.Migrations
{
    public partial class addfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                schema: "ProductScope",
                columns: table => new
                {
                    FileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UniqeName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    rowguid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiscalPeriodId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    BranchScope = table.Column<short>(type: "smallint", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File",
                schema: "ProductScope");
        }
    }
}
