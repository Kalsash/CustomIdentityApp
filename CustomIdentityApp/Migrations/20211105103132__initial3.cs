using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomIdentityApp.Migrations
{
    public partial class _initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Text", "Title" },
                values: new object[] { new Guid("716c2e99-6f6c-4472-81a5-43c56e11637c"), "text text", "Новый спутник запущен на орбиту" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("716c2e99-6f6c-4472-81a5-43c56e11637c"));
        }
    }
}
