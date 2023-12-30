using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL.Interfaces;

namespace Capstone.Test.DALTests
{
    [TestClass]
    public class PropertyDALTests
    {
        private TransactionScope tran;

        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RealEstateManagement;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void AddPropertyTest()
        {
            PropertyDAL propertyDAL = new PropertyDAL(connectionString);
            Property property = new Property()
            {
                OwnerID = 2,
                ManagerID = 1,
                PropertyName = "Test Paradise",
                PropertyType = "Triplex",
                NumberOfUnits = 3
            };

            bool result = propertyDAL.AddProperty(property);

            Assert.IsTrue(result);
        }

        //Because the "GetAvailableProperties()" Method calls to another DAL, testing it is incredibly tricky
    }
}
