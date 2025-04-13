using Quizzard.Models.Questions;

namespace Quizzard.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserQuizResultId { get; set; }
        public UserQuizResult UserQuizResult { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string SelectedAnswer { get; set; } = string.Empty;
        public bool IsCorrect => string.Equals(SelectedAnswer?.Trim(), Question.CorrectAnswer?.Trim(), StringComparison.OrdinalIgnoreCase);
    }

}