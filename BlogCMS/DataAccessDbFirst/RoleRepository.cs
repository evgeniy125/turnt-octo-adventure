using DataAccessDbFirst;
using GenericRepository.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.DataAccessDbFirst
{
    public class RoleRepository : Repository<BlogMVCEntities, IdentityRole>
    {
    }
}
