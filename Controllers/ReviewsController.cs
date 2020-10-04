using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly projectWEBContext _context;

        public ReviewsController(projectWEBContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var projectWEBContext = _context.Reviews.Include(r => r.Item);
            return View(await projectWEBContext.ToListAsync());
        }
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            /* var reviews = (from a in _context.Reviews
                            where a.Item.id == id
                            select new { Email = a.registeredUsers.Email, Writer = a.registeredUsers.UserName, Comment = a.CommentBody, Title = a.CommentTitle }).ToList();
             */
            //var reviews = _context.Reviews.Where(m => m.ItemId == id||m.Item.id==id);
            var reviews = (from a in _context.Reviews
                           join b in _context.RegisteredUsers on a.registeredUsers.id equals b.id
                           where a.ItemId == id
                           select new viewclass { Reviews = a, registeredUsers = b }
                           ) ;
            if (reviews.Count()<1)
            {
                return NotFound();
            }

            return View(reviews);
        }
       // /*
        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }
     //   */
        // GET: Reviews/Create
        
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.Item, "id", "id");
            return View();
        }
        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.         public async Task<IActionResult> Create([Bind("ItemId,CommentTitle,CommentBody,Rate")] Reviews reviews)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string ItemId,string CommentBody,string CommentTitle,int rating)
        {
            if (ItemId != null && CommentBody != null)
            {
                Reviews reviews = new Reviews { CommentBody = CommentBody, Item = _context.Item.First(u => u.id.ToString() == ItemId),registeredUsers=_context.RegisteredUsers.First(usr=>usr.id.ToString()== HttpContext.Session.GetString("userId")),Rate=rating,PublishTime=DateTime.Now,CommentTitle=CommentTitle };
                
                _context.Add(reviews);
                await _context.SaveChangesAsync();
               
                return Json("Success");
            }
            return Json("faild");
        }
    /*    public async Task<IActionResult> Create([Bind("Id,ItemId,CommentBody")] Reviews reviews,string email)
        {
            if (ModelState.IsValid)
            {
               
                /*  _context.Add(reviews);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
               *#/
                var usrid = HttpContext.Session.GetString("userId");
                reviews.registeredUsers = _context.RegisteredUsers.First(u => u.id.ToString() == usrid&&u.Email==email);
                reviews.PublishTime = DateTime.Today;
                return Json(reviews);
            }
           // ViewData["ItemId"] = new SelectList(_context.Item, "id", "id", reviews.ItemId);
            return Json(reviews);
        }*/

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Item, "id", "id", reviews.ItemId);
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemId,CommentTitle,CommentBody,Rate,PublishTime")] Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.Id))
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
            ViewData["ItemId"] = new SelectList(_context.Item, "id", "id", reviews.ItemId);
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .Include(r => r.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
