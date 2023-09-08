using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Providers.Migrations
{
    /// <inheritdoc />
    public partial class changecontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Sprints_SprintId",
                table: "File");

            migrationBuilder.DropForeignKey(
                name: "FK_File_Tasks_TaskId",
                table: "File");

            migrationBuilder.DropPrimaryKey(
                name: "PK_File",
                table: "File");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_File_TaskId",
                table: "Files",
                newName: "IX_Files_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_File_SprintId",
                table: "Files",
                newName: "IX_Files_SprintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Sprints_SprintId",
                table: "Files",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Tasks_TaskId",
                table: "Files",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Sprints_SprintId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Tasks_TaskId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameIndex(
                name: "IX_Files_TaskId",
                table: "File",
                newName: "IX_File_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_SprintId",
                table: "File",
                newName: "IX_File_SprintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_File",
                table: "File",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Sprints_SprintId",
                table: "File",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Tasks_TaskId",
                table: "File",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
