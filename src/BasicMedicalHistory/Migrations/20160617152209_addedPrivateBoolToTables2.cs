using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class addedPrivateBoolToTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "showOnPublicView",
                table: "Physician",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "showOnPublicView",
                table: "MedicalCondition",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "showOnPublicView",
                table: "Insurance",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "showOnPublicView",
                table: "EmContact",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "showOnPublicView",
                table: "CustomerMed",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "showOnPublicView",
                table: "Physician",
                nullable: false);

            migrationBuilder.AlterColumn<byte>(
                name: "showOnPublicView",
                table: "MedicalCondition",
                nullable: false);

            migrationBuilder.AlterColumn<byte>(
                name: "showOnPublicView",
                table: "Insurance",
                nullable: false);

            migrationBuilder.AlterColumn<byte>(
                name: "showOnPublicView",
                table: "EmContact",
                nullable: false);

            migrationBuilder.AlterColumn<byte>(
                name: "showOnPublicView",
                table: "CustomerMed",
                nullable: false);
        }
    }
}
