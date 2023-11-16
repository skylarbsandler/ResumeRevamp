using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ResumeRevamp.Migrations
{
    /// <inheritdoc />
    public partial class AddDefinitionTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "synonyms",
                table: "words",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.CreateTable(
                name: "definition",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    word_id = table.Column<int>(type: "integer", nullable: false),
                    definition_text = table.Column<List<string>>(type: "text[]", nullable: false),
                    part_of_speech = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_definition", x => x.id);
                    table.ForeignKey(
                        name: "fk_definition_words_word_id",
                        column: x => x.word_id,
                        principalTable: "words",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_definition_word_id",
                table: "definition",
                column: "word_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "definition");

            migrationBuilder.AlterColumn<List<string>>(
                name: "synonyms",
                table: "words",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);
        }
    }
}
