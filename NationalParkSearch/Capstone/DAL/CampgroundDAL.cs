using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        public string connectionString;
        private const string GetAllCampgroundsAtParkCMD = @"SELECT * FROM campground WHERE park_id = @parkid";
        private const string GetOpenCampgroundsOnDateCMD =
            "SELECT * from campground " +
            "WHERE campground.open_from_mm >= @fromdate AND " +
            "campground.open_to_mm <= @todate;";
        private const string GetCampgroundCMD = @"SELECT * FROM campground WHERE campground_id = @campID";

        public CampgroundDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> GetAllCampgroundsAtPark(int parkID)
        {
            List<Campground> result = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAllCampgroundsAtParkCMD, conn);
                    cmd.Parameters.Add("@parkid", System.Data.SqlDbType.Int).Value = parkID;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.CampID = Convert.ToInt32(reader["campground_id"]);
                        campground.Name = Convert.ToString(reader["name"]);
                        campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
                        campground.ParkID = Convert.ToInt32(reader["park_id"]);
                        campground.FromMonth = Convert.ToInt32(reader["open_from_mm"]);
                        campground.ToMonth = Convert.ToInt32(reader["open_to_mm"]);

                        result.Add(campground);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public List<Campground> GetOpenCampgroundsOnDate(int parkID, DateTime fromDate, DateTime toDate)
        {
            List<Campground> openCampgrounds = new List<Campground>();
            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetOpenCampgroundsOnDateCMD, conn);

                    cmd.Parameters.AddWithValue("@fromMonth", fromMonth);
                    cmd.Parameters.AddWithValue("@toMonth", toMonth);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.CampID = Convert.ToInt32(reader["campground_id"]);
                        campground.Name = Convert.ToString(reader["name"]);
                        campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
                        campground.ParkID = Convert.ToInt32(reader["park_id"]);
                        campground.FromMonth = Convert.ToInt32(reader["open_from_mm"]);
                        campground.ToMonth = Convert.ToInt32(reader["open_to_mm"]);

                        openCampgrounds.Add(campground);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return openCampgrounds;
        }

        public Campground GetCampground(int campID)
        {
            Campground campground = new Campground();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetCampgroundCMD, conn);
                    cmd.Parameters.AddWithValue("@campID", campID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        campground.CampID = Convert.ToInt32(reader["campground_id"]);
                        campground.DailyFee = Convert.ToDecimal(reader["daily_fee"]);
                        campground.ParkID = Convert.ToInt32(reader["park_id"]);
                        campground.FromMonth = Convert.ToInt32(reader["open_from_mm"]);
                        campground.ToMonth = Convert.ToInt32(reader["open_to_mm"]);
                        campground.Name = Convert.ToString(reader["name"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return campground;
        }
    }
}
