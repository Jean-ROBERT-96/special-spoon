using Microsoft.AspNetCore.Mvc;
using TestModel;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            //TestViewModel model = new TestViewModel("Marriott", "Louis", 32);
            TestData repo = new TestData();
            var listUsers = repo.users;

            return View(listUsers);
        }
    }
}
