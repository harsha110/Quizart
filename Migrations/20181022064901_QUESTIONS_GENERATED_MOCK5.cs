using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class QUESTIONS_GENERATED_MOCK5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "question_id",
                table: "option_Table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_option_Table_question_id",
                table: "option_Table",
                column: "question_id");

            migrationBuilder.AddForeignKey(
                name: "FK_option_Table_Question_Table_question_id",
                table: "option_Table",
                column: "question_id",
                principalTable: "Question_Table",
                principalColumn: "question_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_option_Table_Question_Table_question_id",
                table: "option_Table");

            migrationBuilder.DropIndex(
                name: "IX_option_Table_question_id",
                table: "option_Table");

            migrationBuilder.DropColumn(
                name: "question_id",
                table: "option_Table");
        }
    }
}
