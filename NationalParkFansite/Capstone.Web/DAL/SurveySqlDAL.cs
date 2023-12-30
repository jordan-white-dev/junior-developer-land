using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveySqlDAL
    {
        private string connectionString;

        private const string SQL_FindFavoriteParks = "SELECT COUNT(parkCode) as surveys, parkCode FROM survey_result GROUP BY parkCode ORDER BY surveys DESC, parkCode;";
        private const string SQL_AddSurveyToDatabase = "INSERT INTO survey_result(parkCode, emailAddress, state, activityLevel) VALUES (@parkCode, @emailAddress, @state, @activityLevel);";

        public SurveySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Dictionary<string, int> GetFavorites()
        {
            Dictionary<string, int> favorites = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_FindFavoriteParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        favorites[Convert.ToString(reader["parkCode"])] = Convert.ToInt32(reader["surveys"]);
                    }
                }
            }
            catch (SqlException)
            {
                favorites = new Dictionary<string, int>();
            }

            return favorites;
        }

        public bool AddSurveyToDatabase(Survey survey)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_AddSurveyToDatabase, conn);

                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();

                    result = true;
                }
            }
            catch (SqlException)
            {
                result = false;
            }

            return result;
        }
    }
}
