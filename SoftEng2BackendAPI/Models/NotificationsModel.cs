using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Models
{
    public class NotificationsModel
    {
        public int Notifications_ID { get; set; }
        public int User_ID { get; set; }
        public UserModel UserModel { get; set; }
        public string Notification { get; set; }
        public string Is_Seen { get; set; }
        public string Notification_Type { get; set; }        
    }
}
