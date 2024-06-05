using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddCitizenship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenshipTypeEntity_CitizenshipTypeEntity_CitizenshipTypeEntityId",
                table: "CitizenshipTypeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDetail_CitizenshipTypeEntity_CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenshipTypeEntity",
                table: "CitizenshipTypeEntity");

            migrationBuilder.RenameTable(
                name: "CitizenshipTypeEntity",
                newName: "CitizenshipType");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenshipTypeEntity_CitizenshipTypeEntityId",
                table: "CitizenshipType",
                newName: "IX_CitizenshipType_CitizenshipTypeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenshipType",
                table: "CitizenshipType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenshipType_CitizenshipType_CitizenshipTypeEntityId",
                table: "CitizenshipType",
                column: "CitizenshipTypeEntityId",
                principalTable: "CitizenshipType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDetail_CitizenshipType_CitizenshipTypeId",
                table: "ProviderDetail",
                column: "CitizenshipTypeId",
                principalTable: "CitizenshipType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenshipType_CitizenshipType_CitizenshipTypeEntityId",
                table: "CitizenshipType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDetail_CitizenshipType_CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CitizenshipType",
                table: "CitizenshipType");

            migrationBuilder.RenameTable(
                name: "CitizenshipType",
                newName: "CitizenshipTypeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_CitizenshipType_CitizenshipTypeEntityId",
                table: "CitizenshipTypeEntity",
                newName: "IX_CitizenshipTypeEntity_CitizenshipTypeEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CitizenshipTypeEntity",
                table: "CitizenshipTypeEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenshipTypeEntity_CitizenshipTypeEntity_CitizenshipTypeEntityId",
                table: "CitizenshipTypeEntity",
                column: "CitizenshipTypeEntityId",
                principalTable: "CitizenshipTypeEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDetail_CitizenshipTypeEntity_CitizenshipTypeId",
                table: "ProviderDetail",
                column: "CitizenshipTypeId",
                principalTable: "CitizenshipTypeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
