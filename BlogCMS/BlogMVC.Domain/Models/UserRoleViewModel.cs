using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogMVC.Models
{
    public class UserRoleViewModel
    {
        public List<IdentityRole> Roles { get; set; }

        public string SelectedRoleId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}