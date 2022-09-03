using BusinessLayer.Concreate;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class DashBoardController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IActionResult Index()
        {
            using
            Context context = new Context();
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y =>
                y.Email).FirstOrDefault();
            var writerid = context.Writers.Where(x => x.WriterMail == usermail).Select(y =>
                    y.WriterID).FirstOrDefault();


            ViewBag.v1 = context.Blogs.Count().ToString();
            ViewBag.v2 = context.Blogs.Where(x => x.WriterID == writerid    ).Count();
            ViewBag.v3 = context.Categories.Count();
            return View();
        }
    }
}
