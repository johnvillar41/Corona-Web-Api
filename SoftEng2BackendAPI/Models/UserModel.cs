using System;
using System.Drawing;

namespace SoftEng2BackendAPI
{
    public class UserModel
    {
        public int User_ID { get; set; }
        public string User_Username { get; set; }
        public string User_Password { get; set; }
        public Image Profile_Picture { get; set; }

        public enum User_Status { ACTIVE, INACTIVE }
    }
}
