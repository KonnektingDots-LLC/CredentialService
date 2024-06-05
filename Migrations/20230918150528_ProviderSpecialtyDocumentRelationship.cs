using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class ProviderSpecialtyDocumentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_EvidenceFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_EvidenceFilename",
                table: "ProviderSpecialty");

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
