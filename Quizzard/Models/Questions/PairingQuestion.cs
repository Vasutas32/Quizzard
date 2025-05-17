using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzard.Models.Questions
{
    public class PairingQuestion : Question
    {
        [NotMapped]
        public List<string> ColumnA { get; set; } = new List<string>();
        [NotMapped]
        public List<string> ColumnB { get; set; } = new List<string>();
        // Mapping: key is index in ColumnA, value is the index in ColumnB that is the correct pair.
        //public Dictionary<int, int> CorrectPairs { get; set; } = new Dictionary<int, int>();

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
        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            //// Assume UserAnswer contains a dictionary mapping indices from ColumnA to ColumnB
            //// e.g., UserAnswer.Pairings: Dictionary<int, int>
            //if (userAnswer.Pairings == null)
            //    return false;

            //// Check if all provided pairs are correct
            //return CorrectPairs.OrderBy(pair => pair.Key)
            //    .SequenceEqual(userAnswer.Pairings.OrderBy(pair => pair.Key));
            return userAnswer.SelectedAnswer.Equals(this.CorrectAnswer);

        }

        public override Question Copy()
        {
            var copy = (PairingQuestion)this.MemberwiseClone();

            // 2) Deep‑clone the AnswerOptions list itself:
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
