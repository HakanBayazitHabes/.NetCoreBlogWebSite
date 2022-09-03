using BusinessLayer.Concreate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        WriterManager writerManager = new WriterManager(new EfWriterRepository());

        private readonly UserManager<AppUser> _userManager;

        public AdminMessageController(UserManager<AppUser> userManager)
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

        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ComposeMessage(Message2 p)
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
            return RedirectToAction("SendBox");

        }
        
        public IActionResult MessageDetails(int id)
        {
            var value = message2Manager.GetById(id);
            value.MessageStatus = true;
            message2Manager.MessageUpdate(value);
            return View(value);
        }
        public IActionResult UpdateMessage(Message2 message2)
        {
            var value = message2Manager.GetById(message2.MessageID);
            value.MessageStatus = true;
            message2Manager.MessageUpdate(value);
            var jsonMessage = JsonConvert.SerializeObject(message2);
            return Json(jsonMessage);
        }

        //public IActionResult DeleteMessage(int id)
        //{
        //    var value = message2Manager.GetById(id);
        //    message2Manager.MessageDelete(value);
        //    return Json(value);
        //}
    }
}
