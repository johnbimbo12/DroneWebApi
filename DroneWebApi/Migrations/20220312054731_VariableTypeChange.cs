using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneWebApi.Migrations
{
    public partial class VariableTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Drones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Drones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Model", "State" },
                values: new object[] { "Cruiserweight", "IDLE" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Model", "State" },
                values: new object[] { "Heavyweight", "RETURNING" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Model", "State" },
                values: new object[] { "Lightweight", "LOADED" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Model", "State" },
                values: new object[] { "Middleweight", "DELIVERED" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Model", "State" },
                values: new object[] { "Middleweight", "DELIVERING" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Model", "State" },
                values: new object[] { "Heavyweight", "LOADED" });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Model", "State" },
                values: new object[] { "Cruiserweight", "IDLE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "Drones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Model",
                table: "Drones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Model", "State" },
                values: new object[] { 2, 0 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Model", "State" },
                values: new object[] { 3, 5 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Model", "State" },
                values: new object[] { 0, 2 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Model", "State" },
                values: new object[] { 1, 4 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Model", "State" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Model", "State" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "Drones",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Model", "State" },
                values: new object[] { 2, 0 });
        }
    }
}
