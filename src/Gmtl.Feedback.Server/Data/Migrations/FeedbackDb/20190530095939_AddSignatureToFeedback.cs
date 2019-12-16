using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Data.Migrations.FeedbackDb
{
    public partial class AddSignatureToFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Feedbacks",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Feedbacks");
        }
    }
}
