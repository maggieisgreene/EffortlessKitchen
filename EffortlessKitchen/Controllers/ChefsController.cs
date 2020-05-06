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
                .Include(c => c.ChefMenus)
                    .ThenInclude(cm => cm.MenuOption)
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

        public async Task<ActionResult> Edit(int id)
        {
            var chef = await _context.Chef
                .Where(c => c.ChefId == id)
                .FirstOrDefaultAsync();

            return View(chef);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Chef chef)
        {
            try
            {
                var uchef = new Chef()
                {
                    ChefId = id,
                    FirstName = chef.FirstName,
                    LastName = chef.LastName,
                    Description = chef.Description,
                    Specialties = chef.Specialties,
                    Price = chef.Price
                };

                _context.Chef.Update(uchef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var chef = await _context.Chef.FirstOrDefaultAsync(c => c.ChefId == id);

            return View(chef);
        }

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
    }
}