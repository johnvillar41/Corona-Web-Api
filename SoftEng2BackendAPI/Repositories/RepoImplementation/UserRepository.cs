using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.IO;

namespace SoftEng2BackendAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoronaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public async Task<UserModel> FetchSpecificUser(int user_id)
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table WHERE user_id=@userID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userID", user_id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    UserModel.User_Status status;
                    if (reader["user_status"].Equals("Active"))
                    {
                        status = UserModel.User_Status.ACTIVE;
                    }
                    else
                    {
                        status = UserModel.User_Status.INACTIVE;
                    }

                    byte[] myImageByteArrayData = (byte[])reader["profile_picture"];
                    string myImageBase64StringData = Convert.ToBase64String(myImageByteArrayData);
                    user = new UserModel(
                        int.Parse(reader["user_id"].ToString()),
                        reader["user_username"].ToString(),
                        reader["user_password"].ToString(),
                        myImageBase64StringData,
                        status
                        );
                }
            }
            return user;
        }

        public async Task<IEnumerable<UserModel>> FetchUsers()
        {
            List<UserModel> userList = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {

                    UserModel.User_Status status;
                    if (reader["user_status"].Equals("Active"))
                    {
                        status = UserModel.User_Status.ACTIVE;
                    }
                    else
                    {
                        status = UserModel.User_Status.INACTIVE;
                    }

                    byte[] myImageByteArrayData = (byte[])reader["profile_picture"];
                    string myImageBase64StringData = Convert.ToBase64String(myImageByteArrayData);
                    UserModel user = new UserModel(
                        int.Parse(reader["user_id"].ToString()),
                        reader["user_username"].ToString(),
                        reader["user_password"].ToString(),
                        myImageBase64StringData,
                        status
                        );
                    userList.Add(user);
                }
            }
            return userList;
        }

        public async Task<bool> LoginUser(string username, string password)
        {
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
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
