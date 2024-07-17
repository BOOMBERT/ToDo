using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDo.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "Completed", "Description", "DueDate", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, true, "Description11", new DateTime(2024, 7, 18, 15, 46, 20, 141, DateTimeKind.Local).AddTicks(5512), 0, "Title1" },
                    { 2, false, "Description22", new DateTime(2024, 7, 19, 15, 46, 20, 141, DateTimeKind.Local).AddTicks(5561), 1, "Title2" },
                    { 3, true, "Description33", new DateTime(2024, 7, 20, 15, 46, 20, 141, DateTimeKind.Local).AddTicks(5564), 2, "Title3" },
                    { 4, false, "Description44", new DateTime(2024, 7, 21, 15, 46, 20, 141, DateTimeKind.Local).AddTicks(5566), 0, "Title4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ToDoTasks",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
