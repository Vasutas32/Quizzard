using Microsoft.EntityFrameworkCore;
using Quizzard.Models;

namespace Quizzard.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserQuizResult> UserQuizResults { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

    }
}
