using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using PagedList;

namespace BlogMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int page = 1)
        {
            return View(db.Users.OrderBy(u => u.UserName).ToPagedList(page, 5));
        }

        [Authorize(Roles="admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            var model = new UserRoleViewModel(db.Roles.ToList()) { UserId = user.Id, UserName = user.UserName,
                SelectedRoleId = user.Roles.Single().Role.Id };

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(UserRoleViewModel viewModel)
        {
            var user = db.Users.Find(viewModel.UserId);

            if (user.Roles.Count != 1)
                return null;
            var role = db.Roles.Where(r => r.Id == viewModel.SelectedRoleId).Single();
            user.Roles.Clear();
            user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = role.Id });

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
