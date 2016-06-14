using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BasicMedicalHistory.Migrations
{
    public partial class changedMedicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Customer_CustomerId",
                table: "Medication");

            migrationBuilder.DropIndex(
                name: "IX_Medication_CustomerId",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "BloodTypeId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "BloodType");

            migrationBuilder.CreateTable(
                name: "CustomerMed",
                columns: table => new
                {
                    CustomerMedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    MedicationId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Usage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMed", x => x.CustomerMedId);
                    table.ForeignKey(
                        name: "FK_CustomerMed_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerMed_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "DrugInteractions",
                table: "Medication",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SideEffects",
                table: "Medication",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMed_CustomerId",
                table: "CustomerMed",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMed_MedicationId",
                table: "CustomerMed",
                column: "MedicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugInteractions",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "SideEffects",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerMed");

            migrationBuilder.CreateTable(
                name: "BloodType",
                columns: table => new
                {
                    BloodTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BloodTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodType", x => x.BloodTypeId);
                });

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Medication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Medication",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Medication",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "Medication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medication_CustomerId",
                table: "Medication",
                column: "CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Customer_CustomerId",
                table: "Medication",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
