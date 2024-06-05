using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessionalLiabilityCarrierOther",
                table: "ProfessionalLiability",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MalpracticeCarrierOther",
                table: "Malpractice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "hospitalPrivilegesTypeOther",
                table: "Hospital",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "entityTypeOther",
                table: "Corporation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalLiabilityCarrierOther",
                table: "ProfessionalLiability");

            migrationBuilder.DropColumn(
                name: "MalpracticeCarrierOther",
                table: "Malpractice");

            migrationBuilder.DropColumn(
                name: "hospitalPrivilegesTypeOther",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "entityTypeOther",
                table: "Corporation");
        }
    }
}
