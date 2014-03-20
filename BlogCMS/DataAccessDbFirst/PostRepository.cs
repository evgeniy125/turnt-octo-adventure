using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.EF;
using System.Data.Entity;
using BlogMVC.Domain;
using DataAccessDbFirst;

namespace BlogMVC.DataAccessDbFirst
{
    public class PostRepository : Repository<BlogMVCEntities, Post>
    {
        public void EditPost(Post post)
        {
            var entity = Context.Posts.Find(post.PostId);
            Context.Entry(entity).CurrentValues.SetValues(post);
        }
    }
}
