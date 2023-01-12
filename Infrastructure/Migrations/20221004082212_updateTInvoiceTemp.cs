using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateTInvoiceTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "InvoiceTemps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemps_BranchId",
                table: "InvoiceTemps",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemps_Branches_BranchId",
                table: "InvoiceTemps",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemps_Branches_BranchId",
                table: "InvoiceTemps");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemps_BranchId",
                table: "InvoiceTemps");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "InvoiceTemps");
        }
    }
}
