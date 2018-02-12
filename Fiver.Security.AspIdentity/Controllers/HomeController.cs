using System.Diagnostics;
using Fiver.Security.AspIdentity.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
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