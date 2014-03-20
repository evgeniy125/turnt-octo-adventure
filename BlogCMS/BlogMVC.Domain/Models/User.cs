using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlogMVC.Domain
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
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
