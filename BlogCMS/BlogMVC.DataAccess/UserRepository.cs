using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.EF;

using BlogMVC.Domain;


namespace BlogMVC.DataAccess
{
    public class UserRepository : Repository<BlogDbContext, User>
    {
        public void EditUser(User user)
        {
            var entity = Context.Users.Find(user.Id);
            Context.Entry(entity).CurrentValues.SetValues(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns users with their roles</returns>
        public List<UserRoleViewModel> GetUserList()
        {
            var results = from user in Context.Users
                          from role in Context.Roles
                          where user.Roles.FirstOrDefault().RoleId == role.Id
                          select new UserRoleViewModel
                          {
                              UserName = user.UserName,
                              CreateDate = user.CreateDate,
                              UserId = user.Id,
                              RoleId = user.Roles.FirstOrDefault().RoleId,
                              RoleName = role.Name
                          };
            return results.ToList();
        }
    }
}
