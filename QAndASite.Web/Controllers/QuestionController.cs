using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAndASite.Data;
using QAndASite.Web.Models;

namespace QAndASite.Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private string _connectionString;
        public QuestionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [AllowAnonymous]
        public IActionResult ViewQuestion(int questionId)
        {
            var repo = new QuestionRepository(_connectionString);
            ViewQuestionVM vm = new()
            {
                Question = repo.GetQuestionById(questionId)
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddAnswer(Answer answer, int questionId)
        {
            var qRepo = new QuestionRepository(_connectionString);
            var uRepo = new UserRepository(_connectionString);

            int userId = uRepo.GetUserByEmail(User.Identity.Name).Id;

            answer.DatePosted = DateTime.Now;
            qRepo.AddAnswer(answer, questionId, userId);

            return RedirectToAction($"viewquestion?id={questionId}");
        }

        public IActionResult AskAQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuestion(Question question, List<string> tags)
        {
            var qRepo = new QuestionRepository(_connectionString);
            var uRepo = new UserRepository(_connectionString);
            User user = uRepo.GetUserByEmail(User.Identity.Name);
            question.DatePosted = DateTime.Now;
            qRepo.AddQuestion(question, tags, user);
            return RedirectToAction($"viewquestion?questionId={question.Id}");
        }
    }
}
