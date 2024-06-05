using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class CreateMedicalGroupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CareTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalGroupTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MedicalGroupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    MedicalGroupTypeId = table.Column<int>(type: "int", nullable: false),
                    CareTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicaidId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingNPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndorsementLetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployerId_EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EINEvidence = table.Column<bool>(type: "bit", nullable: false),
                    NPICertificateEvidence = table.Column<bool>(type: "bit", nullable: false),
                    EndorsementLetterEvidence = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalGroup_CareTypes_CareTypeId",
                        column: x => x.CareTypeId,
                        principalTable: "CareTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalGroup_MedicalGroupTypes_MedicalGroupTypeId",
                        column: x => x.MedicalGroupTypeId,
                        principalTable: "MedicalGroupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalGroupEntityProviderEntity",
                columns: table => new
                {
                    MedicalGroupId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalGroupEntityProviderEntity", x => new { x.MedicalGroupId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_MedicalGroupEntityProviderEntity_MedicalGroup_MedicalGroupId",
                        column: x => x.MedicalGroupId,
                        principalTable: "MedicalGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalGroupEntityProviderEntity_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalGroup_CareTypeId",
                table: "MedicalGroup",
                column: "CareTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalGroup_MedicalGroupTypeId",
                table: "MedicalGroup",
                column: "MedicalGroupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalGroupEntityProviderEntity_ProviderId",
                table: "MedicalGroupEntityProviderEntity",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDetail_CitizenshipType_CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropTable(
                name: "AddressContact");

            migrationBuilder.DropTable(
                name: "AddressPlanAcceptList");

            migrationBuilder.DropTable(
                name: "AddressPrincipalType");

            migrationBuilder.DropTable(
                name: "AddressState");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "AppNotification");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "CitizenshipType");

            migrationBuilder.DropTable(
                name: "CorpTaxIdType");

            migrationBuilder.DropTable(
                name: "EducationInfoEntityProviderEntity");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.DropTable(
                name: "HospitalEntityProviderEntity");

            migrationBuilder.DropTable(
                name: "InsuranceType");

            migrationBuilder.DropTable(
                name: "Malpractice");

            migrationBuilder.DropTable(
                name: "MedicalGroupEntityProviderEntity");

            migrationBuilder.DropTable(
                name: "MedicalLicense");

            migrationBuilder.DropTable(
                name: "MedicalSchool");

            migrationBuilder.DropTable(
                name: "ProfessionalLiability");

            migrationBuilder.DropTable(
                name: "SpecialtyList");

            migrationBuilder.DropTable(
                name: "SubSpecialtyList");

            migrationBuilder.DropTable(
                name: "AppNotificationTypeList");

            migrationBuilder.DropTable(
                name: "Hospital");

            migrationBuilder.DropTable(
                name: "MedicalGroup");

            migrationBuilder.DropTable(
                name: "MedicalLicenseTypeList");

            migrationBuilder.DropTable(
                name: "EducationInfo");

            migrationBuilder.DropTable(
                name: "InsuranceCarrierList");

            migrationBuilder.DropTable(
                name: "HospPriviledgeList");

            migrationBuilder.DropTable(
                name: "HospitalList");

            migrationBuilder.DropTable(
                name: "CareTypes");

            migrationBuilder.DropTable(
                name: "MedicalGroupTypes");

            migrationBuilder.DropTable(
                name: "EducationTypes");

            migrationBuilder.DropTable(
                name: "PeriodEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDetail_CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressServiceHour",
                table: "AddressServiceHour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "ProviderType");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "ProviderType");

            migrationBuilder.DropColumn(
                name: "CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "OrganizationType");

            migrationBuilder.RenameTable(
                name: "AddressServiceHour",
                newName: "AddressServiceHourEntity");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "AddressEntity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ProviderDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProviderDetail",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUSCitizen",
                table: "ProviderDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrganizationType",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "OrganizationType",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrganizationType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressServiceHourEntity",
                table: "AddressServiceHourEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressEntity",
                table: "AddressEntity",
                column: "Id");

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
