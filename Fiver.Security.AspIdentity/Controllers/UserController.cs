using System.Linq;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public UserController(
            UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = _userManager.Users.ToList();
            return View(viewModel);
        }
    }
}