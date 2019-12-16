using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Data.Migrations.FeedbackDb
{
    public partial class AddedFeedbackDomainFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_DomainId",
                table: "Feedbacks",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Domains_DomainId",
                table: "Feedbacks",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Domains_DomainId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_DomainId",
                table: "Feedbacks");
        }
    }
}
