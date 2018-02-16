using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Security.AspIdentity.ViewModel;

namespace Security.AspIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            var viewModel = new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};

            return View(viewModel);
        }
    }
}