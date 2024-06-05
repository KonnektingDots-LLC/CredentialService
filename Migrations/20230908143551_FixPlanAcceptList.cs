using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class FixPlanAcceptList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanAcceptList_PlanAcceptList_PlanAcceptListEntityId",
                table: "PlanAcceptList");

            migrationBuilder.DropIndex(
                name: "IX_PlanAcceptList_PlanAcceptListEntityId",
                table: "PlanAcceptList");

            migrationBuilder.DropColumn(
                name: "PlanAcceptListEntityId",
                table: "PlanAcceptList");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanAcceptListEntityId",
                table: "PlanAcceptList",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanAcceptList_PlanAcceptListEntityId",
                table: "PlanAcceptList",
                column: "PlanAcceptListEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAcceptList_PlanAcceptList_PlanAcceptListEntityId",
                table: "PlanAcceptList",
                column: "PlanAcceptListEntityId",
                principalTable: "PlanAcceptList",
                principalColumn: "Id");
        }
    }
}
