using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{

    public class MessageController : Controller
    {
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        private readonly UserManager<AppUser> _userManager;

        public MessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> InBox()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;
            var values = message2Manager.GetInboxListByWriter(writerID);
            return View(values);
        }

        public async Task<IActionResult> SendBox()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;
            var values = message2Manager.GetSendBoxListByWriter(writerID);
            return View(values);
        }
        public IActionResult MessageDetails(int id)
        {
            var value = message2Manager.GetById(id);
            return View(value);
        }
        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(Message2 p)
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            var writerID = value.Id;
            p.SenderID = writerID;
            var receiverMail = p.ReceiverMail;
            var receiverId = writerManager.GetWriterByMail(receiverMail);
            if (receiverId != 0)
            {
                p.ReceiverID = receiverId;
                p.MessageStatus = true;
                p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                message2Manager.MessageAdd(p);
            }
            return RedirectToAction("InBox");

        }
    }
}
