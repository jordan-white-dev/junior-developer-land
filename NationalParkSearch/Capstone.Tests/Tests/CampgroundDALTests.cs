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
    public class CampgroundDALTests
    {
        private TransactionScope tran;

        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog = NationalParkReservation; Integrated Security = True";
        private int parkID;
        private int campgroundID;
        private int allCampgroundsRowCount = 0;

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

                cmd = new SqlCommand("SELECT COUNT(*) FROM campground", connection);
                allCampgroundsRowCount = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetAllCampgroundsAtParkTest()
        {
            CampgroundDAL campgroundDAL = new CampgroundDAL(connectionString);
            List<Campground> campgrounds = new List<Campground>(campgroundDAL.GetAllCampgroundsAtPark(parkID));

            Assert.IsNotNull(campgrounds);
            //Only 1 campground in our test park
            Assert.AreEqual(1, campgrounds.Count);
        }

        //Bonus
        [TestMethod()]
        public void GetOpenCampgroundsOnDateTest()
        {
            CampgroundDAL campgroundDAL = new CampgroundDAL(connectionString);
        }

        [TestMethod()]
        public void GetCampgroundTest()
        {
            CampgroundDAL campgroundDAL = new CampgroundDAL(connectionString);
            Campground campground = new Campground();
            campground = campgroundDAL.GetCampground(campgroundID);

            Assert.AreEqual("Picnic Zone", campground.Name);
        }

    }
}
