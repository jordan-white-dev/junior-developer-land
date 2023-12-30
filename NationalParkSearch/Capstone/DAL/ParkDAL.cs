using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkDAL
    {
        private string connectionString;
        private const string AddParkCMD = "";
        private const string DeleteParkCMD = "";
        private const string GetAllParksCMD = @"SELECT * FROM park ORDER BY park.Name;";
        private const string GetParkCMD = @"SELECT * FROM park WHERE park_id = @parkID;";

        public ParkDAL (string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddPark (Park park)
        {
            return false;
        }
        
        public bool DeletePark (int parkID)
        {
            return false;
        }

        public List<Park> GetAllParks ()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAllParksCMD, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Park park = new Park();
                        park.ParkID = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Description = Convert.ToString(reader["description"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.AnnualVisitors = Convert.ToInt32(reader["visitors"]);

                        parks.Add(park);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return parks;
        }
        
        public Park GetPark (int parkID)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetParkCMD, conn);
                    cmd.Parameters.Add("@parkid", System.Data.SqlDbType.Int).Value = parkID;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        park.ParkID = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        park.Description = Convert.ToString(reader["description"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.AnnualVisitors = Convert.ToInt32(reader["visitors"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return park;
        }
    }
}
