using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstrumentsTracking.Services
{
    public class TelegramNotification : INotificationService
    {
        private const string API = "********************";

        public void SendNotificationBind(string equipment, string engineer)
        {
            using(var client = new HttpClient())
            {
                var endpoint = new Uri(API + "Привязано оборудование: " + equipment +
                                                                             "----" +
                                                                        "Инженер: " +
                                                                            engineer);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        public void SendNonLinkedNotification(string equipment)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(API + "Оборудование не привязано!!!: " + equipment);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        public void SendNotificationSamplingAct(string wellNumber, string engineer)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(API + "Акт отбора проб - номер скважины: " +
                                                                       wellNumber +
                                                                 "-----Инженер: " +
                                                                          engineer);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        public void SentNotificationChangeWellNumber(string engineer, string equipment, string wellNumberOld, string wellNumberCurrent)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(API + "Смена скважины - Инженер: " +
                                                               engineer +
                                                        "-----оборудование: " +
                                                                 equipment +
                                                                 "-----из скважины: " +
                                                                 wellNumberOld +
                                                        "-----в скважину: " +
                                                                 wellNumberCurrent);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        public void SentNotificationUnbind(string equipment, string engineer)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(API + "Отвязано - оборудование: " +
                                                               equipment +
                                                        "-----Инженер: " +
                                                                 engineer);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
