using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInsuranceCarrierList2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceCarrierId",
                table: "ProfessionalLiability");

            migrationBuilder.DropColumn(
                name: "InsuranceCarrierId",
                table: "Malpractice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsuranceCarrierId",
                table: "ProfessionalLiability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceCarrierId",
                table: "Malpractice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
