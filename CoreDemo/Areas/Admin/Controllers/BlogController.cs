using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccessLayer.Concreate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";


                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CalismaStatic.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> blogModels = new List<BlogModel>
            {
                new BlogModel{ID=1,BlogName="C# Programlamaya Giriş"},
                new BlogModel{ID=2,BlogName="Tesla Firmasının Araçları"},
                new BlogModel{ID=3,BlogName="2020 olimpiyatları"}
            };
            return blogModels;
        }
        public IActionResult BlogListExcel()
        {
            return View();
        }

        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";


                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CalismaDynamic.xlsx");
                }
            }

        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> blogModel2s = new List<BlogModel2>();
            using (var context = new Context())
            {
                blogModel2s = context.Blogs.Select(x => new BlogModel2
                {
                    ID=x.BlogID,
                    BlogName=x.BlogTitle
                }).ToList();
            }
            return blogModel2s;
        }
        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
