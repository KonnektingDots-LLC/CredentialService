using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class updateDocumentLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLocation_OrganizationType_OrganizationTypeId",
                table: "DocumentLocation");

            migrationBuilder.DropIndex(
                name: "IX_DocumentLocation_OrganizationTypeId",
                table: "DocumentLocation");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "OrganizationTypeId",
                table: "DocumentLocation");

            migrationBuilder.RenameColumn(
                name: "hasNPI",
                table: "DocumentType",
                newName: "IsExpired");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "DocumentType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DocumentType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentSectionTypeId",
                table: "DocumentType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "DocumentType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "DocumentLocation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentSectionTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSectionTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_DocumentSectionTypeId",
                table: "DocumentType",
                column: "DocumentSectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentType_DocumentSectionTypeEntity_DocumentSectionTypeId",
                table: "DocumentType",
                column: "DocumentSectionTypeId",
                principalTable: "DocumentSectionTypeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentType_DocumentSectionTypeEntity_DocumentSectionTypeId",
                table: "DocumentType");

            migrationBuilder.DropTable(
                name: "DocumentSectionTypeEntity");

            migrationBuilder.DropIndex(
                name: "IX_DocumentType_DocumentSectionTypeId",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "DocumentSectionTypeId",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "DocumentLocation");

            migrationBuilder.RenameColumn(
                name: "IsExpired",
                table: "DocumentType",
                newName: "hasNPI");

            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "MedicalGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "DocumentType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "DocumentType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeId",
                table: "DocumentLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StateId",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLocation_OrganizationTypeId",
                table: "DocumentLocation",
                column: "OrganizationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLocation_OrganizationType_OrganizationTypeId",
                table: "DocumentLocation",
                column: "OrganizationTypeId",
                principalTable: "OrganizationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
