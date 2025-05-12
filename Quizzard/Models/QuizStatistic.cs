namespace Quizzard.Models
{
    public class QuizStatistic
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = "";
        public int TotalAttempts { get; set; }
        public int PerfectAttempts { get; set; }
        public double AverageScore { get; set; }
        public List<QuestionStatistics> Questions { get; set; } = new();
    }

    public class QuestionStatistics
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = "";
        // For each possible answer (option text or “True”/“False”/free-text), how many times it was chosen
        public Dictionary<string, int> OptionPickCounts { get; set; } = new();

        // How many times users answered this question correctly
        public int CorrectCount { get; set; }

        // Total times question was attempted
        public int TotalCount { get; set; }
    }
}
