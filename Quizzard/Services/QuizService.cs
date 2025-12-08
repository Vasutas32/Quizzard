using Quizzard.Data;
using Quizzard.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quizzard.Models.Questions;

namespace Quizzard.Services
{
    public class QuizService
    {
        private readonly QuizDbContext _context;

        public QuizService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetQuizzesAsync()
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => (q as OptionsQuestion).AnswerOptions) // Ensure answer options are loaded
                .ToListAsync();
        }

        public async Task<List<Quiz>> GetQuizzesWithoutQuestionsAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => (q as OptionsQuestion).AnswerOptions) // Ensure answer options are loaded
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task AddAnswerOptionsAsync(List<AnswerOption> options)
        {
            _context.AnswerOptions.AddRange(options);
            await _context.SaveChangesAsync();
        }



        public async Task SaveQuizResultAsync(UserQuizResult quizResult)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => (q as OptionsQuestion).AnswerOptions) // Include answer options
                .FirstOrDefaultAsync(q => q.Id == quizResult.QuizId);

            if (quiz == null) throw new Exception("Quiz not found");

            //foreach (var answer in quizResult.Answers)
            //{
            //    var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            //    if (question != null)
            //    {
            //        var correctOption = question.AnswerOptions[question.CorrectAnswer];
            //    }
            //}

            quizResult.CorrectAnswers = quizResult.Answers.Count(a => a.IsCorrect);
            quizResult.WrongAnswers = quizResult.Answers.Count(a => !a.IsCorrect);

            _context.UserQuizResults.Add(quizResult);
            await _context.SaveChangesAsync();
        }


        public async Task<QuizStatistic> GetQuizStatisticsAsync(int quizId)
        {
            // Load all completed results and their answers:
            var results = await _context.UserQuizResults
                .Include(r => r.Answers)
                .Where(r => r.QuizId == quizId)
                .ToListAsync();

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz == null) throw new Exception("Quiz not found");

            double avgScore = 0;
            if (results.Count > 0)
            {
                avgScore = results.Average(r => (double)r.CorrectAnswers / quiz.Questions.Count);
            }

            var stats = new QuizStatistic
            {
                QuizId = quizId,
                QuizTitle = quiz.Title,
                TotalAttempts = results.Count,
                PerfectAttempts = results.Count(r => r.WrongAnswers == 0),
                AverageScore = avgScore
            };

            // Per-question
            foreach (var q in await _context.Questions
                   .Where(q => q.QuizId == quizId)
                   .Include(q => (q as OptionsQuestion).AnswerOptions)
                   .ToListAsync())
            {
                var qStat = new QuestionStatistic
                {
                    QuestionId = q.Id,
                    QuestionText = q.Text,
                    TotalCount = results.SelectMany(r => r.Answers)
                                          .Count(a => a.QuestionId == q.Id),
                    CorrectCount = results.SelectMany(r => r.Answers)
                                          .Count(a => a.QuestionId == q.Id && a.IsCorrect)
                };

                // tally picks
                var picks = results.SelectMany(r => r.Answers)
                    .Where(a => a.QuestionId == q.Id);

                if (q is SingleChoiceQuestion || q is MultipleChoiceQuestion)
                {
                    // for each option text, count how many answers used that index
                    foreach (var opt in (q as OptionsQuestion).AnswerOptions)
                    {
                        var idx = (q as OptionsQuestion).AnswerOptions.IndexOf(opt).ToString();
                        qStat.OptionPickCounts[opt.OptionText] =
                            picks.Count(a => a.SelectedAnswer?.Split(';').Contains(idx) == true);
                    }
                }
                else if (q is TrueFalseQuestion)
                {
                    qStat.OptionPickCounts["True"] = picks.Count(a => a.SelectedAnswer == "1");
                    qStat.OptionPickCounts["False"] = picks.Count(a => a.SelectedAnswer == "0");
                }
                else if (q is TextInputQuestion)
                {
                    // count each distinct input
                    foreach (var group in picks.GroupBy(a => a.SelectedAnswer))
                        qStat.OptionPickCounts[group.Key] = group.Count();
                }
                // pairing could be handled similarly…

                stats.Questions.Add(qStat);
            }

            return stats;
        }

        public async Task RegisterUserAsync(UserAccount user)
        {
            _context.UserAccounts.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
