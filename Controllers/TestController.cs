using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test(int? id, string text)
        {
            //var id = Request.RouteValues["id"];
            //var searchText = Request.Query["text];

            ViewBag.Id = id;
            ViewBag.searchText = text;

            return View();
        }
    }
}
