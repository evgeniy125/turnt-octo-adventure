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
using BlogMVC.Domain;
using BlogMVC.DataAccess;
//using BlogMVC.DataAccessDbFirst;

namespace BlogMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserRepository userRepo = new UserRepository();
        private RoleRepository roleRepo = new RoleRepository();

        public ActionResult Index(int page = 1)
        {
            return View(userRepo.All.OrderBy(u => u.UserName).ToPagedList(page, 5));
        }

        [Authorize(Roles="admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userRepo.Find(id);

            var model = new UserRoleViewModel() { UserId = user.Id, UserName = user.UserName,
                SelectedRoleId = user.Roles.Single().Role.Id, Roles = roleRepo.All.ToList() };

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
            var user = userRepo.Find(viewModel.UserId);

            if (user.Roles.Count != 1)
                return null;
            var role = roleRepo.FindBy(r => r.Id == viewModel.SelectedRoleId).Single();
            user.Roles.Clear();
            user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = role.Id });

            if (ModelState.IsValid)
            {
                userRepo.EditUser(user);
                userRepo.Save();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userRepo.Dispose();
                roleRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
