using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeRevamp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefinitionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_definition_words_word_id",
                table: "definition");

            migrationBuilder.AlterColumn<int>(
                name: "word_id",
                table: "definition",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "part_of_speech",
                table: "definition",
                type: "text",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<string>(
                name: "definition_text",
                table: "definition",
                type: "text",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AddForeignKey(
                name: "fk_definition_words_word_id",
                table: "definition",
                column: "word_id",
                principalTable: "words",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_definition_words_word_id",
                table: "definition");

            migrationBuilder.AlterColumn<int>(
                name: "word_id",
                table: "definition",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "part_of_speech",
                table: "definition",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<List<string>>(
                name: "definition_text",
                table: "definition",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "fk_definition_words_word_id",
                table: "definition",
                column: "word_id",
                principalTable: "words",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
