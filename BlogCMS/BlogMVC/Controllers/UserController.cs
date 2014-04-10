using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PagedList;

using BlogMVC.Models;
using BlogMVC.Domain;
using BlogMVC.DataAccess;


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

        public ActionResult Users()
        {
            return View(Json(userRepo.GetUserList(), JsonRequestBehavior.AllowGet));
        }

        public JsonResult GetUsers()
        {
            return Json(userRepo.GetUserList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoles()
        {
            return Json(roleRepo.All, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(UserRoleViewModel viewModel)
        {
            var user = userRepo.Find(viewModel.UserId);

            if (user.Roles.Count != 1)
                return null;
            var role = roleRepo.FindBy(r => r.Id == viewModel.RoleId).Single();
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
