using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.IO;
using SoftEng2BackendAPI.ApikeyAttribute;

namespace SoftEng2BackendAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        public async Task<UserModel> FetchSpecificUserAsync(int user_id)
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table WHERE user_id=@userID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userID", user_id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {                
                    byte[] myImageByteArrayData = (byte[])reader["profile_picture"];
                    string myImageBase64StringData = Convert.ToBase64String(myImageByteArrayData);
                    user = new UserModel(
                        int.Parse(reader["user_id"].ToString()),
                        reader["user_username"].ToString(),
                        reader["user_password"].ToString(),
                        myImageBase64StringData,
                        reader["user_status"].ToString()
                        );
                }
            }
            return user;
        }

        public async Task<IEnumerable<UserModel>> FetchUsersAsync()
        {
            List<UserModel> userList = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {                 
                    byte[] myImageByteArrayData = (byte[])reader["profile_picture"];
                    string myImageBase64StringData = Convert.ToBase64String(myImageByteArrayData);
                    UserModel user = new UserModel(
                        int.Parse(reader["user_id"].ToString()),
                        reader["user_username"].ToString(),
                        reader["user_password"].ToString(),
                        myImageBase64StringData,
                        reader["user_status"].ToString()
                        );
                    userList.Add(user);
                }
            }
            return userList;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table WHERE user_username=@username AND user_password=@password";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    isValid = true;
                }
            }
            return isValid;
        }
    }
}
