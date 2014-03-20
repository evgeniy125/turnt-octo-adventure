using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMVC.DataAccess;
using BlogMVC.Domain;

namespace BlogMVC.Models
{
    public class MyRoleManager
    {
        static BlogDbContext context = new BlogDbContext();
        public static string GetRoleForUser(string userid)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            return userManager.GetRoles(userid).Single();
        }
    }
}