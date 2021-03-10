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

        //For Fetching and inserting data from db
        public UserModel(int user_ID, string user_Username, string user_Password, string profileString, string status)
        {
            User_ID = user_ID;
            User_Username = user_Username;
            User_Password = user_Password;
            StringProfilePic = profileString;
            User_Status = status;
        }     
     
        public UserModel()
        {
        }
    }
}
