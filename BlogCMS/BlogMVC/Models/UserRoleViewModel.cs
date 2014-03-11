using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMVC.Models
{
    public class UserRoleViewModel
    {
        private List<IdentityRole> roles;

        public UserRoleViewModel() { }

        public UserRoleViewModel(List<IdentityRole> roles)
        {
            this.roles = roles;
        }
        public string SelectedRoleId { get; set; }

        public IEnumerable<SelectListItem> Roles
        {
            get { return new SelectList(roles, "Id", "Name"); }
        }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}