using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzard.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedAnswerIndexToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedAnswerIndex",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswerIndex",
                table: "Questions");
        }
    }
}
