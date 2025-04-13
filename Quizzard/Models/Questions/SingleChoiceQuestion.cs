namespace Quizzard.Models.Questions
{
    public class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion()
        {
            Type = QuestionType.SingleChoice;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {

            return string.Equals(userAnswer.SelectedAnswer?.Trim(), this.CorrectAnswer?.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }

}
