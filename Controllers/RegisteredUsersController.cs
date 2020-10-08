﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projectWEB.Data;
using projectWEB.Models;
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
        private readonly ILogger _logger;
        public RegisteredUsersController(projectWEBContext context, UserManager<RegisteredUsers> userManager,
                    SignInManager<RegisteredUsers> signInManager,
                    ILogger<RegisteredUsersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        private async void signin(RegisteredUsers user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                //new Claim(ClaimTypes.Role,user.MemberType.ToString()),
            };

            var claimsIdentity  = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { };


            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);

            HttpContext.Session.SetInt32("userId", int.Parse(user.Id));
            //return Redirect("/items/index");

            /* moved this to check another kind of auth 
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authproperties = new AuthenticationProperties { /* ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)* / };
            //  var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authproperties);
            // HttpContext.Session.SetString("Logged", "5");
          //  RedirectToAction( "item","Items");
          */
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("FullName,UserName,Email,Password")] RegisteredUsers registeredUsers)
        {
            if ((_context.RegisteredUsers.Where(u => u.UserName == registeredUsers.UserName).Count()) > 0)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(registeredUsers, registeredUsers.Password);
                if (result.Succeeded)
                {
                    _context.Add(registeredUsers);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(registeredUsers, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return View("index", Index());
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View("item", "Items");

        }

        /*
         public async IActionResult Signup([Bind("Id,UserName,Email,Password,MemberType")] RegisteredUsers registeredUsers)
         {
             if (ModelState.IsValid)
             {
                 if (ModelState.IsValid)
                 {
                     _context.Add(registeredUsers);
                     await _context.SaveChangesAsync();
                     return RedirectToAction(nameof(Index));
                 }
                 return View(registeredUsers);
             }

         }
         /*
         public async Task<IActionResult> Login()
         {
             return View(await _context.RegisteredUsers.ToListAsync());
         }

         */
        public IActionResult login()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Login(String Username, String Password)
        {
            var users = _context.RegisteredUsers.Where(u => u.UserName == Username && u.Password == Password).First();

            if (users != null)
            {
                signin(users);
                return RedirectToAction("mainshop","Items");//View(nameof(Index),);
            }
            //  return View( _context.RegisteredUsers.ToListAsync());
            return View();
        }
        /*
                private void signin()
                {
                   // HttpContext.Session.SetString("Logged", "5");
                }
        */


      
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

        // GET: RegisteredUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegisteredUsers.ToListAsync());
        }
        /*
         * 
         *   public IActionResult Index()
        {
            return View();
        }
        // GET: RegisteredUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers
                .FirstOrDefaultAsync(m => m.id == id);
            if (registeredUsers == null)
            {
                return NotFound();
            }

            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegisteredUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,UserName,Email,Password,MemberType")] RegisteredUsers registeredUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registeredUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers.FindAsync(id);
            if (registeredUsers == null)
            {
                return NotFound();
            }
            return View(registeredUsers);
        }

        // POST: RegisteredUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,UserName,Email,Password,MemberType")] RegisteredUsers registeredUsers)
        {
            if (id != registeredUsers.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registeredUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisteredUsersExists(registeredUsers.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers
                .FirstOrDefaultAsync(m => m.id == id);
            if (registeredUsers == null)
            {
                return NotFound();
            }

            return View(registeredUsers);
        }

        // POST: RegisteredUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registeredUsers = await _context.RegisteredUsers.FindAsync(id);
            _context.RegisteredUsers.Remove(registeredUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisteredUsersExists(int id)
        {
            return _context.RegisteredUsers.Any(e => e.id == id);
        } */
    }
}
