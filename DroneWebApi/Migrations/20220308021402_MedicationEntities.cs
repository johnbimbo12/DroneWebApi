using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneWebApi.Migrations
{
    public partial class MedicationEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_Drones_DroneId",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_Drones_DroneId",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Drones");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                table: "Medications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "Id", "Code", "DroneId", "Image", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "MD_PCM_TB", 5, null, "Paracetamol Tablets", 300.0 },
                    { 2, "MD_CL_IV", 6, null, "Chloroquine Intravenous", 350.0 },
                    { 3, "MD_MET_TB", 5, null, "Metronidazole Tablets", 200.0 },
                    { 4, "MD_VITC_SY", 3, null, "Vitamin C Syrup", 375.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DroneId",
                table: "Medications",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Drones_DroneId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DroneId",
                table: "Medications");

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Medications");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                table: "Drones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_DroneId",
                table: "Drones",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_Drones_DroneId",
                table: "Drones",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
