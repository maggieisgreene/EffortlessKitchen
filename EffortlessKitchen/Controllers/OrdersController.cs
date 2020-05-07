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
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EffortlessKitchen.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> ChooseChef()
        {
            var chefs = await _context.Chef.ToListAsync();
            var list = new List<ChooseChefViewModel>();

            foreach (var item in chefs)
            {
                var chef = new ChooseChefViewModel()
                {
                    ChefId = item.ChefId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    IsChecked = false
                };

                list.Add(chef);
            }

            return View(list);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orders/Create
        public async Task<ActionResult> Create([FromRoute] int id)
        {
            var chefMenus = await _context.ChefMenu
                .Where(cm => cm.ChefId == id)
                .Select(cm => new SelectListItem() { Text = cm.MenuOption.Name, Value = cm.ChefMenuId.ToString() })
                .ToListAsync();

            var view = new OrderFormViewModel();

            view.ChefMenus = chefMenus;

            return View(view);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("DateTime, GuestCount, DateCreated, ApplicationUserId, ChefMenuId")] OrderFormViewModel vm)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var order = new Order()
                {
                    DateTime = vm.DateTime,
                    GuestCount = vm.GuestCount,
                    DateCreated = DateTime.Now,
                    ApplicationUserId = user.Id,
                    ChefMenuId = vm.ChefMenuId
                };


                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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