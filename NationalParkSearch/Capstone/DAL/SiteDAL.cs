using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SiteDAL
    {
        private string connectionString = "";
        private const string GetAvailableSitesAtCampgroundCMD =
        @"SELECT DISTINCT TOP(5) s.site_id,s.campground_id, s.site_number, s.max_occupancy, s.accessible, s.max_rv_length, s.utilities, campground.name, (campground.daily_fee * @daysBetween) AS TotalCost " +
        "FROM reservation as r JOIN " +
        "site s ON s.site_id = r.site_id " +
        "JOIN campground ON campground.campground_id = s.campground_id " +
        "WHERE (r.from_date NOT BETWEEN @fromdate1 AND @todate1) " +
        "AND (r.to_date NOT BETWEEN @fromdate2 AND @todate2) " +
        "AND (s.campground_id = @campid)" +
        "AND (campground.open_from_mm <= @fromMonth AND campground.open_from_mm <= @toMonth) " +
        "AND(campground.open_to_mm >= @fromMonth and campground.open_to_mm  >= @toMonth);";
        private const string GetAvailableSitesAtParkCMD =
            @"SELECT DISTINCT site.*, campground.name, (campground.daily_fee * @daysBetween) AS TotalCost
            FROM site JOIN 
            campground ON campground.campground_id = site.campground_id JOIN
            reservation ON site.site_id = reservation.site_id
            WHERE campground.park_id = @parkid AND 
            (reservation.from_date NOT BETWEEN @fromdate AND @todate)
            AND (reservation.to_date NOT BETWEEN @fromdate AND @todate)
            AND (campground.open_from_mm <= @fromMonth AND campground.open_from_mm <= @toMonth) 
            AND (campground.open_to_mm >= @fromMonth and campground.open_to_mm  >= @toMonth);";
        private const string Advanced_GetAvailableSitesAtCampgroundCMD =
            @"SELECT DISTINCT site.*, campground.name, (campground.daily_fee * @daysBetween) AS TotalCost
            FROM site JOIN 
            campground ON campground.campground_id = site.campground_id JOIN
            reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campid AND 
            (reservation.from_date NOT BETWEEN @fromdate AND @todate)
            AND (reservation.to_date NOT BETWEEN @fromdate AND @todate)
            AND (campground.open_from_mm <= @fromMonth AND campground.open_from_mm <= @toMonth) 
            AND (campground.open_to_mm >= @fromMonth and campground.open_to_mm  >= @toMonth)
            AND (site.accessible >= @accessibilityValue)
            AND (site.utilities >= @utilityValue)
            AND (site.max_rv_length >= @rvLength)
            AND (site.max_occupancy >= @numOfOccupants);";
        private const string Advanced_GetAvailableSitesAtParkCMD =
            @"SELECT DISTINCT site.*, campground.name, (campground.daily_fee * @daysBetween) AS TotalCost
            FROM site JOIN 
            campground ON campground.campground_id = site.campground_id JOIN
            reservation ON site.site_id = reservation.site_id
            WHERE campground.park_id = @parkid AND 
            (reservation.from_date NOT BETWEEN @fromdate AND @todate)
            AND (reservation.to_date NOT BETWEEN @fromdate AND @todate)
            AND (site.accessible >= @accessibilityValue)
            AND (site.utilities >= @utilityValue)
            AND (site.max_rv_length >= @rvLength)
            AND (site.max_occupancy >= @numOfOccupants);
            AND(campground.open_from_mm <= @fromMonth AND campground.open_from_mm <= @toMonth)
            AND(campground.open_to_mm >= @fromMonth and campground.open_to_mm  >= @toMonth) ";

        public SiteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetAvailableSitesAtCampground(DateTime fromDate, DateTime toDate, int campgroundID, ref List<Site> sites)
        {
            List<string> result = new List<string>();
            List<Decimal> totalCost = new List<decimal>();
            int daysBetween = (int)(toDate - fromDate).TotalDays + 1;
            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAvailableSitesAtCampgroundCMD, conn);
                    cmd.Parameters.AddWithValue("@fromdate1", fromDate);
                    cmd.Parameters.AddWithValue("@fromdate2", fromDate);
                    cmd.Parameters.AddWithValue("@todate1", toDate);
                    cmd.Parameters.AddWithValue("@todate2", fromDate);
                    cmd.Parameters.AddWithValue("@campid", campgroundID);
                    cmd.Parameters.AddWithValue("@daysBetween", daysBetween);
                    cmd.Parameters.Add("@fromMonth", System.Data.SqlDbType.Int).Value = fromMonth;
                    cmd.Parameters.Add("@toMonth", System.Data.SqlDbType.Int).Value = toMonth;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteID = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        site.MaxOccupants = Convert.ToInt32(reader["max_occupancy"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.SiteNumber = (int)(reader["site_number"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        totalCost.Add(Convert.ToDecimal(reader["TotalCost"]));

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            for (int i = 0; i < sites.Count; i++)
            {
                result.Add(sites[i].ToString() + string.Format("{0:C2}", totalCost[i]));
            }

            return result;
        }

        public List<string> GetAvailableSitesAtPark(DateTime fromDate, DateTime toDate, int parkID, ref List<Site> sites)
        {
            List<string> result = new List<string>();
            List<decimal> totalCosts = new List<decimal>();
            int daysBetween = (int)(toDate - fromDate).TotalDays + 1;
            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAvailableSitesAtParkCMD, conn);
                    cmd.Parameters.Add("@fromdate", System.Data.SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@todate", System.Data.SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.AddWithValue("@parkid", parkID);
                    cmd.Parameters.AddWithValue("@daysBetween", daysBetween);
                    cmd.Parameters.Add("@fromMonth", System.Data.SqlDbType.Int).Value = fromMonth;
                    cmd.Parameters.Add("@toMonth", System.Data.SqlDbType.Int).Value = toMonth;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteID = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        site.MaxOccupants = Convert.ToInt32(reader["max_occupancy"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        string campName = Convert.ToString(reader["name"]);
                        result.Add(campName.PadRight(35));
                        totalCosts.Add(Convert.ToDecimal(reader["TotalCost"]));

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i] += " " + sites[i].ToString() + " " + string.Format("{0:c2}", totalCosts[i]);
            }

            return result;
        }

        public List<string> AdvancedSearchGetAvailableSitesAtCampground(DateTime fromDate, DateTime toDate, int campgroundID, bool accessible, bool utilities, int numOfOccupants, int rvLength, ref List<Site> sites)
        {
            List<string> result = new List<string>();
            List<decimal> totalCosts = new List<decimal>();
            int daysBetween = (int)(toDate - fromDate).TotalDays + 1;
            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(Advanced_GetAvailableSitesAtCampgroundCMD, conn);
                    cmd.Parameters.Add("@fromdate", System.Data.SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@todate", System.Data.SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.AddWithValue("@campid", campgroundID);
                    cmd.Parameters.AddWithValue("@utilityValue", utilities);
                    cmd.Parameters.AddWithValue("@accessibilityValue", accessible);
                    cmd.Parameters.AddWithValue("@rvLength", rvLength);
                    cmd.Parameters.AddWithValue("@numOfOccupants", numOfOccupants);
                    cmd.Parameters.AddWithValue("@daysBetween", daysBetween);
                    cmd.Parameters.Add("@fromMonth", System.Data.SqlDbType.Int).Value = fromMonth;
                    cmd.Parameters.Add("@toMonth", System.Data.SqlDbType.Int).Value = toMonth;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteID = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        site.MaxOccupants = Convert.ToInt32(reader["max_occupancy"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        string campName = Convert.ToString(reader["name"]);
                        result.Add(campName.PadRight(35));
                        totalCosts.Add(Convert.ToDecimal(reader["TotalCost"]));

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            for (int i = 0; i < sites.Count; i++)
            {
                result[i] += " " + sites[i].ToString() + " " + string.Format("{0:c2}", totalCosts[i]);
            }

            return result;
        }

        public List<string> AdvancedSearchGetAvailableSitesAtPark(DateTime fromDate, DateTime toDate, int parkID, bool accessible, bool utilities, int numOfOccupants, int rvLength, ref List<Site> sites)
        {
            List<string> result = new List<string>();
            List<decimal> totalCosts = new List<decimal>();
            int fromMonth = fromDate.Month;
            int toMonth = toDate.Month;
            int daysBetween = (int)(toDate - fromDate).TotalDays + 1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(Advanced_GetAvailableSitesAtParkCMD, conn);
                    cmd.Parameters.Add("@fromdate", System.Data.SqlDbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add("@todate", System.Data.SqlDbType.DateTime).Value = toDate;
                    cmd.Parameters.AddWithValue("@parkid", parkID);
                    cmd.Parameters.AddWithValue("@utilityValue", utilities);
                    cmd.Parameters.AddWithValue("@accessibilityValue", accessible);
                    cmd.Parameters.AddWithValue("@rvLength", rvLength);
                    cmd.Parameters.AddWithValue("@numOfOccupants", numOfOccupants);
                    cmd.Parameters.AddWithValue("@daysBetween", daysBetween);
                    cmd.Parameters.Add("@fromMonth", System.Data.SqlDbType.Int).Value = fromMonth;
                    cmd.Parameters.Add("@toMonth", System.Data.SqlDbType.Int).Value = toMonth;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.SiteID = Convert.ToInt32(reader["site_id"]);
                        site.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        site.MaxOccupants = Convert.ToInt32(reader["max_occupancy"]);
                        site.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        string campName = Convert.ToString(reader["name"]);
                        result.Add(campName.PadRight(35));
                        totalCosts.Add(Convert.ToDecimal(reader["TotalCost"]));

                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i] += " " + sites[i].ToString() + " " + string.Format("{0:c2}", totalCosts[i]);
            }

            return result;
        }
    }
}
