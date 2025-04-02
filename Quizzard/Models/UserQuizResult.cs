namespace Quizzard.Models
{
    public class UserQuizResult
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

        public string? UserId { get; set; } // Nullable for anonymous users
        public List<UserAnswer> Answers { get; set; } = new List<UserAnswer>();
    }

}
