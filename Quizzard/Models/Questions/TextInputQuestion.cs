namespace Quizzard.Models.Questions
{
    public class TextInputQuestion : Question
    {

        // Returns a list of acceptable answers
        public List<string> CorrectAnswersLocal => this.CorrectAnswer
            .Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        public TextInputQuestion()
        {
            Type = QuestionType.TextInput;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            //Fix if needed, probalby need to split up selected answers the same way we did for correct answer
            return CorrectAnswersLocal.Any(ans => string.Equals(ans, userAnswer.SelectedAnswer?.Trim(), StringComparison.OrdinalIgnoreCase));

        }
    }

}
