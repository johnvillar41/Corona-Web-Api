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
    }
}
