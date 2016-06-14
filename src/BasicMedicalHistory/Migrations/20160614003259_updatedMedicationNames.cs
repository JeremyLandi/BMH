using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class updatedMedicationNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalName",
                table: "Medication");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Medication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Medication");

            migrationBuilder.AddColumn<string>(
                name: "MedicalName",
                table: "Medication",
                nullable: true);
        }
    }
}
