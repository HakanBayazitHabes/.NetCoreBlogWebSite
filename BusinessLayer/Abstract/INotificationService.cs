using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface INotificationService
    {
        void NotificationAdd(Notification notification);
        void NotificationDelete(Notification notification);
        void NotificationUpdate(Notification notification);
        List<Notification> GetList();
        Notification GetById(int id);
    }
}
