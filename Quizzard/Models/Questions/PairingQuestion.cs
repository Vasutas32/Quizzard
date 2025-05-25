using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzard.Models.Questions
{
    public class PairingQuestion : Question
    {
        [NotMapped]
        public List<string> ColumnA { get; set; } = new List<string>();
        [NotMapped]
        public List<string> ColumnB { get; set; } = new List<string>();


        [NotMapped]
        public Dictionary<int, int> CorrectPairs { get; set; } = new();

        public PairingQuestion()
        {
            Type = QuestionType.Pairing;
        }
        public void SyncAnswerOptionsFromColumns()
        {
            AnswerOptions.Clear();
            foreach (var text in ColumnA) AnswerOptions.Add(new AnswerOption { OptionText = text });
            foreach (var text in ColumnB) AnswerOptions.Add(new AnswerOption { OptionText = text });
        }
        public void SyncCorrectAnswerFromPairs()
        {
            // e.g. pairs {0→2, 1→0} → "02;10"
            CorrectAnswer = string.Join(";",
              CorrectPairs
                .OrderBy(kv => kv.Key)
                .Select(kv => $"{kv.Key}{kv.Value}")
            );
        }
        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            return userAnswer.SelectedAnswer.Equals(this.CorrectAnswer);

        }

        public override Question Copy()
        {
            var copy = (PairingQuestion)this.MemberwiseClone();

            // Deep‑clone the AnswerOptions list itself:
            copy.AnswerOptions = this.AnswerOptions
                .Select(opt => new AnswerOption
                {
                    // copy whichever fields AnswerOption has; at minimum:
                    OptionText = opt.OptionText
                })
                .ToList();
            copy.ColumnA = new List<string>(this.ColumnA);
            copy.ColumnB = new List<string>(this.ColumnB);

            return copy;
        }
    }

}
