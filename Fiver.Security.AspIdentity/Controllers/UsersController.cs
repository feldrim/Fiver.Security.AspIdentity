using System.Linq;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;

        public UsersController(
            UserManager<AppIdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = userManager.Users.ToList();
            return View(viewModel);
        }
    }
}