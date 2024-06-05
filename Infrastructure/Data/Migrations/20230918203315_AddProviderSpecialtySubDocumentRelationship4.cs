using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderSpecialtySubDocumentRelationship4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "AzureBlobFilename");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSubSpecialty_DocumentLocation_AzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "AzureBlobFilename",
                principalTable: "DocumentLocation",
                principalColumn: "AzureBlobFilename",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
