namespace Quizzard.Models.Questions
{

    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice,
        TrueFalse,
        TextInput,
        Pairing
    }

    public abstract class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public QuestionType Type { get; protected set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public string CorrectAnswer { get; set; } = string.Empty;
        public string? ImagePath { get; set; }



        // This abstract method forces each derived class to implement its own answer checking logic.
        public abstract bool IsAnswerCorrect(UserAnswer userAnswer);

        public virtual Question Copy()
        {
            var copy = (Question)this.MemberwiseClone();

            // 2) Deep‑clone the AnswerOptions list itself:
            //copy.AnswerOptions = this.AnswerOptions
            //    .Select(opt => new AnswerOption
            //    {
            //        // copy whichever fields AnswerOption has; at minimum:
            //        OptionText = opt.OptionText
            //    })
            //    .ToList();

            //copy.CorrectAnswer = copy.AnswerOptions.First().OrderIndex.ToString();

            return copy;
        }
    }
}
