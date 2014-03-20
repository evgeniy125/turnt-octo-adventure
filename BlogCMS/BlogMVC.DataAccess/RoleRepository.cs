using GenericRepository.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.DataAccess
{
    public class RoleRepository : Repository<BlogDbContext, IdentityRole>
    {
    }
}
