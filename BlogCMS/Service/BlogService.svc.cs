using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using GenericRepository.EF;

using BlogMVC.DataAccess;
using BlogMVC.Domain;

namespace Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BlogService
    {
        private PostRepository _postRepo = new PostRepository();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "posts")]
        public HttpResponse<Post> AddPost(Post post)
        {
            _postRepo.Add(post);
            _postRepo.Save();
            return new HttpResponse<Post> { Success = true, Data = post };
        }

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "posts/{id}", ResponseFormat = WebMessageFormat.Json)]
        public HttpResponse<string> DeletePost(string id)
        {
            var post = _postRepo.Find(int.Parse(id));
            if (post == null)
            {
                return new HttpResponse<string> { Success = false, ErrorMessage = "No such post exists." };
            }
            _postRepo.Delete(post);
            _postRepo.Save();
            return new HttpResponse<string> { Success = true };
        }

        [OperationContract]
        [WebGet(UriTemplate = "posts/{id}", ResponseFormat = WebMessageFormat.Json)]
        public HttpResponse<Post> GetPost(string id)
        {
            int postId = int.Parse(id);
            var post = _postRepo.Find(postId);
            if (post == null)
            {
                return new HttpResponse<Post> { Success = false, ErrorMessage = "No such post exists." };
            }
            return new HttpResponse<Post> { Success = true, Data = post };
        }


        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "posts/{id}")]
        public HttpResponse<Post> UpdatePost(Post post, string id)
        {
            int postId = int.Parse(id);
            if (post.PostId != postId)
            {
                return new HttpResponse<Post> { Success = false, ErrorMessage = "No such post exists." };
            }
            if (_postRepo.Find(postId) == null)
            {
                _postRepo.Add(post);
            }
            else
            {
                _postRepo.EditPost(post);
            }
            _postRepo.Save();
            return new HttpResponse<Post> { Success = true };
        }

        [OperationContract]
        [WebGet(UriTemplate = "posts", ResponseFormat = WebMessageFormat.Json)]
        public HttpResponse<List<Post>> GetPostList()
        {
            return new HttpResponse<List<Post>> { Success = true, Data = _postRepo.All.ToList() };
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "posts/{id}/Clone",
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public HttpResponse<Post> ClonePost(string id)
        {
            var post = _postRepo.Find(int.Parse(id));
            if (post == null)
            {
                return new HttpResponse<Post> { Success = false, ErrorMessage = "No such post exists." };
            }
            _postRepo.Add(post);
            _postRepo.Save();
            return new HttpResponse<Post> { Success = true, Data = post };
        }


        // Add more operations here and mark them with [OperationContract]
    }
}
