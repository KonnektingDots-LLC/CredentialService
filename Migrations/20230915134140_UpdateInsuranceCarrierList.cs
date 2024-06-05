using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInsuranceCarrierList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Malpractice_InsuranceCarrierList_InsuranceCarrierId",
                table: "Malpractice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalLiability_InsuranceCarrierList_InsuranceCarrierId",
                table: "ProfessionalLiability");


            migrationBuilder.DropTable(
                name: "InsuranceCarrierList");


            migrationBuilder.DropIndex(
                name: "IX_ProfessionalLiability_InsuranceCarrierId",
                table: "ProfessionalLiability");

            migrationBuilder.DropIndex(
                name: "IX_Malpractice_InsuranceCarrierId",
                table: "Malpractice");


            migrationBuilder.AddColumn<int>(
                name: "ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.AddColumn<int>(
                name: "MalpracticeCarrierId",
                table: "Malpractice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MalpracticeCarrierList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalpracticeCarrierList", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO MalpracticeCarrierList (Name, CreatedBy, CreationDate, ModifiedBy, ModifiedDate, IsActive, ExpiredDate, IsExpired)" +
                " VALUES ('SIMED', 'lleon', GETDATE(), '', '', 1, '', '')");

            migrationBuilder.CreateTable(
                name: "ProfessionalCarrierList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalCarrierList", x => x.Id);
                });



            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalLiability_ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability",
                column: "ProfessionalLiabilityCarrierId");


            migrationBuilder.CreateIndex(
                name: "IX_Malpractice_MalpracticeCarrierId",
                table: "Malpractice",
                column: "MalpracticeCarrierId");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropForeignKey(
                name: "FK_Malpractice_MalpracticeCarrierList_MalpracticeCarrierId",
                table: "Malpractice");







            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalLiability_ProfessionalCarrierList_ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability");






            migrationBuilder.DropTable(
                name: "MalpracticeCarrierList");

            migrationBuilder.DropTable(
                name: "ProfessionalCarrierList");





            migrationBuilder.DropIndex(
                name: "IX_ProfessionalLiability_ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability");




            migrationBuilder.DropIndex(
                name: "IX_Malpractice_MalpracticeCarrierId",
                table: "Malpractice");

            migrationBuilder.DropColumn(
                name: "ProfessionalLiabilityCarrierId",
                table: "ProfessionalLiability");


     


            migrationBuilder.DropColumn(
                name: "MalpracticeCarrierId",
                table: "Malpractice");


            migrationBuilder.CreateTable(
                name: "InsuranceCarrierList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCarrierList", x => x.Id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalLiability_InsuranceCarrierId",
                table: "ProfessionalLiability",
                column: "InsuranceCarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Malpractice_InsuranceCarrierId",
                table: "Malpractice",
                column: "InsuranceCarrierId");


            migrationBuilder.AddForeignKey(
                name: "FK_Malpractice_InsuranceCarrierList_InsuranceCarrierId",
                table: "Malpractice",
                column: "InsuranceCarrierId",
                principalTable: "InsuranceCarrierList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalLiability_InsuranceCarrierList_InsuranceCarrierId",
                table: "ProfessionalLiability",
                column: "InsuranceCarrierId",
                principalTable: "InsuranceCarrierList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
