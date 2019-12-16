using Microsoft.EntityFrameworkCore.Migrations;

namespace Gmtl.Feedback.Server.Data.Migrations.FeedbackDb
{
    public partial class ChangesInAPIKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiKeys_Domains_DomainId",
                table: "ApiKeys");

            migrationBuilder.DropIndex(
                name: "IX_ApiKeys_DomainId",
                table: "ApiKeys");

            migrationBuilder.AlterColumn<string>(
                name: "KeyValue",
                table: "ApiKeys",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeyValue",
                table: "ApiKeys",
                nullable: true,
                oldClrType: typeof(string));

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
    }
}
