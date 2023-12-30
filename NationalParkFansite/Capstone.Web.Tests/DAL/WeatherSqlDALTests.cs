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
    public class WeatherSqlDALTest
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

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 1, 100, 200, 'mega rain');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 2, 200, 300, 'super rain');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 3, 300, 400, 'ultra rain');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 4, 400, 500, 'apocalyptic rain');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO weather VALUES ('JNP', 5, 500, 600, 'waterworld');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetWeatherAtParkTest()
        {
            WeatherSqlDAL weatherSqlDAL = new WeatherSqlDAL(connectionString);
            List<Weather> weathers = weatherSqlDAL.GetWeatherAtPark("JNP");

            Assert.IsNotNull(weathers);
            Assert.AreEqual("mega rain", weathers[0].Forecast);
        }

    }
}


