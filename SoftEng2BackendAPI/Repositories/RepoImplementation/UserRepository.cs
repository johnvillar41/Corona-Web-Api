using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.IO;
using SoftEng2BackendAPI.ApikeyAttribute;
using System.Text;
using System.Data;

namespace SoftEng2BackendAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        ///     This function will fetch a specific user given an id
        /// </summary>
        /// <returns>
        ///     This will return a user Object model with the given student id
        /// </returns>
        public async Task<UserModel> FetchSpecificUserAsync(int user_id)
        {
            UserModel user = null;
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table WHERE user_id=@userID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userID", user_id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {                      
                        user = new UserModel(
                            int.Parse(reader["user_id"].ToString()),
                            reader["user_username"].ToString(),
                            reader["user_password"].ToString(),
                            reader["profile_picture"].ToString(),
                            reader["user_status"].ToString()
                            );
                    }
                }
            }
            return user;
        }
        /// <summary>
        ///     This will fetch all the user from db
        /// </summary>
        /// <returns>
        ///     Return a list of all the students
        /// </returns>
        public async Task<IEnumerable<UserModel>> FetchUsersAsync()
        {
            List<UserModel> userList = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM User_Table";
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {                       
                        UserModel user = new UserModel(
                            int.Parse(reader["user_id"].ToString()),
                            reader["user_username"].ToString(),
                            reader["user_password"].ToString(),
                            reader["profile_picture"].ToString(),
                            reader["user_status"].ToString()
                            );
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }
        /// <summary>
        ///     This will check whether the user is authenticated to log in
        /// </summary>      
        /// <returns>
        ///     Will return a boolean type for whether the student is in the database or not
        /// </returns>
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
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
        /// <summary>
        ///     This will insert a new verified user in the database
        ///     By: 1)Passing a new user 
        ///         2)The newUser.StringProfilePic will be sent to the database as a string value
        /// </summary>
        /// <param name="newUser">
        ///     Will pass a new UserModel as a parameter    
        /// </param>        
        public async Task RegisterNewUserAsync(UserModel newUser)
        {
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "INSERT INTO User_Table(user_username,user_password,profile_picture,user_status)" +
                    "VALUES(@username,@password,@picture,@status)";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", newUser.User_Username);
                command.Parameters.AddWithValue("@password", newUser.User_Password);              
                command.Parameters.AddWithValue("@picture", newUser.StringProfilePic);
                command.Parameters.AddWithValue("@status", newUser.User_Status);

                await command.ExecuteNonQueryAsync();
            }
        }       
    }
}
