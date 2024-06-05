using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddCorporationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CorpTaxTypeId = table.Column<int>(type: "int", nullable: false),
                    CorpTaxIdTypeId = table.Column<int>(type: "int", nullable: false),
                    CorpTaxIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityTypeId = table.Column<int>(type: "int", nullable: false),
                    CorporatePracticeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationEffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillingNPI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenderingNPI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporationCertificateEvidence = table.Column<bool>(type: "bit", nullable: true),
                    NPICertificateEvidence = table.Column<bool>(type: "bit", nullable: true),
                    EIN_W9Evidence = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corporation_CorpTaxIdType_CorpTaxIdTypeId",
                        column: x => x.CorpTaxIdTypeId,
                        principalTable: "CorpTaxIdType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corporation_EntityType_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corporation_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorporationSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporationId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    CorporationEntityId = table.Column<int>(type: "int", nullable: true),
                    SpecialtyListEntityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporationSpecialty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorporationSpecialty_Corporation_CorporationEntityId",
                        column: x => x.CorporationEntityId,
                        principalTable: "Corporation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CorporationSpecialty_SpecialtyList_SpecialtyListEntityId",
                        column: x => x.SpecialtyListEntityId,
                        principalTable: "SpecialtyList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CorporationSubSpecialty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporationId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    CorporationEntityId = table.Column<int>(type: "int", nullable: true),
                    SpecialtyListEntityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporationSubSpecialty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorporationSubSpecialty_Corporation_CorporationEntityId",
                        column: x => x.CorporationEntityId,
                        principalTable: "Corporation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CorporationSubSpecialty_SpecialtyList_SpecialtyListEntityId",
                        column: x => x.SpecialtyListEntityId,
                        principalTable: "SpecialtyList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProviderCorporation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Corporation = table.Column<int>(type: "int", nullable: false),
                    CorporationEntityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderCorporation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderCorporation_Corporation_CorporationEntityId",
                        column: x => x.CorporationEntityId,
                        principalTable: "Corporation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_CorpTaxIdTypeId",
                table: "Corporation",
                column: "CorpTaxIdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_EntityTypeId",
                table: "Corporation",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Corporation_ProviderId",
                table: "Corporation",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporationSpecialty_CorporationEntityId",
                table: "CorporationSpecialty",
                column: "CorporationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporationSpecialty_SpecialtyListEntityId",
                table: "CorporationSpecialty",
                column: "SpecialtyListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporationSubSpecialty_CorporationEntityId",
                table: "CorporationSubSpecialty",
                column: "CorporationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CorporationSubSpecialty_SpecialtyListEntityId",
                table: "CorporationSubSpecialty",
                column: "SpecialtyListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderCorporation_CorporationEntityId",
                table: "ProviderCorporation",
                column: "CorporationEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressPlanAcceptList");

            migrationBuilder.DropTable(
                name: "CorporationSpecialty");

            migrationBuilder.DropTable(
                name: "CorporationSubSpecialty");

            migrationBuilder.DropTable(
                name: "ProviderCorporation");

            migrationBuilder.DropTable(
                name: "Corporation");
        }
    }
}
