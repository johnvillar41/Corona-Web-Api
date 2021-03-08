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

        private User_Status status;
        public enum User_Status { ACTIVE, INACTIVE }
        public UserModel(int user_ID, string user_Username, string user_Password, Image profile_Picture, User_Status status)
        {
            User_ID = user_ID;
            User_Username = user_Username;
            User_Password = user_Password;
            Profile_Picture = profile_Picture;
            this.status = status;
        }

        public UserModel()
        {
        }
    }
}
