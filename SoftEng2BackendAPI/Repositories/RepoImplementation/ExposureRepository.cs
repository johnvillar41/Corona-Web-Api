using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Models;
using SoftEng2BackendAPI.Repositories.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.RepoImplementation
{
    public class ExposureRepository : IExposureRepository
    {
        /// <summary>
        ///     This will fetch all the ExposedModels
        /// </summary>
        /// <returns>
        ///     Return a list of ExposedModels
        /// </returns>
        public async Task<IEnumerable> FetchAllExposedStudentsAsync()
        {
            List<ExposureModel> exposedList = new List<ExposureModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM Exposure_Table";
                SqlCommand command = new SqlCommand(queryString, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        ExposureModel exposureModel = new ExposureModel
                        {
                            Exposure_ID = int.Parse(reader["exposure_id"].ToString()),
                            User_ID = int.Parse(reader["user_id"].ToString().ToString()),
                            Exposed_To_ID = int.Parse(reader["exposed_to_id"].ToString()),
                            Exposed_Date = Convert.ToDateTime(reader["exposed_date"].ToString())
                        };
                        exposedList.Add(exposureModel);
                    }
                }
            }
            return exposedList;
        }
        /// <summary>
        ///     This will fetch all the students that are exposed from the student given the id
        /// </summary>
        /// <param name="id">
        ///     Id of the student who will be searched
        /// </param>
        /// <returns>
        ///     Will return a list of exposed UserModels
        /// </returns>
        public async Task<IEnumerable<UserModel>> FetchExposedStudentsGivenByIDAsync(int id)
        {

            List<UserModel> exposedStudents = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                connection.Open();
                string queryString = "SELECT Exposure_Table.exposed_to_id,User_Table.user_username,User_Table.profile_picture,User_Table.user_status FROM Exposure_Table INNER JOIN User_Table ON Exposure_Table.user_id = User_Table.user_id WHERE Exposure_Table.user_id=@user_id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user_id",id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        UserModel user = new UserModel
                        {
                            User_ID = int.Parse(reader["exposed_to_id"].ToString()),
                            User_Username = reader["user_username"].ToString(),                            
                            StringProfilePic = reader["profile_picture"].ToString(),
                            User_Status = reader["user_status"].ToString()
                        };
                        exposedStudents.Add(user);
                    }
                }
                return exposedStudents;
            }
        }
        /// <summary>
        ///     This function will insert a new Exposed Student to the database and inverting those values so that both of the students are exposed to each other
        /// </summary>
        /// <param name="exposureModel"></param>
        /// <returns>
        ///     Returns a void
        /// </returns>
        public async Task InsertNewExposedStudentAsync(ExposureModel exposureModel)
        {
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                //Created two queries which swaps the values so that the user who is exposed will also be exposed to the other and vice versa
                string queryString = "INSERT INTO Exposure_Table(user_id,exposed_to_id,exposed_date) VALUES(@user_id,@exposed_id,@exposed_date)";
                string queryString2 = "INSERT INTO Exposure_Table(exposed_to_id,user_id,exposed_date) VALUES(@user_id,@exposed_id,@exposed_date)";
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user_id", exposureModel.User_ID);
                command.Parameters.AddWithValue("@exposed_id", exposureModel.Exposed_To_ID);
                command.Parameters.AddWithValue("@exposed_date", exposureModel.Exposed_Date);
                SqlCommand command2 = new SqlCommand(queryString2, connection);
                command2.Parameters.AddWithValue("@user_id", exposureModel.User_ID);
                command2.Parameters.AddWithValue("@exposed_id", exposureModel.Exposed_To_ID);
                command2.Parameters.AddWithValue("@exposed_date", exposureModel.Exposed_Date);
                await command.ExecuteNonQueryAsync();
                await command2.ExecuteNonQueryAsync();
            }            
        }
        /// <summary>
        ///     This function will update the exposed student values in database
        /// </summary>
        /// <param name="exposedModel">
        ///     A new exposedModel will be passed as a parameter
        /// </param>       
        public async Task UpdateExposedStudentAsync(ExposureModel exposedModel)
        {
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE Exposure_Table(user_id,exposed_to_id,exposed_date) VALUES (@user_id,@exposed_to_id,@exposed_date) WHERE exposed_id=@exposed_id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@user_id", exposedModel.User_ID);
                command.Parameters.AddWithValue("@exposed_to_id", exposedModel.Exposed_To_ID);
                command.Parameters.AddWithValue("@exposed_date", exposedModel.Exposed_Date);
                command.Parameters.AddWithValue("@exposed_id", exposedModel.Exposure_ID);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
