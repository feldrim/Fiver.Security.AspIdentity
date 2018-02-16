using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Security.AspIdentity.Models;

namespace Security.AspIdentity.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public RoleController(RoleManager<AppIdentityRole> roleManager)
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