using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QAndASite.Data;
using QAndASite.Web.Models;
using System.Diagnostics;

namespace QAndASite.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionRepository(_connectionString);
            IndexVM vm = new()
            {
                Questions = repo.GetAllQuestions()
            };
            return View(vm);
        }
    }
}