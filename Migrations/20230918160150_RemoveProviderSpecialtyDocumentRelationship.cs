using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProviderSpecialtyDocumentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_EvidenceFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_EvidenceFilename",
                table: "ProviderSpecialty");

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_EvidenceFilename",
                table: "ProviderSpecialty",
                column: "EvidenceFilename",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_EvidenceFilename",
                table: "ProviderSpecialty",
                column: "EvidenceFilename",
                principalTable: "DocumentLocation",
                principalColumn: "AzureBlobFilename",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
