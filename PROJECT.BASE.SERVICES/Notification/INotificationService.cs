using PROJECT.BASE.ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT.BASE.SERVICES
{
    public interface INotificationService
    {
        void SendNotification(string urlConnect, MessegeQueue data);
        void SendSMS(MessegeQueue data);
        void SendSlackMessage(MessegeQueue data);
        void SendEmail(MessegeQueue data);
        void SendFireBase(MessegeQueue data);
        string LogFileReplace(string pathFile);
        void WriteLogFileReplace(string pathFile);
    }
}
