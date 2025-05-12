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
        public List<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
        public string CorrectAnswer { get; set; } = string.Empty;
        public string? ImagePath { get; set; }



        // This abstract method forces each derived class to implement its own answer checking logic.
        public abstract bool IsAnswerCorrect(UserAnswer userAnswer);
    }
}
