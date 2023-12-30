using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests.Tests
{
    [TestClass]
    public class ParkDALTests
    {
        private TransactionScope tran;

        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog = NationalParkReservation; Integrated Security = True";
        private int parkID;
        private int allParksRowCount = 0;

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

                cmd = new SqlCommand("SELECT COUNT(*) FROM park", connection);
                allParksRowCount = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetAllParksTest()
        {
            ParkDAL parkDAL = new ParkDAL(connectionString);
            List<Park> parks = new List<Park>(parkDAL.GetAllParks());

            Assert.IsNotNull(parks);
            Assert.AreEqual(allParksRowCount, parks.Count);
        }

        [TestMethod()]
        public void GetParkTest()
        {
            ParkDAL parkDAL = new ParkDAL(connectionString);
            Park park = new Park();
            park = parkDAL.GetPark(parkID);

            Assert.AreEqual("Jellystone", park.Name);
        }

    }
}
