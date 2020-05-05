using System;
using System.Collections.Generic;
using System.Text;
using EffortlessKitchen.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                Address = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Seeding the Chef table!
            modelBuilder.Entity<Chef>().HasData(
                new Chef()
                {
                    ChefId = 1,
                    FirstName = "Gordon",
                    LastName = "Ramsey",
                    Description = "While Gordon Rasmey is very much a household name in numerous parts of the world, what if he could be in your household? Cooking you a fresh, delicious meal!",
                    Specialties = "Anything and everything!",
                    Price = 200.00
                },
                new Chef()
                {
                    ChefId = 2,
                    FirstName = "Rachel",
                    LastName = "Ray",
                    Description = "This fun, exciteful human is not only an extraordinary person, but an even better chef! Have a good laugh while being guarenteed a great meal with Rachel!",
                    Specialties = "Baking anything to satisfy a sweet tooth!",
                    Price = 200.00
                }
            );

            // Seeding the Menu Option table!
            modelBuilder.Entity<MenuOption>().HasData(
                new MenuOption()
                {
                    MenuOptionId = 1,
                    Name = "Spaghetti Bolognese",
                    Description = "An infinitely timeless and delicious meal: they don't become a staple without being great!",
                    Ingredients = "Spaghetti noodles, ground beef, tomatoes, and spices",
                    Price = 25.00
                },
                new MenuOption()
                {
                    MenuOptionId = 2,
                    Name = "Spicy Chicken Enchiladas",
                    Description = "This version of enchiladas will knock your socks off! Definitely will want to hold onto your margarita a little tighter while this dish is being served." +
                    "Spicy Chicken Enchiladas are served with fresh salsa and gaucamole, tortilla chips, black beans, and cilantro lime rice!",
                    Ingredients = "Flour, Chicken, Avocados, Shredded Cheese, Tomatoes, Peppers, Black Beans, White Rice, and spices",
                    Price = 28.00
                }
            );

            // Seeding the Chef Menu table!
            modelBuilder.Entity<ChefMenu>().HasData(
                new ChefMenu()
                {
                    ChefMenuId = 1,
                    ChefId  = 1,
                    MenuOptionId = 1
                },
                new ChefMenu()
                {
                    ChefMenuId = 2,
                    ChefId = 2,
                    MenuOptionId = 2
                },
                new ChefMenu()
                {
                    ChefMenuId = 3,
                    ChefId = 1,
                    MenuOptionId = 2
                }
            );

            // Seeding the Order table!
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 1,
                    DateTime = new DateTime(2020-06-15),
                    GuestCount = 2,
                    DateCreated = new DateTime(2020-05-5),
                    DateCompleted = new DateTime(2020-04-30),
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    ChefMenuId = 1
                }
            );
        }
    }
}
