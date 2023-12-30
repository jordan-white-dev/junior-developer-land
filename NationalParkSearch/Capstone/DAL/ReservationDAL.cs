using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString = "";
        private const string AddReservationCMD =
            @"INSERT INTO reservation (site_id, name, from_date, to_date, create_date)
            VALUES (@siteid, @name, @fromdate, @todate, GETDATE()); SELECT CAST(SCOPE_IDENTITY() as int)";
        private const string GetAllReservationsCMD = @"SELECT * FROM reservation;";
        private const string GetReservationCostCMD =
            @"SELECT (campground.daily_fee * (@DaysAtSite)) AS TotalCost
            FROM campground JOIN
            site ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campgroundID ";
        private const string GetReservationCMD = "SELECT * FROM reservation WHERE reservation_id = @reservationID";
        private const string GetAllReservationsForNext30DaysCMD =
            @"SELECT reservation.*, campground.name AS CampName
            FROM reservation
            JOIN site ON site.site_id = reservation.site_id
            JOIN campground ON site.campground_id = campground.campground_id
            WHERE reservation.from_date <= GETDATE()+ 30
            AND park_id = @parkid;";


        public ReservationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddReservation(Reservation reservation)
        {
            int newID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(AddReservationCMD, connection);

                    cmd.Parameters.Add("@siteid", System.Data.SqlDbType.Int).Value = reservation.SiteID;
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = reservation.Name;
                    cmd.Parameters.Add("@fromdate", System.Data.SqlDbType.DateTime).Value = reservation.FromDate;
                    cmd.Parameters.Add("@todate", System.Data.SqlDbType.DateTime).Value = reservation.ToDate;
                    newID = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return newID;
        }

        public List<Reservation> GetAllReservations()
        {
            List<Reservation> allReservations = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAllReservationsCMD, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                        reservation.Name = Convert.ToString(reader["name"]);
                        reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
                        reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
                        reservation.CreationDate = Convert.ToDateTime(reader["create_date"]);
                        reservation.SiteID = Convert.ToInt32(reader["site_id"]);

                        allReservations.Add(reservation);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return allReservations;
        }

        public decimal GetReservationCost(DateTime fromDate, DateTime toDate, int campgroundID)
        {
            int daysBetween = (int)(toDate - fromDate).TotalDays +1;
            decimal cost = 0M;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetReservationCostCMD, conn);
                    cmd.Parameters.Add("@campgroundid", System.Data.SqlDbType.Int).Value = campgroundID;
                    cmd.Parameters.Add("@DaysAtSite", System.Data.SqlDbType.Int).Value = daysBetween;
                    cost = (decimal)cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cost;
        }

        public Reservation GetReservation(int reservationID)
        {
            Reservation reservation = new Reservation();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetReservationCMD, conn);
                    cmd.Parameters.AddWithValue("@reservationID", reservationID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                        reservation.SiteID = Convert.ToInt32(reader["site_id"]);
                        reservation.Name = Convert.ToString(reader["name"]);
                        reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
                        reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
                        reservation.CreationDate = Convert.ToDateTime(reader["create_date"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return reservation;
        }

        public List<string> GetReservationsInNext30Days(int parkID)
        {
            List<Reservation> reservations = new List<Reservation>();
            List<string> resultOutput = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(GetAllReservationsForNext30DaysCMD, conn);
                    cmd.Parameters.Add("@parkid", System.Data.SqlDbType.Int).Value = parkID;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                        reservation.Name = Convert.ToString(reader["name"]);
                        reservation.FromDate = Convert.ToDateTime(reader["from_date"]);
                        reservation.ToDate = Convert.ToDateTime(reader["to_date"]);
                        reservation.CreationDate = Convert.ToDateTime(reader["create_date"]);
                        reservation.SiteID = Convert.ToInt32(reader["site_id"]);
                        string campName = Convert.ToString(reader["CampName"]);
                        resultOutput.Add(campName.PadRight(35));
                        reservations.Add(reservation);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            for (int i = 0; i < resultOutput.Count; i++)
            {
                resultOutput[i] +=  reservations[i].ToString();
            }

            return resultOutput;
        }
    }
}
