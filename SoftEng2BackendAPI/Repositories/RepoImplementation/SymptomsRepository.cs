using Microsoft.AspNetCore.Mvc;
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
    public class SymptomsRepository : ISymptomRepository
    {
        public async Task<IEnumerable<SymptomsModel>> FetchAllSymptomsAsync()
        {
            List<SymptomsModel> symptomsList = new List<SymptomsModel>();
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM Symptoms_Table";
                connection.Open();
                SqlCommand command = new SqlCommand(queryString,connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    symptomsList.Add(new SymptomsModel(
                        int.Parse(reader["symptoms_id"].ToString()),
                        int.Parse(reader["user_id"].ToString()),
                        reader["symptoms_name"].ToString()));
                }
            }
            return symptomsList;
        }
        public Task<IEnumerable<SymptomsModel>> FetchAllSymptomsForSpecificStudentAsync(int student_id)
        {
            throw new NotImplementedException();
        }

        public Task<SymptomsModel> FetchSpecificSymptomAsync(int symptom_id)
        {
            throw new NotImplementedException();
        }
    }
}
