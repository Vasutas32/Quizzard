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

            modelBuilder.Entity<AnswerOption>()
        .HasOne(a => a.Question) // AnswerOption has a Question property of type Question
        .WithMany() // The inverse collection on the Question base class doesn't exist, so we use WithMany() or configure it on the derived class.
        .HasForeignKey(a => a.QuestionId)
        .IsRequired(); // An AnswerOption MUST belong to a Question

            modelBuilder.Entity<OptionsQuestion>()
        .HasMany(q => q.AnswerOptions)
        .WithOne() // The inverse is configured in step 1, so we use WithOne() without a lambda
        .HasForeignKey(a => a.QuestionId);


            base.OnModelCreating(modelBuilder);
        }


    }
}
