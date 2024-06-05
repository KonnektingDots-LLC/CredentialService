using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class DeleteMedicalGroupEntityProviderEntityReliontionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalGroupEntityProviderEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalGroupEntityProviderEntity",
                columns: table => new
                {
                    MedicalGroupId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalGroupEntityProviderEntity", x => new { x.MedicalGroupId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_MedicalGroupEntityProviderEntity_MedicalGroup_MedicalGroupId",
                        column: x => x.MedicalGroupId,
                        principalTable: "MedicalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalGroupEntityProviderEntity_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalGroupEntityProviderEntity_ProviderId",
                table: "MedicalGroupEntityProviderEntity",
                column: "ProviderId");
        }
    }
}
