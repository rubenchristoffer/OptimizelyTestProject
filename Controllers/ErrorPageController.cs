using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using OptimizelyTestProject.ContentTypes.Pages;
using System.Globalization;

namespace OptimizelyTestProject.Controllers
{
    public class ErrorPageController : PageController<ErrorPage>
    {

        [Route("/error/{statusCode}")]
        public IActionResult Index(string statusCode, ErrorPage currentPage)
        {
            var test1 = ServiceLocator.Current.GetInstance<IContentLanguageAccessor>().Language;
            var test2 = CultureInfo.CurrentCulture;
            var test3 = CultureInfo.CurrentUICulture;

            return View(currentPage);
        }

    }
}