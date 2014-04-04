using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GenericRepository.EF;

using BlogMVC.Models;
using BlogMVC.DataAccess;
using BlogMVC.Domain;


namespace BlogMVC.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private PostRepository postRepo = new PostRepository();
        private UserRepository userRepo = new UserRepository();
        public ActionResult Index([Bind(Prefix = "id")] string userId)
        {
            var user = userRepo.Find(userId);
            if (user != null)
                return View(user);
            return HttpNotFound();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postRepo.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Authorize(Roles = "admin,writer")]
        public ActionResult Create(string userId)
        {
            if (User.Identity.GetUserId().ToString() != userId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,writer")]
        public ActionResult Create(Post post)
        {
            if (User.Identity.GetUserId().ToString() != post.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                postRepo.Add(post);
                postRepo.Save();
                return RedirectToAction("Index", new { id = post.UserId });
            }

            return View(post);
        }

        [Authorize(Roles="admin,writer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postRepo.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("writer") && post.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,writer")]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("writer") && post.UserId != User.Identity.GetUserId())
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                postRepo.EditPost(post);
                postRepo.Save();
                return RedirectToAction("Index", new {id = post.UserId});
            }
            return View(post);
        }

        [Authorize(Roles = "admin,writer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postRepo.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("writer") && post.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(post);
        }


        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin,writer")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postRepo.Find(id);
            if (User.IsInRole("writer") && post.UserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = post.UserId;
            postRepo.Delete(post);
            postRepo.Save();
            return RedirectToAction("Index", new { id = userId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                postRepo.Dispose();
                userRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
