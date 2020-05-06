using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessKitchen.Data;
using EffortlessKitchen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EffortlessKitchen.Controllers
{
    [Authorize]
    public class ChefsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ChefsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Chef.ToListAsync());
        }

        public async Task<ActionResult> Details(int id)
        {
            var chef = await _context.Chef
                .Where(c => c.ChefId == id)
                .FirstOrDefaultAsync();

            return View(chef);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ChefId, FirstName, LastName, Description, Specialties, Price")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        // GET: Chef/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chef/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Chef/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var chef = await _context.Chef.FirstOrDefaultAsync(c => c.ChefId == id);

            return View(chef);
        }

        // POST: Chef/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Chef chef)
        {
            try
            {
                chef.ChefId = id;
                _context.Chef.Remove(chef);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}