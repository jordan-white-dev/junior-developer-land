using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDALTests
    {
        private TransactionScope tran;

        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog = NationalParkReservation; Integrated Security = True";
        private int parkID;
        private int campgroundID;
        private int siteID;
        private int reservationID;
        private int allReservationsRowCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd;

                cmd = new SqlCommand("INSERT INTO park (name, location, establish_date, area, visitors, description) VALUES ('Jellystone', 'Ohio', '1900-01-01', 38420, 123456, 'Bears really love to steal picnic baskets here.'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                parkID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@parkid, 'Picnic Zone', 10, 12, 100.00); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                cmd.Parameters.AddWithValue("@parkid", parkID);
                campgroundID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO site (campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (@campgroundid, 13, 1, 0, 0, 0); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                cmd.Parameters.AddWithValue("@campgroundid", campgroundID);
                siteID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date) VALUES (@siteid, 'Jason Family Reservation', '2019-03-15', '2019-03-18', '2019-02-02 03:03:03.300'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                cmd.Parameters.AddWithValue("@siteid", siteID);
                reservationID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM reservation", connection);
                allReservationsRowCount = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetAllReservationsTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);

            List<Reservation> reservations = new List<Reservation>(reservationDAL.GetAllReservations());

            Assert.IsNotNull(reservations);
            Assert.AreEqual(allReservationsRowCount, reservations.Count);
        }

        [TestMethod()]
        public void GetReservationCostTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);
            DateTime toDate = new DateTime(2019, 03, 18);
            DateTime fromDate = new DateTime(2019, 03, 15);
        

            decimal totalCost = reservationDAL.GetReservationCost(fromDate, toDate, campgroundID);

            Assert.AreEqual(400.00M, totalCost);
        }

        [TestMethod()]
        public void GetReservationTest()
        {
            ReservationDAL reservationDAL = new ReservationDAL(connectionString);

            Reservation reservation = reservationDAL.GetReservation(reservationID);

            Assert.IsNotNull(reservation);
            Assert.AreEqual("Jason Family Reservation", reservation.Name );
        }



    }
}
