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
    public class SiteDALTests
    {
        private TransactionScope tran;

        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog = NationalParkReservation; Integrated Security = True";
        private int parkID;
        private int campgroundID;
        private int siteID;
        private int allSitesRowCount = 0;
        private int reservationID;

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

                cmd = new SqlCommand("SELECT COUNT(*) FROM site", connection);
                allSitesRowCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date) VALUES (@siteid, 'Jason Family Reservation', '2019-03-15', '2019-03-18', '2019-02-02 03:03:03.300'); SELECT CAST(SCOPE_IDENTITY() as int);", connection);
                cmd.Parameters.AddWithValue("@siteid", siteID);
                reservationID = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAvailableSitesAtCampgroundTestSuccess()
        {
            SiteDAL siteDAL = new SiteDAL(connectionString);
            DateTime toDate = new DateTime(2019, 02, 01);
            DateTime fromDate = new DateTime(2019, 02, 24);
            List<Site> sites = new List<Site>();
            List<string> availableSites = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref sites);
            Assert.IsNotNull(sites);
        }
        [TestMethod]
        public void GetAvailableSitesAtCampgroundTestFailure()
        {
            SiteDAL siteDAL = new SiteDAL(connectionString);
            DateTime toDate = new DateTime(2019, 03, 18);
            DateTime fromDate = new DateTime(2019, 04, 15);

            List<Site> sites = new List<Site>();
            List<string> availableSites = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref sites);
            Assert.AreEqual(0, sites.Count);

            // Next test for only 1 date overlapping
            DateTime toDate2 = new DateTime(2019, 02, 15);
            DateTime fromDate2 = new DateTime(2019, 03, 15);
            List<Site> sites2 = new List<Site>();
            List<string> availableSites2 = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref sites2);
            Assert.AreEqual(0, sites2.Count);

            // Next test for only 1 date overlapping
            DateTime toDate3 = new DateTime(2019, 03, 16);
            DateTime fromDate3 = new DateTime(2019, 03, 17);
            List<Site> sites3 = new List<Site>();
            List<string> availableSites3 = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref sites3);

            Assert.AreEqual(0, sites3.Count);

            // Next test for only 1 date overlapping
            DateTime toDate4 = new DateTime(2019, 03, 15);
            DateTime fromDate4 = new DateTime(2019, 03, 18);
            List<Site> sites4 = new List<Site>();
            List<string> availableSites4 = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref sites4);
            Assert.AreEqual(0, sites4.Count);
        }

        //Bonus
        [TestMethod]
        public void GetAvailableSitesAtParkTest()
        {
            SiteDAL siteDAL = new SiteDAL(connectionString);
        }

    }
}
