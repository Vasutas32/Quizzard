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

        public int SelectedAnswerIndex { get; set; }
        public bool IsCorrect => SelectedAnswerIndex == Question.CorrectAnswerIndex;
    }

}