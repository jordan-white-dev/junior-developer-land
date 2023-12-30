using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Test.DALTests
{
    [TestClass]
    public class ServiceRequestDALTests
    {
        private TransactionScope tran;

        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RealEstateManagement;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;

                    conn.Open();

                    cmd = new SqlCommand("SET IDENTITY_INSERT site_user ON; INSERT INTO site_user(user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES(999, 'Test', 'Johnson', '555-555-5555', 'test@test.com', 'tenant', 'password', 'salt'); SET IDENTITY_INSERT site_user OFF;", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void AddServiceRequestTest()
        {
            ServiceRequestDAL serviceRequestDAL = new ServiceRequestDAL(connectionString);
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                TenantID = 999,
                Description = "There are not enough tests in my house",
                IsEmergency = false,
                Category = "other",
                IsCompleted = false
            };

            bool result = serviceRequestDAL.AddServiceRequest(serviceRequest);

            Assert.IsTrue(result);
        }

        //TODO: Add GetAllServiceRequestsTest()
    }
}
