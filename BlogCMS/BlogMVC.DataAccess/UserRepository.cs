using BlogMVC.Domain;
using GenericRepository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.DataAccess
{
    public class UserRepository : Repository<BlogDbContext, User>
    {
        public void EditUser(User user)
        {
            var entity = Context.Users.Find(user.Id);
            Context.Entry(entity).CurrentValues.SetValues(user);
        }
    }
}
