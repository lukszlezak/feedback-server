using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Data.Migrations.FeedbackDb
{
    public partial class AddedApiKeyValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyValue",
                table: "ApiKeys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_DomainId",
                table: "ApiKeys",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKeys_Domains_DomainId",
                table: "ApiKeys",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeys_Domains_DomainId",
                table: "ApiKeys");

            migrationBuilder.DropIndex(
                name: "IX_ApiKeys_DomainId",
                table: "ApiKeys");

            migrationBuilder.DropColumn(
                name: "KeyValue",
                table: "ApiKeys");
        }
    }
}
