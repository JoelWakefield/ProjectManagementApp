using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class RefactorDataRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PhaseSchedules");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveId",
                table: "Stages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Assigned",
                table: "ProjectUserRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ProjectUserRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ArchiveId",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveId",
                table: "ProjectRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveId",
                table: "PhaseSchedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PhaseSchedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArchiveId",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StageId",
                table: "Phases",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUserPhase",
                columns: table => new
                {
                    AssignedPhasesId = table.Column<string>(type: "text", nullable: false),
                    AssignmentsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPhase", x => new { x.AssignedPhasesId, x.AssignmentsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPhase_AspNetUsers_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPhase_Phases_AssignedPhasesId",
                        column: x => x.AssignedPhasesId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserProjectRole",
                columns: table => new
                {
                    ProjectRolesId = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserProjectRole", x => new { x.ProjectRolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserProjectRole_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserProjectRole_ProjectRoles_ProjectRolesId",
                        column: x => x.ProjectRolesId,
                        principalTable: "ProjectRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseArchives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: false),
                    StageId = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhaseScheduleArchives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhaseId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseScheduleArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectArchives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProjectedStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProjectedEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalWorkHours = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleArchives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageArchives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageArchives", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseSchedules_PhaseId",
                table: "PhaseSchedules",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseSchedules_UserId",
                table: "PhaseSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_OwnerId",
                table: "Phases",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_ProjectId",
                table: "Phases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_StageId",
                table: "Phases",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPhase_AssignmentsId",
                table: "ApplicationUserPhase",
                column: "AssignmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserProjectRole_UsersId",
                table: "ApplicationUserProjectRole",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_AspNetUsers_OwnerId",
                table: "Phases",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Projects_ProjectId",
                table: "Phases",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phases_Stages_StageId",
                table: "Phases",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhaseSchedules_AspNetUsers_UserId",
                table: "PhaseSchedules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhaseSchedules_Phases_PhaseId",
                table: "PhaseSchedules",
                column: "PhaseId",
                principalTable: "Phases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId",
                table: "Projects",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phases_AspNetUsers_OwnerId",
                table: "Phases");

            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Projects_ProjectId",
                table: "Phases");

            migrationBuilder.DropForeignKey(
                name: "FK_Phases_Stages_StageId",
                table: "Phases");

            migrationBuilder.DropForeignKey(
                name: "FK_PhaseSchedules_AspNetUsers_UserId",
                table: "PhaseSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_PhaseSchedules_Phases_PhaseId",
                table: "PhaseSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_OwnerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ApplicationUserPhase");

            migrationBuilder.DropTable(
                name: "ApplicationUserProjectRole");

            migrationBuilder.DropTable(
                name: "PhaseArchives");

            migrationBuilder.DropTable(
                name: "PhaseScheduleArchives");

            migrationBuilder.DropTable(
                name: "ProjectArchives");

            migrationBuilder.DropTable(
                name: "ProjectRoleArchives");

            migrationBuilder.DropTable(
                name: "StageArchives");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OwnerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_PhaseSchedules_PhaseId",
                table: "PhaseSchedules");

            migrationBuilder.DropIndex(
                name: "IX_PhaseSchedules_UserId",
                table: "PhaseSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Phases_OwnerId",
                table: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Phases_ProjectId",
                table: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Phases_StageId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "Assigned",
                table: "ProjectUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ProjectUserRoles");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "PhaseSchedules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PhaseSchedules");

            migrationBuilder.DropColumn(
                name: "ArchiveId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Phases");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PhaseSchedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
