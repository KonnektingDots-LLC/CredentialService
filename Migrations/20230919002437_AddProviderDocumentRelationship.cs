using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderDocumentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DocumentLocation_ProviderId",
                table: "DocumentLocation",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLocation_Provider_ProviderId",
                table: "DocumentLocation",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id");
                //onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLocation_Provider_ProviderId",
                table: "DocumentLocation");

            migrationBuilder.DropIndex(
                name: "IX_DocumentLocation_ProviderId",
                table: "DocumentLocation");
        }
    }
}
