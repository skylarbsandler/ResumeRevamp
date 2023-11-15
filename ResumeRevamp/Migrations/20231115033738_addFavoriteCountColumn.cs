using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeRevamp.Migrations
{
    /// <inheritdoc />
    public partial class addFavoriteCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "favorites_count",
                table: "users",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "favorites_count",
                table: "users");
        }
    }
}
