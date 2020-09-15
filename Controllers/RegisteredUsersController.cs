using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public RegisteredUsersController(projectWEBContext context)
        {
            _context = context;
        }

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
                new Claim(ClaimTypes.Role,user.MemberType.ToString()),
            };

            var claimsIdentity  = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { };


            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);

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
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> signup(string UserName,string Email,string Password) 
        public async Task<IActionResult> Signup([Bind("UserName,Email,Password")] RegisteredUsers registeredUsers)
        {
                if((_context.RegisteredUsers.Where(u => u.UserName == registeredUsers.UserName).Count())>0)
                {
                //throw f
                return View();
                }
                if (ModelState.IsValid)
                {
               // RegisteredUsers registeredUsers = new RegisteredUsers() { UserName = UserName, Email = Email, Password = Password };
                _context.Add(registeredUsers);
                    await _context.SaveChangesAsync();
                var users = _context.RegisteredUsers.First(u => u.UserName == registeredUsers.UserName);
                signin(users);
                return View("index", Index());
                }
                return View("item","Items");
            
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
                return RedirectToAction("Index","Items");//View(nameof(Index),);
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
