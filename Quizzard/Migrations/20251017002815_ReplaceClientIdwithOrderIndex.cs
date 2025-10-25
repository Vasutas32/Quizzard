using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzard.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceClientIdwithOrderIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Questions_QuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AnswerOptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsShuffled",
                table: "Questions",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MultipleChoiceQuestionId",
                table: "AnswerOptions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "AnswerOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PairingQuestionId",
                table: "AnswerOptions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SingleChoiceQuestionId",
                table: "AnswerOptions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_MultipleChoiceQuestionId",
                table: "AnswerOptions",
                column: "MultipleChoiceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_PairingQuestionId",
                table: "AnswerOptions",
                column: "PairingQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_SingleChoiceQuestionId",
                table: "AnswerOptions",
                column: "SingleChoiceQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Questions_MultipleChoiceQuestionId",
                table: "AnswerOptions",
                column: "MultipleChoiceQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Questions_PairingQuestionId",
                table: "AnswerOptions",
                column: "PairingQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Questions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Questions_SingleChoiceQuestionId",
                table: "AnswerOptions",
                column: "SingleChoiceQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Questions_MultipleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Questions_PairingQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Questions_QuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerOptions_Questions_SingleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerOptions_MultipleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerOptions_PairingQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerOptions_SingleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "IsShuffled",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MultipleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "PairingQuestionId",
                table: "AnswerOptions");

            migrationBuilder.DropColumn(
                name: "SingleChoiceQuestionId",
                table: "AnswerOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "AnswerOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerOptions_Questions_QuestionId",
                table: "AnswerOptions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
