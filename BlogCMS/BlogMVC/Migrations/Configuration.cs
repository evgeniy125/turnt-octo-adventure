namespace BlogMVC.Migrations
{
    using BlogMVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogMVC.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "evgeniy"))
            {
                var user = new User
                {
                    UserName = "evgeniy",
                    Posts = new List<Post> {
                new Post {Title = "Hello", Content = "Hello world", CreateDate = DateTime.Now}}
                };
                var user2 = new User { UserName = "stepan" };

                userManager.Create(user, "password");
                userManager.Create(user2, "password");
                roleManager.Create(new IdentityRole { Name = "admin" });
                roleManager.Create(new IdentityRole { Name = "writer" });
                roleManager.Create(new IdentityRole { Name = "reader" });
                userManager.AddToRole(user.Id, "admin");
                userManager.AddToRole(user2.Id, "writer");
            }

            for (int i = 1; i < 100; i++)
            {
                var user = new User
                {
                    UserName = "user" + i.ToString(),
                    Posts = new List<Post> {
                new Post {Title = "Hello", Content = "Hello world", CreateDate = DateTime.Now}}
                };
                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "reader");
            }
        }
    }
}
