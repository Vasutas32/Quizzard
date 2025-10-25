namespace Quizzard.Models.Questions
{
    public class SingleChoiceQuestion : OptionsQuestion
    {
        public SingleChoiceQuestion()
        {
            Type = QuestionType.SingleChoice;
        }

        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {

            return string.Equals(userAnswer.SelectedAnswer?.Trim(), this.CorrectAnswer?.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public override Question Copy()
        {
            var copy = (SingleChoiceQuestion)this.MemberwiseClone();

            //2) Deep‑clone the AnswerOptions list itself:
            copy.AnswerOptions = this.AnswerOptions
                .Select(opt => new AnswerOption
                {
                    // copy whichever fields AnswerOption has; at minimum:
                    OptionText = opt.OptionText
                })
                .ToList();

            copy.CorrectAnswer = this.CorrectAnswer;

            return copy;
        }
    }

}
