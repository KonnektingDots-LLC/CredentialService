using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInsuranceCarrierList3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Malpractice_MalpracticeCarrierList_MalpracticeCarrierId",
                table: "Malpractice",
                column: "MalpracticeCarrierId",
                principalTable: "MalpracticeCarrierList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);



            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalLiability_ProfessionalCarrierList_ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability",
                column: "ProfessionalLiabilityCarrierId",
                principalTable: "ProfessionalCarrierList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
