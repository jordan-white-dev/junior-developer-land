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
    public class UnitDALTests
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
        public void AddUnitTest()
        {
            UnitDAL unitDAL = new UnitDAL(connectionString);
            Unit unit = new Unit()
            {
                PropertyID = 1,
                MonthlyRent = 999,
                SquareFeet = 999,
                NumberOfBeds = 9,
                NumberOfBaths = 9,
                Description = "9 Testing Rooms",
                Tagline = "Never enough tests",
                ApplicationFee = 99,
                SecurityDeposit = 999,
                PetDeposit = 99,
                AddressLine1 = "99 Test Lane",
                City = "Testville",
                State = "Ohio",
                ZipCode = 99999,
                WasherDryer = false,
                AllowCats = false,
                AllowDogs = false,
                ParkingSpots = "9 Car Garage",
                Gym = false,
                Pool = false
            };

            bool result = unitDAL.AddUnit(unit);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetAllUnitsAtPropertyTest()
        {
            UnitDAL unitDAL = new UnitDAL(connectionString);
            List<Unit> units = unitDAL.GetAvailableUnitsAtProperty(999);

            Assert.IsNotNull(units);
        }
    }
}
