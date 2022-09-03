using BusinessLayer.Concreate;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterAboutOnDashboard :ViewComponent
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail ==
              usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = writerManager.GetWriterById(writerID);
            return View(values);
        }

    }
}
