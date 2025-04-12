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
            //// Assume UserAnswer contains a list of selected indices (you may need to adjust UserAnswer accordingly)
            //// For simplicity, let's say UserAnswer.SelectedAnswerIndices is a List<int>
            //return userAnswer.SelectedAnswerIndices != null &&
            //       userAnswer.SelectedAnswerIndices.OrderBy(x => x)
            //           .SequenceEqual(CorrectAnswerIndices.OrderBy(x => x));
            return userAnswer.SelectedAnswerIndex == CorrectAnswerIndex;
        }
    }

}
