using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Domain.Models
{
    public class UserRoleModel
    {
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
