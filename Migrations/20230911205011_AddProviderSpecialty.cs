using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderSpecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporationSubSpecialty_SpecialtyList_SpecialtyListEntityId",
                table: "CorporationSubSpecialty");

            migrationBuilder.RenameColumn(
                name: "SpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                newName: "SubSpecialtyListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CorporationSubSpecialty_SpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                newName: "IX_CorporationSubSpecialty_SubSpecialtyListEntityId");

            migrationBuilder.CreateTable(
                name: "ProviderSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyListId = table.Column<int>(type: "int", nullable: false),
                    EvidenceFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderSpecialty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderSpecialty_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderSpecialty_SpecialtyList_SpecialtyListId",
                        column: x => x.SpecialtyListId,
                        principalTable: "SpecialtyList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProviderSubSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    SubSpecialtyListId = table.Column<int>(type: "int", nullable: false),
                    EvidenceFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderSubSpecialty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderSubSpecialty_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderSubSpecialty_SubSpecialtyList_SubSpecialtyListId",
                        column: x => x.SubSpecialtyListId,
                        principalTable: "SubSpecialtyList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_ProviderId",
                table: "ProviderSpecialty",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSpecialty_SpecialtyListId",
                table: "ProviderSpecialty",
                column: "SpecialtyListId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_ProviderId",
                table: "ProviderSubSpecialty",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderSubSpecialty_SubSpecialtyListId",
                table: "ProviderSubSpecialty",
                column: "SubSpecialtyListId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorporationSubSpecialty_SubSpecialtyList_SubSpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                column: "SubSpecialtyListEntityId",
                principalTable: "SubSpecialtyList",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporationSubSpecialty_SubSpecialtyList_SubSpecialtyListEntityId",
                table: "CorporationSubSpecialty");

            migrationBuilder.DropTable(
                name: "ProviderSpecialty");

            migrationBuilder.DropTable(
                name: "ProviderSubSpecialty");

            migrationBuilder.RenameColumn(
                name: "SubSpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                newName: "SpecialtyListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_CorporationSubSpecialty_SubSpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                newName: "IX_CorporationSubSpecialty_SpecialtyListEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorporationSubSpecialty_SpecialtyList_SpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                column: "SpecialtyListEntityId",
                principalTable: "SpecialtyList",
                principalColumn: "Id");
        }
    }
}
