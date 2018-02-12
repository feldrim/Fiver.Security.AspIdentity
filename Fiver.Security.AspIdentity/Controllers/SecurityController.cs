using System;
using System.Threading.Tasks;
using Fiver.Security.AspIdentity.Models.Security;
using Fiver.Security.AspIdentity.Services.Email;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AspIdentity.Controllers
{
    [Authorize]
    public class SecurityController : Controller
    {
        #region " Fields & Constructor "

        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public SecurityController(
            UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager,
            IEmailSender emailSender,
            RoleManager<AppIdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        #endregion

        #region " Login / Logout / Access Denied "

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty,
                        "Confirm your email please");
                    return View(model);
                }

            var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Login Failed");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region " Register "

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new AppIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await this.signInManager.SignInAsync(user, isPersistent: false);
                await _userManager.AddToRoleAsync(user, "DefaultUser");

                //var confrimationCode =
                //    await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //var callbackurl = Url.Action(
                //    controller: "Security",
                //    action: "ConfirmEmail",
                //    values: new { userId = user.Id, code = confrimationCode },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    user.Email,
                //    "Confirm Email",
                //    callbackurl);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return View("ConfirmEmail");

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region " Forgot Password "

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return View();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction("ForgotPasswordEmailSent");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return RedirectToAction("ForgotPasswordEmailSent");

            //var confirmationCode =
            //    await _userManager.GeneratePasswordResetTokenAsync(user);

            //var callbackurl = Url.Action(
            //    controller: "Security",
            //    action: "ResetPassword",
            //    values: new { userId = user.Id, code = confirmationCode },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(
            //    user.Email,
            //    "Reset Password",
            //    callbackurl);

            return RedirectToAction("ForgotPasswordEmailSent");
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        #endregion

        #region " Reset Password "

        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
                throw new ApplicationException("Code must be supplied for password reset.");

            var model = new ResetPasswordViewModel {Code = code};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirm");

            var result = await _userManager.ResetPasswordAsync(
                user, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirm");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }

        #endregion
    }
}