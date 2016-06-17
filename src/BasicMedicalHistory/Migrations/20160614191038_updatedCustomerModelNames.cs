using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicMedicalHistory.Migrations
{
    public partial class updatedCustomerModelNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustName",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "BloodType",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustFirst",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustLast",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustUserName",
                table: "Customer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustFirst",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustLast",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustUserName",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "BloodType",
                table: "Customer",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "CustName",
                table: "Customer",
                nullable: true);
        }
    }
}
