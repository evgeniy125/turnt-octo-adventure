using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using BlogMVC.Domain;

namespace BlogMVC.DataAccess
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        public BlogDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}
