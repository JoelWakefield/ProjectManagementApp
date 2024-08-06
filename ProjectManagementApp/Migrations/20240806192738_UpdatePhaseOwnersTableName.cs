using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhaseOwnersTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhasesOwners",
                table: "PhasesOwners");

            migrationBuilder.RenameTable(
                name: "PhasesOwners",
                newName: "PhaseOwners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhaseOwners",
                table: "PhaseOwners",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PhaseOwners",
                table: "PhaseOwners");

            migrationBuilder.RenameTable(
                name: "PhaseOwners",
                newName: "PhasesOwners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhasesOwners",
                table: "PhasesOwners",
                column: "Id");
        }
    }
}
