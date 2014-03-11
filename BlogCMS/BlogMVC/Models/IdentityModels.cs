using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace BlogMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public override string Id { get { return base.Id; } }

        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(80)]
        public string Bio { get; set; }
        [Column(TypeName="date")]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Post> Posts { get; set; }
    }

        public class Post
        {
            public int PostId { get; set; }
            [MaxLength(40)]
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime CreateDate { get; set; }
            [Required]
            [ForeignKey("UserId")]
            public virtual User User { get; set; }
            public string UserId { get; set; }
        }

        public class BlogInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
            //    new List<User>{
            //    new User {Name="pluralsight", UserName="PluralSight", CreateDate = new DateTime(2011,10,10),
            //        Posts = new List<Post> { new Post { Content = "Having fun with data annotations", Title = "Data Annotations",
            //    CreateDate = new DateTime(2011,8,9)}} },
            //    new User {Name="giantpuppy", UserName="Sampson the Newfie", CreateDate = new DateTime(2011,7,9),
            //     }
            //}.ForEach(b => context.Users.Add(b));

            //    context.Roles.Add(new IdentityRole { Name = "admins" });
            //    base.Seed(context);
            }
        }
}