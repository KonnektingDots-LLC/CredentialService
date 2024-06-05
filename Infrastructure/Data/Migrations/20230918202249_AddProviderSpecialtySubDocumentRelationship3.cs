using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderSpecialtySubDocumentRelationship3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProviderSubSpecialty_AzureBlobFilename",
            //    table: "ProviderSubSpecialty",
            //    column: "AzureBlobFilename");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty",
                column: "AzureBlobFilename");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_AzureBlobFilename",
                table: "ProviderSpecialty",
                column: "AzureBlobFilename",
                principalTable: "DocumentLocation",
                principalColumn: "AzureBlobFilename",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProviderSubSpecialty_DocumentLocation_AzureBlobFilename",
            //    table: "ProviderSubSpecialty",
            //    column: "AzureBlobFilename",
            //    principalTable: "DocumentLocation",
            //    principalColumn: "AzureBlobFilename",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_AzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSubSpecialty_DocumentLocation_AzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSubSpecialty_AzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_AzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.AddColumn<string>(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "DocumentLocationAzureBlobFilename");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty",
                column: "DocumentLocationAzureBlobFilename");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty",
                column: "DocumentLocationAzureBlobFilename",
                principalTable: "DocumentLocation",
                principalColumn: "AzureBlobFilename");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSubSpecialty_DocumentLocation_DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty",
                column: "DocumentLocationAzureBlobFilename",
                principalTable: "DocumentLocation",
                principalColumn: "AzureBlobFilename");
        }
    }
}
