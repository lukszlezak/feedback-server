using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Migrations.FeedbackDb
{
    public partial class AddedFeedbackModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DomainId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
        }
    }
}
