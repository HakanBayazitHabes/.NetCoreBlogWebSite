using BusinessLayer.Concreate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        Context context = new Context();
        private readonly UserManager<AppUser> _userManager;

        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListCategory();
            return View(values);
        }
        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = blogManager.GetBlogByID(id);
            return View(values);
        }
        public async Task<IActionResult> BlogListByWriter()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;
            var values = blogManager.GetListWithCategoryByWriterBm(writerID);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryvalues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BlogAdd(Blog blog)
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;

            BlogValidator blogValidator = new BlogValidator();
            ValidationResult results = blogValidator.Validate(blog);
            if (results.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterID =writerID;
                blogManager.BlogAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = blogManager.GetById(id);
            blogManager.BlogDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = blogManager.GetById(id);
            List<SelectListItem> categoryvalues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }
        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog blog)
        {
            var writerID = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            blog.WriterID = writerID;
            var blogValue_ = blogManager.GetById(blog.BlogID);
            blog.BlogCreateDate = DateTime.Parse(blogValue_.BlogCreateDate.ToShortDateString());
            blog.BlogStatus = true;
            blogManager.BlogUpdate(blog);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
