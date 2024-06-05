using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MGTypeList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MGTypeId = table.Column<int>(type: "int", nullable: false),
                    MGTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MGTypeList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    MGTypeId = table.Column<int>(type: "int", nullable: false),
                    MGName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGMedicaidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGNPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGBillingNPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGTaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGEndorsementLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MGEmployerId_EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGProvSpecialtyListId = table.Column<int>(type: "int", nullable: false),
                    MGProvSubSpecialtyListId = table.Column<int>(type: "int", nullable: false),
                    MGContactId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGMailingAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGPhysicalAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGEINEvidence = table.Column<bool>(type: "bit", nullable: false),
                    MGNPICertificateEvidence = table.Column<bool>(type: "bit", nullable: false),
                    MGEndorsementLetterEvidence = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalGroup_MGTypeList_MGTypeId",
                        column: x => x.MGTypeId,
                        principalTable: "MGTypeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalGroup_MGTypeId",
                table: "MedicalGroup",
                column: "MGTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalGroup");

            migrationBuilder.DropTable(
                name: "MGTypeList");
        }
    }
}
