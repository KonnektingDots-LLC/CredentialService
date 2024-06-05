using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressRelionshipAccept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressPlanAcceptList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPlanAcceptList", x => x.Id);
                });

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AddressServiceHour");



            migrationBuilder.DropColumn(
                name: "ADARequirement",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AcceptNewPatients",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AdaptedToDisabledPatients",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "IsPrincipal",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MovedMoredThan5Miles",
                table: "Address");


            migrationBuilder.RenameColumn(
                name: "AddressZipCode",
                table: "Address",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "AddressTypeListId",
                table: "Address",
                newName: "AddressTypeId");

            migrationBuilder.RenameColumn(
                name: "AddressState",
                table: "Address",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "AddressMedicaidId",
                table: "Address",
                newName: "AddressStateId");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Address",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                table: "Address",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "AddressCounty",
                table: "Address",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "AddressCity",
                table: "Address",
                newName: "Address2");

            migrationBuilder.RenameColumn(
                name: "ADAComment",
                table: "Address",
                newName: "Address1");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AddressServiceHour",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "AddressServiceHour",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AddressServiceHour",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationAddressId",
                table: "AddressServiceHour",
                type: "int",
                nullable: false,
                defaultValue: 0);



            migrationBuilder.AlterColumn<bool>(
                name: "IsClosed",
                table: "Address",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Address",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AttestationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_AttestationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationTypeId = table.Column<int>(type: "int", nullable: false),
                    AddressMedicaidID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    AddressPrincipalTypeId = table.Column<int>(type: "int", nullable: false),
                    IsAcceptingNewPatient = table.Column<bool>(type: "bit", nullable: false),
                    IsComplyWithAda = table.Column<bool>(type: "bit", nullable: false),
                    AdaComplyComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMovedMoreThan5Miles = table.Column<bool>(type: "bit", nullable: false),
                    IsAdaptedToDiabledPatient = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationAddress_AddressPrincipalType_AddressPrincipalTypeId",
                        column: x => x.AddressPrincipalTypeId,
                        principalTable: "AddressPrincipalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationAddress_OrganizationType_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attestation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    AttestationTypeId = table.Column<int>(type: "int", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attestation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attestation_AttestationType_AttestationTypeId",
                        column: x => x.AttestationTypeId,
                        principalTable: "AttestationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressServiceHour_OrganizationAddressId",
                table: "AddressServiceHour",
                column: "OrganizationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressStateId",
                table: "Address",
                column: "AddressStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attestation_AttestationTypeId",
                table: "Attestation",
                column: "AttestationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAddress_AddressId",
                table: "OrganizationAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAddress_AddressPrincipalTypeId",
                table: "OrganizationAddress",
                column: "AddressPrincipalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAddress_OrganizationTypeId",
                table: "OrganizationAddress",
                column: "OrganizationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AddressState_AddressStateId",
                table: "Address",
                column: "AddressStateId",
                principalTable: "AddressState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AddressType_AddressTypeId",
                table: "Address",
                column: "AddressTypeId",
                principalTable: "AddressType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressServiceHour_OrganizationAddress_OrganizationAddressId",
                table: "AddressServiceHour",
                column: "OrganizationAddressId",
                principalTable: "OrganizationAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AddressState_AddressStateId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_AddressType_AddressTypeId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressServiceHour_OrganizationAddress_OrganizationAddressId",
                table: "AddressServiceHour");

            migrationBuilder.DropTable(
                name: "Attestation");

            migrationBuilder.DropTable(
                name: "OrganizationAddress");

            migrationBuilder.DropTable(
                name: "AttestationType");

            migrationBuilder.DropIndex(
                name: "IX_AddressServiceHour_OrganizationAddressId",
                table: "AddressServiceHour");

            migrationBuilder.DropIndex(
                name: "IX_Address_AddressStateId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OrganizationAddressId",
                table: "AddressServiceHour");



            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Address",
                newName: "AddressZipCode");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Address",
                newName: "AddressState");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Address",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Address",
                newName: "AddressLine1");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Address",
                newName: "AddressCounty");

            migrationBuilder.RenameColumn(
                name: "AddressTypeId",
                table: "Address",
                newName: "AddressTypeListId");

            migrationBuilder.RenameColumn(
                name: "AddressStateId",
                table: "Address",
                newName: "AddressMedicaidId");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Address",
                newName: "AddressCity");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Address",
                newName: "ADAComment");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AddressServiceHour",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DayOfWeek",
                table: "AddressServiceHour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AddressServiceHour",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "AddressServiceHour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");




            migrationBuilder.AlterColumn<bool>(
                name: "IsClosed",
                table: "Address",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Address",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "ADARequirement",
                table: "Address",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptNewPatients",
                table: "Address",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdaptedToDisabledPatients",
                table: "Address",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrincipal",
                table: "Address",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MovedMoredThan5Miles",
                table: "Address",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressContactFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressContact", x => x.Id);
                });
        }
    }
}
