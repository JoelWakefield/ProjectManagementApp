using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPhaseAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPhase_AspNetUsers_AssignmentsId",
                table: "ApplicationUserPhase");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPhase_Phases_AssignedPhasesId",
                table: "ApplicationUserPhase");

            migrationBuilder.RenameColumn(
                name: "AssignmentsId",
                table: "ApplicationUserPhase",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AssignedPhasesId",
                table: "ApplicationUserPhase",
                newName: "PhaseId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPhase_AssignmentsId",
                table: "ApplicationUserPhase",
                newName: "IX_ApplicationUserPhase_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Stages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PhaseSchedules",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StageId",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPhase_AspNetUsers_UserId",
                table: "ApplicationUserPhase",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPhase_Phases_PhaseId",
                table: "ApplicationUserPhase",
                column: "PhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPhase_AspNetUsers_UserId",
                table: "ApplicationUserPhase");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPhase_Phases_PhaseId",
                table: "ApplicationUserPhase");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ApplicationUserPhase",
                newName: "AssignmentsId");

            migrationBuilder.RenameColumn(
                name: "PhaseId",
                table: "ApplicationUserPhase",
                newName: "AssignedPhasesId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPhase_UserId",
                table: "ApplicationUserPhase",
                newName: "IX_ApplicationUserPhase_AssignmentsId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Stages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PhaseSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "StageId",
                table: "Phases",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Phases",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Phases",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPhase_AspNetUsers_AssignmentsId",
                table: "ApplicationUserPhase",
                column: "AssignmentsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPhase_Phases_AssignedPhasesId",
                table: "ApplicationUserPhase",
                column: "AssignedPhasesId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
