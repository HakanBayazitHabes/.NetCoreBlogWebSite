using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.Repositories;
using EntityLayer.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessage2Repository : GenericRepository<Message2>, IMessage2Dal
    {
        public List<Message2> GetInboxWithMessageByWriter(int id)
        {
            using (var context = new Context())
                return context.Message2s.OrderByDescending(x=>x.MessageID).Include(x => x.SenderUser).Where(x => x.ReceiverID == id).ToList();
        }

        public List<Message2> GetSendWithMessageByWriter(int id)
        {
            using (var context = new Context())
                return context.Message2s.OrderByDescending(x => x.MessageID).Include(x => x.RecevierUser).Where(x => x.SenderID == id).ToList();
        }
    }
}
