using System;
using System.Drawing;

namespace SoftEng2BackendAPI
{
    public class UserModel
    {
        public int User_ID { get; set; }
        public string User_Username { get; set; }
        public string User_Password { get; set; }       
        public string StringProfilePic { get; set; }
        public string User_Status { get; set; }       
    }
}
