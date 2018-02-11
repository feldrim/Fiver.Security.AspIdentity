using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<RoleController> _roleManager;

        protected RoleController(RoleManager<RoleController> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var vievModel = _roleManager.Roles.ToList();
            return View(vievModel);
        }
    }
}