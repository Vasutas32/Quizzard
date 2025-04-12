using Quizzard.Models.Questions;

namespace Quizzard.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
