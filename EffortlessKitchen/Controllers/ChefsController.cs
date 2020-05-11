using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessKitchen.Data;
using EffortlessKitchen.Models;
using EffortlessKitchen.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<ActionResult> Create()
        {
            var vm = new ChefFormViewModel();

            var menuOptions = await _context.MenuOption
                .Select(mo => new SelectListItem()
                {
                    Text = mo.Name,
                    Value = mo.MenuOptionId.ToString()
                }).ToListAsync();

            vm.MenuOptions = menuOptions;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ChefId, FirstName, LastName, Description, Specialties, Price, ChefMenus, SelectedMenuOptionIds")] ChefFormViewModel vm)
        {
            var chef = new Chef()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Description = vm.Description,
                Specialties = vm.Specialties,
                Price = vm.Price
            };

            if (vm.SelectedMenuOptionIds != null)
            {
                chef.ChefMenus = vm.SelectedMenuOptionIds.Select(menuId => new ChefMenu()
                {
                    ChefId = chef.ChefId,
                    MenuOptionId = menuId
                }).ToList();
            }

            _context.Chef.Add(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var vm = new ChefFormViewModel();

            var chef = await _context.Chef
                .Include(c => c.ChefMenus)
                .FirstOrDefaultAsync(c => c.ChefId == id);

            var menuOptions = await _context.MenuOption
                .Select(mo => new SelectListItem()
                {
                    Text = mo.Name,
                    Value = mo.MenuOptionId.ToString()
                })
                .ToListAsync();

            vm.FirstName = chef.FirstName;
            vm.LastName = chef.LastName;
            vm.Description = chef.Description;
            vm.Specialties = chef.Specialties;
            vm.Price = chef.Price;
            vm.MenuOptions = menuOptions;
            vm.SelectedMenuOptionIds = chef.ChefMenus.Select(cm => cm.MenuOptionId).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ChefFormViewModel chef)
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

                uchef.ChefMenus = chef.SelectedMenuOptionIds.Select(menuId => new ChefMenu()
                {
                    ChefId = chef.ChefId,
                    MenuOptionId = menuId
                }).ToList();

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