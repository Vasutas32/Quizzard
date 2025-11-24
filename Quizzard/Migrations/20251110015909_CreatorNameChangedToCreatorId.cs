using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzard.Migrations
{
    /// <inheritdoc />
    public partial class CreatorNameChangedToCreatorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Quizzes",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Quizzes");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Quizzes",
                type: "text",
                nullable: true);
        }
    }
}
