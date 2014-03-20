using BlogMVC.Domain;
using DataAccessDbFirst;
using GenericRepository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.DataAccessDbFirst
{
    public class UserRepository : Repository<BlogMVCEntities, User>
    {
        public void EditUser(User user)
        {
            var entity = Context.AspNetUsers.Find(user.Id);
            Context.Entry(entity).CurrentValues.SetValues(user);
        }
    }
}
