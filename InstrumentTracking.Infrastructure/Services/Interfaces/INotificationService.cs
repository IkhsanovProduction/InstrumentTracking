using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentsTracking.Services
{
    public interface INotificationService
    {
        public void SendNonLinkedNotification(string equipment);
        public void SendNotificationBind(string equipment, string engineer);
        public void SentNotificationUnbind(string equipment, string engineer);
        public void SendNotificationSamplingAct(string wellNumber, string engineer);
        public void SentNotificationChangeWellNumber(string engineer, string equipment, string wellNumberOld, string wellNumberCurrent);
    }
}
