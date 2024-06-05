using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpecialtyAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUSCitizen",
                table: "ProviderDetail");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrganizationType");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "ProviderType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "ProviderType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "ProviderDetail",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProviderDetail",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CitizenshipTypeId",
                table: "ProviderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OrganizationType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OrganizationType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "OrganizationType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrganizationType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "OrganizationType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "OrganizationType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "OrganizationType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CitizenshipTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CitizenshipTypeEntityId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_CitizenshipTypeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitizenshipTypeEntity_CitizenshipTypeEntity_CitizenshipTypeEntityId",
                        column: x => x.CitizenshipTypeEntityId,
                        principalTable: "CitizenshipTypeEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialtyList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrganizationTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtyList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialtyList_OrganizationType_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSpecialtyList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrganizationTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubSpecialtyList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSpecialtyList_OrganizationType_OrganizationTypeId",
                        column: x => x.OrganizationTypeId,
                        principalTable: "OrganizationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDetail_CitizenshipTypeId",
                table: "ProviderDetail",
                column: "CitizenshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenshipTypeEntity_CitizenshipTypeEntityId",
                table: "CitizenshipTypeEntity",
                column: "CitizenshipTypeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtyList_OrganizationTypeId",
                table: "SpecialtyList",
                column: "OrganizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSpecialtyList_OrganizationTypeId",
                table: "SubSpecialtyList",
                column: "OrganizationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDetail_CitizenshipTypeEntity_CitizenshipTypeId",
                table: "ProviderDetail",
                column: "CitizenshipTypeId",
                principalTable: "CitizenshipTypeEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDetail_CitizenshipTypeEntity_CitizenshipTypeId",
                table: "ProviderDetail");

            migrationBuilder.DropTable(
                name: "CitizenshipTypeEntity");

            migrationBuilder.DropTable(
                name: "SpecialtyList");

            migrationBuilder.DropTable(
                name: "SubSpecialtyList");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDetail_CitizenshipTypeId",
                table: "ProviderDetail");

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
        }
    }
}
