using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Models;
using SoftEng2BackendAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.RepoImplementation
{
    public class NotificationRepository : INotificationRepository
    {
        public async Task<IEnumerable<NotificationsModel>> FetchAllNotificationsAsync()
        {
            List<NotificationsModel> notificationlist = new List<NotificationsModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM Notifications_Table";
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader =await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        NotificationsModel model = new NotificationsModel
                        {
                            Notifications_ID = int.Parse(reader["notif_id"].ToString()),
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            UserModel = await FetchUser(int.Parse(reader["user_id"].ToString())),
                            Notification = reader["notification"].ToString(),
                            Is_Seen = reader["is_seen"].ToString(),
                            Notification_Type = reader["notification_type"].ToString()
                        };
                        notificationlist.Add(model);
                    }
                }
            }
            return notificationlist;
        }
        private async Task<UserModel>FetchUser(int id)
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM User_Table WHERE user_id=@user_id";
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user_id", id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        user = new UserModel
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            User_Username = reader["user_username"].ToString(),
                            User_Password = reader["user_password"].ToString(),
                            StringProfilePic = reader["profile_picture"].ToString(),
                            User_Status = reader["user_status"].ToString()
                        };                       
                    }
                }
            }
            return user;
        }
    }
}
