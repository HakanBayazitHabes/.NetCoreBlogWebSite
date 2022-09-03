using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class Message2Manager : IMessage2Service
    {
        IMessage2Dal _message2Dal;

        public Message2Manager(IMessage2Dal message2Dal)
        {
            _message2Dal = message2Dal;
        }

        public Message2 GetById(int id)
        {
            return _message2Dal.GetByID(id);
        }

        public List<Message2> GetInboxListByWriter(int id)
        {
            return _message2Dal.GetInboxWithMessageByWriter(id);
        }

        public List<Message2> GetList()
        {
            return _message2Dal.GetListAll();
        }

        public List<Message2> GetSendBoxListByWriter(int id)
        {
            return _message2Dal.GetSendWithMessageByWriter(id);
        }

        public void MessageAdd(Message2 message)
        {
            _message2Dal.Insert(message);
        }

        public void MessageDelete(Message2 message)
        {
            _message2Dal.Delete(message);
        }

        public void MessageUpdate(Message2 message)
        {
            _message2Dal.Update(message);
        }
    }
}
