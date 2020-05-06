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
    public class MenuOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public MenuOptionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.MenuOption.ToListAsync());
        }

        public async Task<ActionResult> Details(int id)
        {
            var option = await _context.MenuOption
                .Where(mo => mo.MenuOptionId == id)
                .FirstOrDefaultAsync();

            return View(option);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("MenuOptionId, Name, Description, Ingredients, Price")] MenuOption option)
        {
            if (ModelState.IsValid)
            {
                _context.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(option);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var option = await _context.MenuOption
                .Where(mo => mo.MenuOptionId == id)
                .FirstOrDefaultAsync();

            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MenuOption option)
        {
            try
            {
                var uoption = new MenuOption()
                {
                    MenuOptionId = id,
                    Name = option.Name,
                    Description = option.Description,
                    Ingredients = option.Ingredients,
                    Price = option.Price
                };

                _context.MenuOption.Update(uoption);
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
            var option = await _context.MenuOption.FirstOrDefaultAsync(mo => mo.MenuOptionId == id);

            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, MenuOption option)
        {
            try
            {
                option.MenuOptionId = id;
                _context.MenuOption.Remove(option);
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