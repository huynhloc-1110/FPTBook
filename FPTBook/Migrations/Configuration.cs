namespace FPTBook.Migrations
{
    using FPTBook.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Antlr.Runtime.Tree;
    using System.Web.WebSockets;

    internal sealed class Configuration : DbMigrationsConfiguration<FPTBook.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed= true;
            ContextKey = "FPTBook.Models.ApplicationDbContext";
        }

        protected override void Seed(FPTBook.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                string[] roleList = { "Store Owner", "Admin" };

                CreateSeveralRoles(context, roleList);
                CreateSeveralUsers(context);
                CreateSeveralBooks(context);
            }
        }

        private void CreateSeveralRoles(ApplicationDbContext context, string[] roleList)
        {
            foreach (string role in roleList)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleResult = roleManager.Create(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleResult.Errors));
                }
            }
        }

        private void CreateSeveralUsers(ApplicationDbContext context)
        {
            CreateUser(context, "admin@gmail.com", "admin@gmail.com", "System Administrator", "Admin_123", "Admin");
            CreateUser(context, "loc@gmail.com", "loc@gmail.com", "Loc Le", "Loc_123", "Store Owner");
            CreateUser(context, "huy@gmail.com", "huy@gmail.com", "HuyVipPro123", "Huy_123", "Customer");
        }

        private void CreateUser(ApplicationDbContext context, string email, string userName, string fullName, string password, string role)
        {
            // create new user and set username, full name, email
            var user = new ApplicationUser()
            {
                UserName = userName,
                FullName = fullName,
                Email = email
            };

            // call user manager to hash the password and store the user in the database
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            // no need to link role to customer
            if (role == "Customer") return;

            // link role to user
            var addAdminRoleResult = userManager.AddToRole(user.Id, role);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateSeveralBooks(ApplicationDbContext context)
        {
            context.Books.Add(new Book()
            {
                Name = "Projects in Computing and Information Systems",
                Author = "Christian W. Dawson",
                Description = "Undertaking an academic project is a key feature of most of today's computing and information systems degree programmes. Simply put, this book provides the reader with everything they will need to successfully complete their computing project.",
                Category = new Category()
                {
                    Name = "Reference Book",
                    Description = "A book intended to be consulted for information on specific matters rather than read from beginning to end.",
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                },

                CoverUrl = "https://cdn.discordapp.com/attachments/1045548142558445608/1053167446367932456/s-l500.png",
                Price = 4,
                StockedQuantity= 3200,
                CreatedDateTime = new DateTime(2015, 12, 6),
                UpdatedDateTime = new DateTime(2015, 12, 6)
            });

            context.Books.Add(new Book()
            {
                Name = "Jikyu 300 en no shinigami",
                Author = "Fujimaru",
                Description = "One day, high school student Sakura Shinji is invited by her classmate Hanamori Yuki to work as a \"God of Death\", a job that helps the dead who have not yet escaped, still entangled with their earthly life. all the nostalgia and send them to the other world.",
                Category = new Category()
                {
                    Name = "Light Novel",
                    Description = "A style of young adult novel primarily targeting high school and middle school students.",
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now
                },

                CoverUrl = "https://cdn.discordapp.com/attachments/1045548142558445608/1053169658552922206/fetch.png",
                Price = 4.67M,
                StockedQuantity = 2100,
                CreatedDateTime = new DateTime(2019, 6, 26),
                UpdatedDateTime = new DateTime(2019, 6, 26)
            });
        }

    }
}
