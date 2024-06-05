using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanAcceptList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanAcceptList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanAcceptListEntityId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_PlanAcceptList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanAcceptList_PlanAcceptList_PlanAcceptListEntityId",
                        column: x => x.PlanAcceptListEntityId,
                        principalTable: "PlanAcceptList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProviderPlanAccept",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    PlanAcceptListId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderPlanAccept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderPlanAccept_PlanAcceptList_PlanAcceptListId",
                        column: x => x.PlanAcceptListId,
                        principalTable: "PlanAcceptList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderPlanAccept_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanAcceptList_PlanAcceptListEntityId",
                table: "PlanAcceptList",
                column: "PlanAcceptListEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderPlanAccept_PlanAcceptListId",
                table: "ProviderPlanAccept",
                column: "PlanAcceptListId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderPlanAccept_ProviderId",
                table: "ProviderPlanAccept",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProviderPlanAccept");

            migrationBuilder.DropTable(
                name: "PlanAcceptList");
        }
    }
}
