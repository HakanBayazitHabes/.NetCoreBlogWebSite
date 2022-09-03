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
    public class NotificationManager : INotificationService
    {
        INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public Notification GetById(int id)
        {
            return _notificationDal.GetByID(id);
        }

        public List<Notification> GetList()
        {
            return _notificationDal.GetListAll();
        }

        public void NotificationAdd(Notification notification)
        {
            _notificationDal.Insert(notification);
        }

        public void NotificationDelete(Notification notification)
        {
            _notificationDal.Delete(notification);
        }

        public void NotificationUpdate(Notification notification)
        {
            _notificationDal.Update(notification);
        }
    }
}
