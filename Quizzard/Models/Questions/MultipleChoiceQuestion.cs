using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzard.Models.Questions
{
    public class MultipleChoiceQuestion : Question
    {
        [NotMapped]
        public List<string> CorrectAnswerOptions { get; set; } = new List<string>();

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

        public override Question Copy()
        {
            var copy = (MultipleChoiceQuestion)this.MemberwiseClone();

            // 2) Deep‑clone the AnswerOptions list itself:
            copy.AnswerOptions = this.AnswerOptions
                .Select(opt => new AnswerOption
                {
                    // copy whichever fields AnswerOption has; at minimum:
                    OptionText = opt.OptionText
                })
                .ToList();

            var newAnswerOptions = new List<string>();
            for(int i = 0; i < this.AnswerOptions.Count; i++)
            {
                var option = this.AnswerOptions[i];
                if (this.CorrectAnswerOptions.Contains(option.ClientId.ToString()))
                {
                    newAnswerOptions.Add(copy.AnswerOptions[i].ClientId.ToString());
                }
            }

            copy.CorrectAnswerOptions = new List<string>(newAnswerOptions);

            return copy;
        }
    }

}
