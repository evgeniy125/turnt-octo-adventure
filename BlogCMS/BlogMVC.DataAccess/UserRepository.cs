using BlogMVC.Domain;
using BlogMVC.Domain.Models;
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

        public List<UserRoleModel> SelectForUserRoleModel()
        {
            var results = from user in Context.Users
                          from role in Context.Roles
                          where user.Roles.FirstOrDefault().RoleId == role.Id
                          select new UserRoleModel
                          {
                              UserName = user.UserName,
                              CreateDate = user.CreateDate,
                              UserId = user.Id,
                              Id = user.Roles.FirstOrDefault().RoleId,
                              RoleName = role.Name
                          };
            return results.ToList();
        }
    }
}
