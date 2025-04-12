namespace Quizzard.Models.Questions
{
    public class TextInputQuestion : Question
    {
        // A string containing correct answers separated by a delimiter (e.g., ";")
        public string CorrectAnswersRaw { get; set; } = string.Empty;

        // Returns a list of acceptable answers
        public List<string> CorrectAnswers => CorrectAnswersRaw
            .Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        public TextInputQuestion()
        {
            Type = QuestionType.TextInput;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            //// Compare the user's text input against each acceptable answer, ignoring case
            //return CorrectAnswers.Any(ans => string.Equals(ans, userAnswer.UserTextAnswer?.Trim(), StringComparison.OrdinalIgnoreCase));
            return true;
        }
    }

}
