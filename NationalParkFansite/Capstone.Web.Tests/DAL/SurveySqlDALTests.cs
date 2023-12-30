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
    public class SurveySqlDALTests
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

                cmd = new SqlCommand("INSERT INTO survey_result(parkCode, emailAddress, state, activityLevel) VALUES ('JNP', 'test@test.com', 'Ohio', 'active');", conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void GetFavoritesTests()
        {
            SurveySqlDAL surveySqlDAL = new SurveySqlDAL(connectionString);
            Dictionary<string, int> result = surveySqlDAL.GetFavorites();

            Assert.AreEqual(true, result.ContainsKey("JNP"));
            Assert.AreEqual(true, result.ContainsValue(1));
        }

        [TestMethod()]
        public void AddSurveyToDatabaseTests()
        {
            SurveySqlDAL surveySqlDAL = new SurveySqlDAL(connectionString);
            Survey survey = new Survey()
            {
                ParkCode = "JNP",
                Email = "bob@test.com",
                State = "Ohio",
                ActivityLevel = "active"
            };
            bool result = surveySqlDAL.AddSurveyToDatabase(survey);

            Assert.IsTrue(result);
        }
    }

}


