using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdddressStateAndAddHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressStateType",
                table: "AddressStateType");

            migrationBuilder.RenameTable(
                name: "AddressStateType",
                newName: "AddressState");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressState",
                table: "AddressState",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HospitalList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_HospitalList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospPriviledgeList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_HospPriviledgeList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodMonthFrom = table.Column<int>(type: "int", nullable: false),
                    PeriodYearFrom = table.Column<int>(type: "int", nullable: false),
                    PeriodMonthTo = table.Column<int>(type: "int", nullable: false),
                    PeriodYearTo = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    HospPriviledgeId = table.Column<int>(type: "int", nullable: false),
                    HospPriviledgeListId = table.Column<int>(type: "int", nullable: false),
                    HospitalPriviledgePeriodId = table.Column<int>(type: "int", nullable: false),
                    HospitalListId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospital_HospPriviledgeList_HospPriviledgeListId",
                        column: x => x.HospPriviledgeListId,
                        principalTable: "HospPriviledgeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospital_HospitalList_HospitalListId",
                        column: x => x.HospitalListId,
                        principalTable: "HospitalList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospital_PeriodEntity_HospitalPriviledgePeriodId",
                        column: x => x.HospitalPriviledgePeriodId,
                        principalTable: "PeriodEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_HospitalListId",
                table: "Hospital",
                column: "HospitalListId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_HospitalPriviledgePeriodId",
                table: "Hospital",
                column: "HospitalPriviledgePeriodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospital_HospPriviledgeListId",
                table: "Hospital",
                column: "HospPriviledgeListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hospital");

            migrationBuilder.DropTable(
                name: "HospPriviledgeList");

            migrationBuilder.DropTable(
                name: "HospitalList");

            migrationBuilder.DropTable(
                name: "PeriodEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressState",
                table: "AddressState");

            migrationBuilder.RenameTable(
                name: "AddressState",
                newName: "AddressStateType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressStateType",
                table: "AddressStateType",
                column: "Id");
        }
    }
}
