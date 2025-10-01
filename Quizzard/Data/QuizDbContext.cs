using Microsoft.EntityFrameworkCore;
using Quizzard.Models;
using Quizzard.Models.Questions;

namespace Quizzard.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<UserQuizResult> UserQuizResults { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasDiscriminator<string>("QuestionType")
                .HasValue<SingleChoiceQuestion>("SingleChoice")
                .HasValue<MultipleChoiceQuestion>("MultipleChoice")
                .HasValue<TrueFalseQuestion>("TrueFalse")
                .HasValue<TextInputQuestion>("TextInput")
                .HasValue<PairingQuestion>("Pairing");

            modelBuilder.Entity<Question>()
        .Property<string>("QuestionType")
        .HasMaxLength(50);

            // Configure the AnswerOption relationship if not already done.
            modelBuilder.Entity<AnswerOption>()
                .HasOne(a => a.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(a => a.QuestionId);

            base.OnModelCreating(modelBuilder);
        }


    }
}
