using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogMVC.Models;
using BlogMVC.DataAccess;


namespace BlogMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private PostRepository postRepo = new PostRepository();

        public ActionResult Index()
        {
            var results = postRepo.All.OrderByDescending(p => p.CreateDate).Take(10).ToList();
            return View(results);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}