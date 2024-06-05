using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderSpecialtySubDocumentRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderSubSpecialty",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSubSpecialty_ProviderId",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderSpecialty",
                table: "ProviderSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_ProviderId",
                table: "ProviderSpecialty");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropColumn(
                name: "EvidenceFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProviderSpecialty");

            migrationBuilder.DropColumn(
                name: "EvidenceFilename",
                table: "ProviderSpecialty");

            migrationBuilder.AddColumn<string>(
                name: "AzureBlobFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AzureBlobFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderSubSpecialty",
                table: "ProviderSubSpecialty",
                columns: new[] { "ProviderId", "SubSpecialtyListId", "AzureBlobFilename" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderSpecialty",
                table: "ProviderSpecialty",
                columns: new[] { "ProviderId", "SpecialtyListId", "AzureBlobFilename" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSpecialty_DocumentLocation_DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderSubSpecialty_DocumentLocation_DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderSubSpecialty",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSubSpecialty_DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderSpecialty",
                table: "ProviderSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_ProviderSpecialty_DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropColumn(
                name: "AzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropColumn(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSubSpecialty");

            migrationBuilder.DropColumn(
                name: "AzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.DropColumn(
                name: "DocumentLocationAzureBlobFilename",
                table: "ProviderSpecialty");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProviderSubSpecialty",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSubSpecialty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProviderSpecialty",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "EvidenceFilename",
                table: "ProviderSpecialty",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderSubSpecialty",
                table: "ProviderSubSpecialty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderSpecialty",
                table: "ProviderSpecialty",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_ProviderId",
                table: "ProviderSubSpecialty",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_ProviderId",
                table: "ProviderSpecialty",
                column: "ProviderId");
        }
    }
}
