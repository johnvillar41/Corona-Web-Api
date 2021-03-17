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
    public class MclTotalRepository : IMclTotalRepository
    {
        /// <summary>
        ///     This fetches all the data from the total values summarized for covid mcl values
        /// </summary>
        /// <returns>
        ///     returns a new MclTotalModel
        /// </returns>
        public async Task<MclTotalModel> FetchTotals()
        {
            MclTotalModel mclTotalModel = null;
            using (SqlConnection connection = new SqlConnection(DBCredentials.CONNECTION_STRING))
            {
                await connection.OpenAsync();
                string sqlQuery = "SELECT * FROM MclTotal_Table";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        mclTotalModel = new MclTotalModel
                        {
                            TotalConfirmed = int.Parse(reader["totalConfirmed"].ToString()),
                            TotalTested = int.Parse(reader["totalTested"].ToString()),
                            TotalDeaths = int.Parse(reader["totalDeaths"].ToString()),
                            TotalRecovered = int.Parse(reader["totalRecovered"].ToString())
                        };
                    }
                }
            }
            return mclTotalModel;
        }
    }
}
