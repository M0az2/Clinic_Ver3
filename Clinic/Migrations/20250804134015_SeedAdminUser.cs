using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Age", "Email", "EndTime", "FullName", "Password", "PhoneNum", "Specialization", "StartTime" },
                values: new object[] { 1, 0, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin User", "admin123", null, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
