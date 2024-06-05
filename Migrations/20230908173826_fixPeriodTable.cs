using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class fixPeriodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfo_PeriodEntity_EducationPeriodId",
                table: "EducationInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_PeriodEntity_HospitalPriviledgePeriodId",
                table: "Hospital");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodEntity",
                table: "PeriodEntity");

            migrationBuilder.RenameTable(
                name: "PeriodEntity",
                newName: "Period");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Period",
                table: "Period",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfo_Period_EducationPeriodId",
                table: "EducationInfo",
                column: "EducationPeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_Period_HospitalPriviledgePeriodId",
                table: "Hospital",
                column: "HospitalPriviledgePeriodId",
                principalTable: "Period",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationInfo_Period_EducationPeriodId",
                table: "EducationInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospital_Period_HospitalPriviledgePeriodId",
                table: "Hospital");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Period",
                table: "Period");

            migrationBuilder.RenameTable(
                name: "Period",
                newName: "PeriodEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodEntity",
                table: "PeriodEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationInfo_PeriodEntity_EducationPeriodId",
                table: "EducationInfo",
                column: "EducationPeriodId",
                principalTable: "PeriodEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospital_PeriodEntity_HospitalPriviledgePeriodId",
                table: "Hospital",
                column: "HospitalPriviledgePeriodId",
                principalTable: "PeriodEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
