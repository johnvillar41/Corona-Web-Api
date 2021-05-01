using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoftEng2BackendAPI.ApikeyAttribute;

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
                await connection.OpenAsync();
                string queryString = "SELECT * FROM User_Table WHERE user_id=@userID";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userID", user_id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        user = new UserModel
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            User_Username = reader["user_username"].ToString(),
                            User_Password = reader["user_password"].ToString(),
                            StringProfilePic = reader["profile_picture"].ToString(),
                            User_Status = reader["user_status"].ToString(),
                            Health_Status = reader["health_status"].ToString()
                        };                       
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
                await connection.OpenAsync();
                string queryString = "SELECT * FROM User_Table";
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        UserModel user = new UserModel
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            User_Username = reader["user_username"].ToString(),
                            User_Password = reader["user_password"].ToString(),
                            StringProfilePic = reader["profile_picture"].ToString(),
                            User_Status = reader["user_status"].ToString(),
                            //Health_Status = reader["health_status"].ToString()
                        };
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
                await connection.OpenAsync();
                string queryString = "SELECT * FROM User_Table WHERE user_username=@username AND user_password=@password";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
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
                await connection.OpenAsync();
                string queryString = "INSERT INTO User_Table(user_username,user_password,profile_picture,user_status,health_status)" +
                    "VALUES(@username,@password,@picture,@status,@healthStatus)";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", newUser.User_Username);
                command.Parameters.AddWithValue("@password", newUser.User_Password);              
                command.Parameters.AddWithValue("@picture", newUser.StringProfilePic);
                command.Parameters.AddWithValue("@status", newUser.User_Status);
                command.Parameters.AddWithValue("@healthStatus", newUser.Health_Status);

                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        ///     This will update the user profile excluding the status
        /// </summary>
        /// <param name="userModel">
        ///     will pass an updated version of usermodel
        /// </param>
        public async Task UpdateSpecificUser(UserModel userModel)
        {
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE User_Table SET user_username=@username,user_password=@password,profile_picture=@picture,health_status=@healthStatus";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", userModel.User_Username);
                command.Parameters.AddWithValue("@password", userModel.User_Password);
                command.Parameters.AddWithValue("@picture", userModel.StringProfilePic);
                command.Parameters.AddWithValue("@healthStatus", userModel.Health_Status);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        ///     This will update the status of a specific user
        /// </summary>
        /// <param name="id">
        ///     Id of the student
        /// </param>
        /// <param name="status">
        ///     Status of the student
        /// </param>        
        public async Task UpdateStatusOfUserAsync(int id, string status)
        {
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE User_Table SET user_status = @user_status WHERE user_id=@user_id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user_status", status);
                command.Parameters.AddWithValue("@user_id", id);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
