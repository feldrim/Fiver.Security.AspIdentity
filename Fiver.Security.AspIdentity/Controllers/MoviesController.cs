using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}