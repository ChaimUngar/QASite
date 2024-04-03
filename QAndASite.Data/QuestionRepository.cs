using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAndASite.Data
{
    public class QuestionRepository
    {
        private readonly string _connectionString;
        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetAllQuestions()
        {
            using var context = new QandADataContext(_connectionString);
            return context.Questions
                .Include(q => q.QuestionsTags)
                .ThenInclude(qt => qt.Tag)
                .Include(q => q.User)
                .Include(q => q.Answers)
                .ToList();
        }

        public Question GetQuestionById(int id)
        {
            using var context = new QandADataContext(_connectionString);
            return context.Questions
                .Include(q => q.QuestionsTags)
                .ThenInclude(qt => qt.Tag)
                .Include(q => q.User)
                .Include(q => q.Answers)
                .ThenInclude(a => a.User)
                .FirstOrDefault(q => q.Id == id);
        }

        public void AddAnswer(Answer answer, int questionId, int userId)
        {
            using var context = new QandADataContext(_connectionString);
            var repo = new UserRepository(_connectionString);
            answer.QuestionId = questionId;
            answer.UserId = userId;
            context.Answers.Add(answer);
            context.SaveChanges();
        }

        public void AddQuestion(Question question, List<string> tags, User user)
        {
            using var context = new QandADataContext(_connectionString);

            context.Questions.Add(question);
            context.SaveChanges();

            foreach (var tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;

                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }

                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            context.SaveChanges();
        }

        private Tag GetTag(string name)
        {
            using var context = new QandADataContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var context = new QandADataContext(_connectionString);
            var tag = new Tag { Name = name };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }
    }
}
