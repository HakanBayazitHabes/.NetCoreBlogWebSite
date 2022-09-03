using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMesssageNotification :ViewComponent
    {
        Message2Manager messega2Manager = new Message2Manager(new EfMessage2Repository());
        private readonly UserManager<AppUser> _userManager;

        public WriterMesssageNotification(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;
            var values = messega2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }
    }
}
