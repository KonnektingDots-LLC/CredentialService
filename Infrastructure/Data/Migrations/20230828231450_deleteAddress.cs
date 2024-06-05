using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class deleteAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provider_AddressEntity_AddressId",
                table: "Provider");

            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.DropIndex(
                name: "IX_Provider_AddressId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Provider");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADAComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADARequirement = table.Column<bool>(type: "bit", nullable: true),
                    AcceptNewPatients = table.Column<bool>(type: "bit", nullable: true),
                    AdaptedToDisabledPatients = table.Column<bool>(type: "bit", nullable: true),
                    AddressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressMedicaidId = table.Column<int>(type: "int", nullable: false),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTypeListId = table.Column<int>(type: "int", nullable: false),
                    AddressZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: true),
                    IsPrincipal = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovedMoredThan5Miles = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provider_AddressId",
                table: "Provider",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_AddressEntity_AddressId",
                table: "Provider",
                column: "AddressId",
                principalTable: "AddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
