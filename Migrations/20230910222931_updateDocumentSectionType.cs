using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class updateDocumentSectionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentType_DocumentSectionTypeEntity_DocumentSectionTypeId",
                table: "DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentSectionTypeEntity",
                table: "DocumentSectionTypeEntity");

            migrationBuilder.RenameTable(
                name: "DocumentSectionTypeEntity",
                newName: "DocumentSectionType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentSectionType",
                table: "DocumentSectionType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentType_DocumentSectionType_DocumentSectionTypeId",
                table: "DocumentType",
                column: "DocumentSectionTypeId",
                principalTable: "DocumentSectionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentType_DocumentSectionType_DocumentSectionTypeId",
                table: "DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentSectionType",
                table: "DocumentSectionType");

            migrationBuilder.RenameTable(
                name: "DocumentSectionType",
                newName: "DocumentSectionTypeEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentSectionTypeEntity",
                table: "DocumentSectionTypeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentType_DocumentSectionTypeEntity_DocumentSectionTypeId",
                table: "DocumentType",
                column: "DocumentSectionTypeId",
                principalTable: "DocumentSectionTypeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
