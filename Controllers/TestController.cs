using Microsoft.AspNetCore.Mvc;

namespace OptimizelyTestProject.Controllers
{
    public class TestController : Controller
    {

        [Route("/test")]
        public IActionResult Index()
        {
            return Json(new { id = 1 });
        }

    }
}