using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderMedicalGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalGroup_CareTypes_CareTypeId",
                table: "MedicalGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalGroup_MedicalGroupTypes_MedicalGroupTypeId",
                table: "MedicalGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalGroupTypes",
                table: "MedicalGroupTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareTypes",
                table: "CareTypes");

            migrationBuilder.RenameTable(
                name: "MedicalGroupTypes",
                newName: "MedicalGroupType");

            migrationBuilder.RenameTable(
                name: "CareTypes",
                newName: "CareType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalGroupType",
                table: "MedicalGroupType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareType",
                table: "CareType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProviderMedicalGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    MedicalGroupId = table.Column<int>(type: "int", nullable: false),
                    MedicalGroupEntityId = table.Column<int>(type: "int", nullable: true),
                    ProviderEntityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderMedicalGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderMedicalGroup_MedicalGroup_MedicalGroupEntityId",
                        column: x => x.MedicalGroupEntityId,
                        principalTable: "MedicalGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProviderMedicalGroup_Provider_ProviderEntityId",
                        column: x => x.ProviderEntityId,
                        principalTable: "Provider",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_MedicalGroupEntityId",
                table: "ProviderMedicalGroup",
                column: "MedicalGroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMedicalGroup_ProviderEntityId",
                table: "ProviderMedicalGroup",
                column: "ProviderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalGroup_CareType_CareTypeId",
                table: "MedicalGroup",
                column: "CareTypeId",
                principalTable: "CareType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalGroup_MedicalGroupType_MedicalGroupTypeId",
                table: "MedicalGroup",
                column: "MedicalGroupTypeId",
                principalTable: "MedicalGroupType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalGroup_CareType_CareTypeId",
                table: "MedicalGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalGroup_MedicalGroupType_MedicalGroupTypeId",
                table: "MedicalGroup");

            migrationBuilder.DropTable(
                name: "ProviderMedicalGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalGroupType",
                table: "MedicalGroupType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareType",
                table: "CareType");

            migrationBuilder.RenameTable(
                name: "MedicalGroupType",
                newName: "MedicalGroupTypes");

            migrationBuilder.RenameTable(
                name: "CareType",
                newName: "CareTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalGroupTypes",
                table: "MedicalGroupTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareTypes",
                table: "CareTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalGroup_CareTypes_CareTypeId",
                table: "MedicalGroup",
                column: "CareTypeId",
                principalTable: "CareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalGroup_MedicalGroupTypes_MedicalGroupTypeId",
                table: "MedicalGroup",
                column: "MedicalGroupTypeId",
                principalTable: "MedicalGroupTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
