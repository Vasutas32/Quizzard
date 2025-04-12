using Quizzard.Models.Questions;

namespace Quizzard.Models
{
    public class AnswerOption
    {
        // Primary key for the AnswerOption record.
        public int Id { get; set; }

        // Foreign key linking this answer option to its parent question.
        public int QuestionId { get; set; }

        // Navigation property: The question to which this option belongs.
        public Question Question { get; set; }

        // The text of the answer option.
        public string OptionText { get; set; } = string.Empty;
    }
}
