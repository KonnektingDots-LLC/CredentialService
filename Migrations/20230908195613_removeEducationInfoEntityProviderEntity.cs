using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class removeEducationInfoEntityProviderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationInfoEntityProviderEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationInfoEntityProviderEntity",
                columns: table => new
                {
                    EducationInfoId = table.Column<int>(type: "int", nullable: false),
                    ProvidersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationInfoEntityProviderEntity", x => new { x.EducationInfoId, x.ProvidersId });
                    table.ForeignKey(
                        name: "FK_EducationInfoEntityProviderEntity_EducationInfo_EducationInfoId",
                        column: x => x.EducationInfoId,
                        principalTable: "EducationInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationInfoEntityProviderEntity_Provider_ProvidersId",
                        column: x => x.ProvidersId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationInfoEntityProviderEntity_ProvidersId",
                table: "EducationInfoEntityProviderEntity",
                column: "ProvidersId");
        }
    }
}
