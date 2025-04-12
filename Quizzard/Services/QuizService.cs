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
                .ThenInclude(q => q.AnswerOptions) // Ensure answer options are loaded
                .ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.AnswerOptions) // Ensure answer options are loaded
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
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
                .ThenInclude(q => q.AnswerOptions) // Include answer options
                .FirstOrDefaultAsync(q => q.Id == quizResult.QuizId);

            if (quiz == null) throw new Exception("Quiz not found");

            foreach (var answer in quizResult.Answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question != null)
                {
                    var correctOption = question.AnswerOptions[question.CorrectAnswerIndex];
                }
            }

            quizResult.CorrectAnswers = quizResult.Answers.Count(a => a.IsCorrect);
            quizResult.WrongAnswers = quizResult.Answers.Count(a => !a.IsCorrect);

            _context.UserQuizResults.Add(quizResult);
            await _context.SaveChangesAsync();
        }



        //public async Task<Dictionary<int, int[]>> GetQuestionStatisticsAsync(int quizId)
        //{
        //    var statistics = await _context.UserAnswers
        //        .Where(a => a.Question.Quiz.Id == quizId)
        //        .GroupBy(a => a.QuestionId)
        //        .Select(g => new
        //        {
        //            QuestionId = g.Key,
        //            OptionCounts = g.GroupBy(a => a.SelectedAnswerIndex)
        //                            .ToDictionary(opt => opt.Key, opt => opt.Count())
        //        })
        //        .ToDictionaryAsync(x => x.QuestionId, x => x.OptionCounts);

        //    return statistics;
        //}
    }
}
