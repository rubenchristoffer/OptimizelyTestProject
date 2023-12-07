using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using OptimizelyTestProject.ContentTypes.Pages;

namespace OptimizelyTestProject.Controllers
{
    public class StartPageController : PageController<StartPage>
    {

        public IActionResult Index(StartPage currentPage)
        {
            return View(currentPage);
        }

    }
}