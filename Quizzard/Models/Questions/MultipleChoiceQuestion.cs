namespace Quizzard.Models.Questions
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion()
        {
            Type = QuestionType.MultipleChoice;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            // If either string is empty, treat it as no answer.
            if (string.IsNullOrWhiteSpace(CorrectAnswer) || string.IsNullOrWhiteSpace(userAnswer.SelectedAnswer))
                return false;

            var correct = CorrectAnswer
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .OrderBy(x => x)
                .ToList();

            var selected = userAnswer.SelectedAnswer
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .OrderBy(x => x)
                .ToList();

            return correct.SequenceEqual(selected);
        }
    }

}
