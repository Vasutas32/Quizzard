using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzard.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnswerPropertiesToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswerIndex",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerIndex",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "SelectedAnswer",
                table: "UserAnswers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswer",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "SelectedAnswerIndex",
                table: "UserAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerIndex",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
