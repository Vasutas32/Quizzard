namespace Quizzard.Models.Questions
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion()
        {
            Type = QuestionType.TrueFalse;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            return (userAnswer.SelectedAnswer == this.CorrectAnswer);
        }
    }

}
