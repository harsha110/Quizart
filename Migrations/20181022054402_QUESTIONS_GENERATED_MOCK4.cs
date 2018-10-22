using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class QUESTIONS_GENERATED_MOCK4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "option_Table",
                columns: table => new
                {
                    option_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    option = table.Column<string>(nullable: true),
                    iscorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option_Table", x => x.option_id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    template_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    topic = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    sparql = table.Column<string>(nullable: true),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.template_id);
                });

            migrationBuilder.CreateTable(
                name: "Question_Table",
                columns: table => new
                {
                    question_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    topic = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    questions = table.Column<string>(nullable: true),
                    template_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Table", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_Question_Table_Template_template_id",
                        column: x => x.template_id,
                        principalTable: "Template",
                        principalColumn: "template_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Table_template_id",
                table: "Question_Table",
                column: "template_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "option_Table");

            migrationBuilder.DropTable(
                name: "Question_Table");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}
