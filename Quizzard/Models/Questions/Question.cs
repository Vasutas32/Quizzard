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
        //TODO: REMOVE
        public QuestionType Type { get; protected set; }
        //Foreign key
        public int QuizId { get; set; }
        //Navigation property
        public Quiz Quiz { get; set; }
        public string CorrectAnswer { get; set; } = string.Empty;
        public string? ImagePath { get; set; }



        // This abstract method forces each derived class to implement its own answer checking logic.
        public abstract bool IsAnswerCorrect(UserAnswer userAnswer);

        public virtual Question Copy()
        {
            var copy = (Question)this.MemberwiseClone();

            return copy;
        }

    }
}
