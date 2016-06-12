using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class updatedCustomerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "CustEmail",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustName",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustEmail",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustName",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Customer",
                nullable: true);
        }
    }
}
