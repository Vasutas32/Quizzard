namespace Quizzard.Models.Questions
{
    public class PairingQuestion : Question
    {
        public List<string> ColumnA { get; set; } = new List<string>();
        public List<string> ColumnB { get; set; } = new List<string>();
        // Mapping: key is index in ColumnA, value is the index in ColumnB that is the correct pair.
        public Dictionary<int, int> CorrectPairs { get; set; } = new Dictionary<int, int>();

        public PairingQuestion()
        {
            Type = QuestionType.Pairing;
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
            return true;

        }
    }

}
