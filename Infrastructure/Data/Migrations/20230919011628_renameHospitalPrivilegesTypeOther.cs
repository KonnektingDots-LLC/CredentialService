using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    /// <inheritdoc />
    public partial class renameHospitalPrivilegesTypeOther : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hospitalPrivilegesTypeOther",
                table: "Hospital",
                newName: "HospitalPrivilegesTypeOther");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HospitalPrivilegesTypeOther",
                table: "Hospital",
                newName: "hospitalPrivilegesTypeOther");
        }
    }
}
