using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessage2Service
    {
        void MessageAdd(Message2 message);
        void MessageDelete(Message2 message);
        void MessageUpdate(Message2 message);
        List<Message2> GetList();
        Message2 GetById(int id);
        List<Message2> GetInboxListByWriter(int id);
        List<Message2> GetSendBoxListByWriter(int id);
    }
}
