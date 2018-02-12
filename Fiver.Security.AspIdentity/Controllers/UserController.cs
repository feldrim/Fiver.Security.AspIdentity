using System;
using System.Linq;
using System.Threading.Tasks;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;


        public UserController(
            UserManager<AppIdentityUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewModel = _userManager.Users.ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.Users.AsNoTracking().SingleAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userManager.Users
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(m => string.Equals(m.Id, id, StringComparison.Ordinal));
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    if (saveChangesError.GetValueOrDefault())
        //    {
        //        _logger.LogError(
        //            "Delete failed. Try again, and if the problem persists see your system administrator.");
        //    }

        //    return View(user);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var user = await _userManager.Users
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(m => m.Id == id);

        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    try
        //    {
        //        await _userManager.DeleteAsync(user);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.)
        //        _logger.LogError(ex, "Delete failed. Try again, and if the problem persists see your system administrator.");

        //        return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
        //    }
        //}

    }
}