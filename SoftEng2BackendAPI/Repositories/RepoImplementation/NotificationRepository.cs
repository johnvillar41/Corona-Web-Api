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
        /// <summary>
        ///     This function will fetch all the notifications on the database
        /// </summary>
        /// <returns>
        ///     Will return a list of Notifications model
        /// </returns>
        public async Task<IEnumerable<NotificationsModel>> FetchAllNotificationsAsync()
        {
            List<NotificationsModel> notificationlist = new List<NotificationsModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                string queryString = "SELECT Notifications_Table.notif_id,Notifications_Table.user_id,User_Table.user_id,User_Table.user_username,User_Table.profile_picture,User_Table.user_status,Notifications_Table.notification,Notifications_Table.is_seen,Notifications_Table.notification_type FROM Notifications_Table INNER JOIN User_Table ON Notifications_Table.user_id = User_Table.user_id";
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader =await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        UserModel userModel = new UserModel
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            User_Username = reader["user_username"].ToString(),
                            StringProfilePic = reader["profile_picture"].ToString(),
                            User_Status = reader["user_status"].ToString()
                        };
                        NotificationsModel model = new NotificationsModel
                        {
                            Notifications_ID = int.Parse(reader["notif_id"].ToString()),
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            UserModel = userModel,
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
    }
}
