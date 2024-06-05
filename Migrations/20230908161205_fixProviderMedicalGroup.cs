using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class fixProviderMedicalGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMedicalGroup_MedicalGroup_MedicalGroupEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMedicalGroup_Provider_ProviderEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProviderMedicalGroup_MedicalGroupEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProviderMedicalGroup_ProviderEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropColumn(
                name: "MedicalGroupEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropColumn(
                name: "ProviderEntityId",
                table: "ProviderMedicalGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_MedicalGroupId",
                table: "ProviderMedicalGroup",
                column: "MedicalGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_ProviderId",
                table: "ProviderMedicalGroup",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMedicalGroup_MedicalGroup_MedicalGroupId",
                table: "ProviderMedicalGroup",
                column: "MedicalGroupId",
                principalTable: "MedicalGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMedicalGroup_Provider_ProviderId",
                table: "ProviderMedicalGroup",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMedicalGroup_MedicalGroup_MedicalGroupId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMedicalGroup_Provider_ProviderId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProviderMedicalGroup_MedicalGroupId",
                table: "ProviderMedicalGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProviderMedicalGroup_ProviderId",
                table: "ProviderMedicalGroup");

            migrationBuilder.AddColumn<int>(
                name: "MedicalGroupEntityId",
                table: "ProviderMedicalGroup",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProviderEntityId",
                table: "ProviderMedicalGroup",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_MedicalGroupEntityId",
                table: "ProviderMedicalGroup",
                column: "MedicalGroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_ProviderEntityId",
                table: "ProviderMedicalGroup",
                column: "ProviderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMedicalGroup_MedicalGroup_MedicalGroupEntityId",
                table: "ProviderMedicalGroup",
                column: "MedicalGroupEntityId",
                principalTable: "MedicalGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMedicalGroup_Provider_ProviderEntityId",
                table: "ProviderMedicalGroup",
                column: "ProviderEntityId",
                principalTable: "Provider",
                principalColumn: "Id");
        }
    }
}
