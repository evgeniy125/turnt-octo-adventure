using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using BlogMVC.Domain;

namespace BlogMVC.DataAccess
{
    public class PostRepository : Repository<BlogDbContext, Post>
    {
        public void EditPost(Post post)
        {
            var entity = Context.Posts.Find(post.PostId);
            Context.Entry(entity).CurrentValues.SetValues(post);
        }

        public void UpdatePost(Post post, int id)
        {
            var entity = Context.Posts.Find(id);
            Context.Entry(entity).CurrentValues.SetValues(post);
        }
    }
}
