using System;
using System.Collections.Generic;
using System.Text;
using EffortlessKitchen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EffortlessKitchen.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<MenuOption> MenuOption { get; set; }
        public DbSet<Chef> Chef { get; set; }
        public DbSet<ChefMenu> ChefMenu { get; set; }
    }
}
