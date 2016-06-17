using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class addedCustUserNameToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "Physician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "MedicalCondition",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "Insurance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "EmContact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "CustomerMed",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "Allergy",
                nullable: true);

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "Physician",
                newName: "ShowOnPublicView");

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "MedicalCondition",
                newName: "ShowOnPublicView");

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "Insurance",
                newName: "ShowOnPublicView");

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "EmContact",
                newName: "ShowOnPublicView");

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "CustomerMed",
                newName: "ShowOnPublicView");

            migrationBuilder.RenameColumn(
                name: "showOnPublicView",
                table: "Allergy",
                newName: "ShowOnPublicView");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "Physician");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "MedicalCondition");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "Insurance");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "EmContact");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "CustomerMed");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "Allergy");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "Physician",
                newName: "showOnPublicView");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "MedicalCondition",
                newName: "showOnPublicView");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "Insurance",
                newName: "showOnPublicView");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "EmContact",
                newName: "showOnPublicView");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "CustomerMed",
                newName: "showOnPublicView");

            migrationBuilder.RenameColumn(
                name: "ShowOnPublicView",
                table: "Allergy",
                newName: "showOnPublicView");
        }
    }
}
