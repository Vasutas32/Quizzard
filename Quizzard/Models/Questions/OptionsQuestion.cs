namespace Quizzard.Models.Questions
{
    public abstract class OptionsQuestion : Question
    {

        public bool IsShuffled { get; set; } = false;
        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();


        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
