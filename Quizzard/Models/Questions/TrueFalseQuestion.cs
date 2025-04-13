namespace Quizzard.Models.Questions
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueFalseQuestion()
        {
            Type = QuestionType.TrueFalse;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            // Here, we assume that a boolean answer is stored as 0 for false and 1 for true in SelectedAnswerIndex.
            // Alternatively, you could use a dedicated property in UserAnswer for true/false.
            return (userAnswer.SelectedAnswer == "1" && CorrectAnswer) ||
                   (userAnswer.SelectedAnswer == "0" && !CorrectAnswer);
        }
    }

}
