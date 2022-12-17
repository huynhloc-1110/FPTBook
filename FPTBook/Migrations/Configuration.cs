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
            // add categories
            context.Categories.Add(new Category()
            {
                Name = "Reference Book",
                Description = "A book intended to be consulted for information on specific matters rather than read from beginning to end.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.Categories.Add(new Category()
            {
                Name = "Light Novel",
                Description = "A style of young adult novel primarily targeting high school and middle school students.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.Categories.Add(new Category()
            {
                Name = "Textbook",
                Description = "A book that teaches a particular subject and that is used especially in schools and colleges.",
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            });

            context.SaveChanges();

            // add books
            context.Books.Add(new Book()
            {
                Name = "Projects in Computing and Information Systems",
                Author = "Christian W. Dawson",
                Description = "Undertaking an academic project is a key feature of most of today's computing and information systems degree programmes. Simply put, this book provides the reader with everything they will need to successfully complete their computing project.",
                Category = context.Categories.First(c => c.Name == "Reference Book"),
                CoverUrl = "book1.jpg",
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
                Category = context.Categories.First(c => c.Name == "Light Novel"),
                CoverUrl = "book2.jpg",
                Price = 4.67M,
                StockedQuantity = 2100,
                CreatedDateTime = new DateTime(2019, 6, 26),
                UpdatedDateTime = new DateTime(2019, 6, 26)
            });

            context.Books.Add(new Book()
            {
                Name = "Math Analysis 12",
                Author = "Ministry of Education and Training",
                Description = "The book is used for Teachers and students study at high schools and educational institutions across the country with the basic math knowledge that every 12th grader should have. The book also helps readers look up the standard knowledge of Math Analysis 12.",
                Category = context.Categories.First(c => c.Name == "Textbook"),
                CoverUrl = "book3.jpg",
                Price = 0.42M,
                StockedQuantity = 2900,
                CreatedDateTime = new DateTime(2022, 2, 16),
                UpdatedDateTime = new DateTime(2022, 2, 16)
            });

            context.Books.Add(new Book()
            {
                Name = "Computer Networking: A Top-Down Approach",
                Author = "James Kurose",
                Description = "Focusing on the Internet and the fundamentally important issues of networking, this book provides an excellent foundation for readers interested in computer science and electrical engineering, without requiring extensive knowledge of programming or mathematics.",
                Category = context.Categories.First(c => c.Name == "Reference Book"),
                CoverUrl = "book4.jpg",
                Price = 53.35M,
                StockedQuantity = 800,
                CreatedDateTime = new DateTime(2016, 4, 26),
                UpdatedDateTime = new DateTime(2016, 4, 26)
            });

            context.Books.Add(new Book()
            {
                Name = "5 Centimeters per Second",
                Author = "Makoto Shinkai",
                Description = "Love can move at the speed of terminal velocity, but as award-winning director Makoto Shinkai reveals in his latest comic, it can only be shared and embraced by those who refuse to see it stop.",
                Category = context.Categories.First(c => c.Name == "Light Novel"),
                CoverUrl = "book5.jpg",
                Price = 11.99M,
                StockedQuantity = 1200,
                CreatedDateTime = new DateTime(2012, 6, 26),
                UpdatedDateTime = new DateTime(2012, 6, 26)
            });
        }

    }
}
