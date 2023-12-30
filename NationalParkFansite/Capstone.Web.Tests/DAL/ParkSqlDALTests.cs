using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Web;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.DAL
{
    [TestClass()]
    public class ParkSqlDALTests 
    {
        private TransactionScope tran;

        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();
              
                cmd = new SqlCommand("INSERT INTO park VALUES ('JNP', 'JellyStone National Park', 'Ohio', 600000, 3333, 300, 42, 'Mushroom', 3000, 1000000, 'Smarter than the average bear!', 'Yogi', 'Pinic basket capital of the world.', 0, 0);", conn);
                cmd.ExecuteNonQuery();
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
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL(connectionString);
            List<Park> parks = parkSqlDAL.GetAllParks();
            Assert.IsNotNull(parks);
        }

        [TestMethod()]
        public void GetParkTest()
        {
            ParkSqlDAL parkSqlDAL = new ParkSqlDAL(connectionString);
            Park park = parkSqlDAL.GetPark("JNP");
            Assert.IsNotNull(park);
            Assert.AreEqual("Pinic basket capital of the world.", park.Description);
        }
    }

}


