using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class addedPrivateBoolToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "showOnPublicView",
                table: "Physician",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "showOnPublicView",
                table: "MedicalCondition",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "showOnPublicView",
                table: "Insurance",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "showOnPublicView",
                table: "EmContact",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "showOnPublicView",
                table: "CustomerMed",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "showOnPublicView",
                table: "Allergy",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "MedicalCondition");

            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "Insurance");

            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "EmContact");

            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "CustomerMed");

            migrationBuilder.DropColumn(
                name: "showOnPublicView",
                table: "Allergy");
        }
    }
}
