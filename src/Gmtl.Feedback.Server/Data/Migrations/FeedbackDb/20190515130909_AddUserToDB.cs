using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Data.Migrations.FeedbackDb
{
    public partial class AddUserToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_Id",
                table: "Feedbacks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_Id",
                table: "Domains",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id",
                table: "Users",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_Id",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Domains_Id",
                table: "Domains");
        }
    }
}
