using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderIdToStages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Stages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Stages");
        }
    }
}
