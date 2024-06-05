using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddCorporationDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorporationDocument",
                columns: table => new
                {
                    CorporationId = table.Column<int>(type: "int", nullable: false),
                    AzureBlobFilename = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporationDocument", x => new { x.CorporationId, x.AzureBlobFilename });
                    table.ForeignKey(
                        name: "FK_CorporationDocument_Corporation_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CorporationDocument_DocumentLocation_AzureBlobFilename",
                        column: x => x.AzureBlobFilename,
                        principalTable: "DocumentLocation",
                        principalColumn: "AzureBlobFilename",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorporationDocument_AzureBlobFilename",
                table: "CorporationDocument",
                column: "AzureBlobFilename",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorporationDocument");
        }
    }
}
