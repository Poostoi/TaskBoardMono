using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Providers.Migrations
{
    /// <inheritdoc />
    public partial class changemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sprints_SprintId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SprintId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SprintId",
                table: "Users");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DataImage",
                table: "File",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SprintId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DataImage",
                table: "File",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SprintId",
                table: "Users",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sprints_SprintId",
                table: "Users",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id");
        }
    }
}
