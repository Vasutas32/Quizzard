using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzard.Models.Questions
{
    public class PairingQuestion : OptionsQuestion
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
        // In PairingQuestion.cs
        public void SyncAnswerOptionsFromColumns()
        {
            // 1. Separate existing options into A and B lists based on their current index
            int aCount = ColumnA.Count;
            int bCount = ColumnB.Count;
            int oldCount = AnswerOptions.Count;

            // Safety check: Calculate the true count of the smaller list to use as 'half'
            int existingACount = Math.Min(oldCount, aCount);
            int existingBCount = Math.Min(oldCount - existingACount, bCount);

            // Extract existing objects from AnswerOptions
            var existingAOptions = AnswerOptions.Take(existingACount).ToList();
            var existingBOptions = AnswerOptions.Skip(existingACount).Take(existingBCount).ToList();

            // 2. Build the new A list, reusing existing options
            var newAnswerOptions = new List<AnswerOption>();
            for (int i = 0; i < newAnswerOptions.Count; i++)
            {
                newAnswerOptions[i].OrderIndex = i;
            }

            // Synchronize Column A (A options are the first part of the list)
            for (int i = 0; i < ColumnA.Count; i++)
            {
                if (i < existingAOptions.Count)
                {
                    // Reuse existing option and update its text
                    existingAOptions[i].OptionText = ColumnA[i];
                    newAnswerOptions.Add(existingAOptions[i]);
                }
                else
                {
                    // New option (new row added)
                    newAnswerOptions.Add(new AnswerOption { OptionText = ColumnA[i] });
                }
            }

            // 3. Build the new B list, reusing existing options
            // Synchronize Column B (B options start after A options)
            for (int i = 0; i < ColumnB.Count; i++)
            {
                if (i < existingBOptions.Count)
                {
                    // Reuse existing option and update its text
                    existingBOptions[i].OptionText = ColumnB[i];
                    newAnswerOptions.Add(existingBOptions[i]);
                }
                else
                {
                    // New option (new row added)
                    newAnswerOptions.Add(new AnswerOption { OptionText = ColumnB[i] });
                }
            }

            // 4. Replace the list with the synchronized options
            // This preserves the ClientIds of all reused objects.
            AnswerOptions = newAnswerOptions;
        }
        public void SyncCorrectAnswerFromPairs()
        {
            // Use LINQ to transform the dictionary into a sequence of ID-pairs.
            CorrectAnswer = string.Join(";",
                CorrectPairs
                    // Convert each KeyValuePair<int AIndex, int BIndex>
                    .Select(kv =>
                    {
                        // Format: "A_ID:B_ID"
                        return $"{kv.Key}:{kv.Value + this.ColumnA.Count}";
                    })
                    //Order the pairs alphabetically by the entire string (A_ID:B_ID) to ensure the order is deterministic for correct answer checking.
                    .OrderBy(s => s)
            );
        }
        public override bool IsAnswerCorrect(UserAnswer userAnswer)
        {
            return userAnswer.SelectedAnswer.Equals(this.CorrectAnswer);

        }

        public override Question Copy()
        {
            var copy = (PairingQuestion)this.MemberwiseClone();

            //Deep-clone the text lists
            copy.ColumnA = new List<string>(this.ColumnA);
            copy.ColumnB = new List<string>(this.ColumnB);

            //Generate a new set of AnswerOptions for the copy with new Client IDs
            copy.AnswerOptions = new List<AnswerOption>();
            foreach (var text in copy.ColumnA) copy.AnswerOptions.Add(new AnswerOption { OptionText = text });
            foreach (var text in copy.ColumnB) copy.AnswerOptions.Add(new AnswerOption { OptionText = text });
            for (int i = 0; i < copy.AnswerOptions.Count; i++)
            {
                copy.AnswerOptions[i].OrderIndex = i;
            }

            //Find the old A/B indices, then find the new A/B ClientIds at those indices.
            copy.CorrectPairs = new Dictionary<int, int>(this.CorrectPairs);

            //Logic for when using GUID
            //foreach (var kvp in this.CorrectPairs)
            //{
            //    //Find the index of the old ClientId (kvp.Key) in the original AnswerOptions list
            //    var oldAIndex = this.AnswerOptions.FindIndex(opt => opt.OrderIndex == kvp.Key);
            //    var oldBIndex = this.AnswerOptions.FindIndex(opt => opt.OrderIndex == kvp.Value);

            //    //Get the new ClientIds from the same indices in the copy's AnswerOptions list
            //    if (oldAIndex != -1 && oldBIndex != -1)
            //    {
            //        var newAClientId = copy.AnswerOptions[oldAIndex].OrderIndex;
            //        var newBClientId = copy.AnswerOptions[oldBIndex].OrderIndex;

            //        //Add the new mapped pair
            //        copy.CorrectPairs[newAClientId] = newBClientId;
            //    }
            //}

            //Update the final CorrectAnswer string using the newly mapped IDs.
            copy.SyncCorrectAnswerFromPairs();

            return copy;
        }
    }

}
