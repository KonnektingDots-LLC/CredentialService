using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderSpecialtySubDocumentRelationshipOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "AzureBlobFilename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty",
                column: "AzureBlobFilename",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "AzureBlobFilename");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty",
                column: "AzureBlobFilename");
        }
    }
}
