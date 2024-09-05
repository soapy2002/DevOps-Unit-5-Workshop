namespace DotnetTemplate.Web.Controllers
{
    using DotnetTemplate.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {

        public HomeController() {}

        public IActionResult FirstPage()
        {
            var model = new FirstPageViewModel();
            return View(model);
        }

        public IActionResult SecondPage()
        {
            var model = new SecondPageViewModel();
            return View(model);
        }
    }
}
