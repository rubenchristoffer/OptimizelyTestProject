using Microsoft.AspNetCore.Mvc;

namespace OptimizelyTestProject.Controllers
{
    [Route("{controller}")]
    public class TestController : Controller
    {

        public IActionResult Index()
        {
            return Json(new { id = 1 });
        }

    }
}
