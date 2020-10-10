using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace projectWEB.Controllers
{
    public class RegisteredUsersController : Controller
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        private readonly SignInManager<RegisteredUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        public RegisteredUsersController(projectWEBContext context, UserManager<RegisteredUsers> userManager,
                    SignInManager<RegisteredUsers> signInManager, RoleManager<IdentityRole> roleManager,
                    ILogger<RegisteredUsersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<JsonResult> Register(RegisterViewModel model)
        {
            if (_context.RegisteredUsers.Where(u => u.UserName == model.Username).Count() > 0)
            {
                return Json(new { Success = true, Messages = string.Join("<br />", "This User already exists") });

            }
            if (ModelState.IsValid)
            {
                RegisteredUsers registeredUsers = new RegisteredUsers()
                {
                    FullName = model.FullName,
                    UserName = model.Username,
                    Email = model.Email,
                    RegisteredOn = DateTime.Now
                };
                var result = await _userManager.CreateAsync(registeredUsers, model.Password);
                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync("User"))
                    {
                        //assign User role to newly registered user
                        await _userManager.AddToRoleAsync(registeredUsers, "User");
                    }
                    _context.Add(registeredUsers);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(registeredUsers, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return Json(new { Success = true });
                }
                else
                {
                    // If we got this far, something failed, redisplay form
                    AddErrors(result);
                    return Json(new { Success = false, Messages = string.Join("<br />", result) });
                }
            }
            return new JsonResult(new object());;
        }
        public async Task<IActionResult> ChangeAvatar()
        {
            string userId = _userManager.GetUserId(User);

            ProfileDetailsViewModel model = new ProfileDetailsViewModel
            {
                User = await _userManager.FindByIdAsync(userId)
            };

            return PartialView("_ChangeAvatar", model);
        }
        public async Task<JsonResult> UpdateAvatar(int pictureID)
        {
            if (pictureID > 0 && User.Identity.IsAuthenticated)
            {
                string userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    user.PictureID = pictureID;

                    var result = await _userManager.UpdateAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return new JsonResult( new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) });
                }
            }
            else
            {
                return new JsonResult( new { Success = false, Message = "Invalid User" });
            }

            return new JsonResult(new object()); ;
        }
        //[HttpPost]
        //private async void signin(RegisteredUsers user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name,user.UserName),
        //        //new Claim(ClaimTypes.Role,user.MemberType.ToString()),
        //    };

        //    var claimsIdentity  = new ClaimsIdentity(
        //        claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var authProperties = new AuthenticationProperties { };


        //    await HttpContext.SignInAsync(
        //      CookieAuthenticationDefaults.AuthenticationScheme,
        //       new ClaimsPrincipal(claimsIdentity),
        //       authProperties);

        //    HttpContext.Session.SetInt32("userId", int.Parse(user.Id));
        //    //return Redirect("/items/index");

        //    /* moved this to check another kind of auth 
        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var authproperties = new AuthenticationProperties { /* ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)* / };
        //    //  var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authproperties);
        //    // HttpContext.Session.SetString("Logged", "5");
        //  //  RedirectToAction( "item","Items");
        //  */
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> loginAsync(string returnUrl)
        { // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Json(new { Success = true, RequiresVerification = false });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Json(new { Success = false, Messages = "PP.Login.Validation.LockedOut" });
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Json(new { Success = false, Messages = "PP.Login.Validation.InvalidLoginAttempt" });
                }
                
            }
            // If we got this far, something failed, redisplay form

            return Json(new { Success = false, Messages = "PP.Login.Validation.InvalidLoginAttempt" });
            
        }
        public async Task<IActionResult> UserProfileAsync(string tab)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileDetailsViewModel model = new ProfileDetailsViewModel
            {
                ActiveTab = tab,

                User = user
            };

            if (model.User == null) return NotFound();
            bool isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                return PartialView("_UserProfile", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UpdateProfile(UpdateProfileDetailsViewModel model)
        {
            if (model != null && User.Identity.IsAuthenticated)
            {
                string userId = _userManager.GetUserId(User);

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.UserName = model.Username;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.Address = model.Address;
                    user.ZipCode = model.ZipCode;

                    var result = await _userManager.UpdateAsync(user);

                    return Json( new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) });
                }
            }
            else
            {
                return Json(new { Success = false, Message = "Invalid User" });
            }

            return new JsonResult(new object());;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public async Task<IActionResult> ChangePassword()
        {
            string userId = _userManager.GetUserId(User);

            ProfileDetailsViewModel model = new ProfileDetailsViewModel
            {
                User = await _userManager.FindByIdAsync(userId)
            };

            return PartialView("_ChangePassword", model);
        }


        public async Task<JsonResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            if (model != null && User.Identity.IsAuthenticated)
            {
                string userId = _userManager.GetUserId(User);

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);


                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Json(new { Success = result.Succeeded, Message = string.Join("\n", result.Errors) });

                }
            }
            else
            {
                return Json(new { Success = false, Message = "Invalid User" });
            }

            return new JsonResult(new object());
        }
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string code, string userId)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                Code = code,
                UserId = userId
            };

            return string.IsNullOrEmpty(code) || string.IsNullOrEmpty(userId) ? View("Error") : View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (!result.Succeeded)
                {
                    return Json( new { Success = result.Succeeded, Messages = string.Join("\n", result.Errors) });
                }
                else
                {
                    return Json(new { Success = result.Succeeded, Messages = "Your password has been reset. Please login with your updated credentials now." });
                }
            }
            else
            {
                return Json(new { Success = false, Messages = "Unable to reset password." });
            }
        }
        // GET: RegisteredUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegisteredUsers.ToListAsync());
        }
    }
}
