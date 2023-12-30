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
    public class ApplicationDALTests
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

                    cmd = new SqlCommand("SET IDENTITY_INSERT property ON; INSERT INTO property (property_id, owner_id, manager_id, property_name, property_type, number_of_units) VALUES (999, 2, 1, 'Test Lofts', 'Single Family', 1); SET IDENTITY_INSERT property OFF;", conn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SET IDENTITY_INSERT site_user ON; INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (999, 'Test', 'Johnson', '555-555-5555', 'test@test.com', 'tenant', 'password', 'salt'); SET IDENTITY_INSERT site_user OFF;", conn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SET IDENTITY_INSERT unit ON; INSERT INTO unit (unit_id, property_id, tenant_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (999, 999, 999, 9999, 9999, 9, 9, 'Testing is really good and this is one', 'TEST!!!', 99, 999, 99, '99 Testing Lane', 'Testville', 'Ohio', 99999, 0, 0, 0, 'Test Parking', 0, 0); SET IDENTITY_INSERT unit OFF;", conn);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SET IDENTITY_INSERT tenant_application ON; INSERT INTO tenant_application (application_id, unit_id, first_name, last_name, phone_number, email_address, last_residence_owner, last_residence_contact_phone_number, last_residence_tenancy_start_date, last_residence_tenancy_end_date, employment_status, employer_name, employer_contact_phone_number, annual_income, number_of_residents, number_of_cats, number_of_dogs) VALUES (999, 999, 'Test', 'Johnson', '555-555-5555', 'test@test.com', 'NA', '555-555-5555', 'NA', 'NA', 0, 'Self-Employed', 'NA', 'NA', 9, 9, 9); SET IDENTITY_INSERT tenant_application OFF;", conn);
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
        public void AddApplicationTest()
        {
            ApplicationDAL applicationDAL = new ApplicationDAL(connectionString);
            int preInsertCount = applicationDAL.GetAllApplications().Count;

            Application testApplication = new Application()
            {
                UnitID = 1,
                FirstName = "Test",
                LastName = "Johnson",
                PhoneNumber = "555-555-5555",
                EmailAddress = "test@test.com",
                LastResidenceOwner = "Test Management Services",
                LastResidenceContactPhoneNumber = "666-666-6666",
                LastResidenceTenancyStartDate = "January 1, 0000",
                LastResidenceTenancyEndDate = "December 31, 9999",
                EmploymentStatus = false,
                EmployerName = "Test Recruiting Company",
                EmployerContactPhoneNumber = "777-777-7777",
                AnnualIncome = "99,999",
                NumberOfResidents = 9,
                NumberOfCats = 9,
                NumberOfDogs = 9
            };

            bool checkAddApplicationSuccess = applicationDAL.AddApplication(testApplication);
            int postInsertCount = applicationDAL.GetAllApplications().Count;

            Assert.AreEqual(true, checkAddApplicationSuccess);
            Assert.AreEqual(preInsertCount + 1, postInsertCount);
        }

        [TestMethod()]
        public void GetAllApplicationsTest()
        {
            ApplicationDAL applicationDAL = new ApplicationDAL(connectionString);
            List<Application> applications = applicationDAL.GetAllApplications();

            Application testApplication = new Application();

            foreach (Application application in applications)
            {
                if (application.ApplicationID == 999)
                {
                    testApplication = application;
                }
            }

            Assert.IsNotNull(applications);
            CollectionAssert.Contains(applications, testApplication);
        }
    }
}
