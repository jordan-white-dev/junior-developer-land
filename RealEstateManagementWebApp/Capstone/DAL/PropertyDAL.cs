using Capstone.DAL.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class PropertyDAL : IPropertyDAL
    {
        private const string SQL_AddProperty = "INSERT INTO property (owner_id, manager_id, property_name, property_type, number_of_units, image_source) VALUES (@ownerID, @managerID, @propertyName, @propertyType, @numberOfUnits, @imageSource);";
        private const string SQL_GetAllProperties = "SELECT * FROM property;";
        private const string SQL_GetAvailableProperties = "SELECT DISTINCT property.property_id, property.owner_id, property.manager_id, property.property_name, property.property_type, property.number_of_units, property.image_source FROM property JOIN unit ON property.property_id = unit.property_id WHERE tenant_id IS NULL;";
        private const string SQL_GetPropertiesForOwner = "SELECT p.* FROM property as p JOIN site_user as u ON p.owner_id = u.user_id WHERE p.owner_id = @ownerID;";

        private string connectionString;

        private UnitDAL unitDAL;

        public PropertyDAL(string connectionString)
        {
            this.connectionString = connectionString;
            unitDAL = new UnitDAL(connectionString);
        }

        public bool AddProperty(Property property)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddProperty, connection);
                    cmd.Parameters.AddWithValue("@ownerID", property.OwnerID);
                    cmd.Parameters.AddWithValue("@managerID", property.ManagerID);
                    cmd.Parameters.AddWithValue("@propertyName", property.PropertyName);
                    cmd.Parameters.AddWithValue("@propertyType", property.PropertyType);
                    cmd.Parameters.AddWithValue("@numberOfUnits", property.NumberOfUnits);
                    cmd.Parameters.AddWithValue("@imageSource", (property.ImageSource ?? (object)DBNull.Value));

                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (SqlException ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        public List<Property> GetAvailableProperties()
        {
            List<Property> returnedProperties = new List<Property>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableProperties, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Property property = new Property();

                        property.PropertyID = Convert.ToInt32(reader["property_id"]);
                        property.OwnerID = Convert.ToInt32(reader["owner_id"]);
                        property.ManagerID = Convert.ToInt32(reader["manager_id"]);
                        property.PropertyName = Convert.ToString(reader["property_name"]);
                        property.PropertyType = Convert.ToString(reader["property_type"]);
                        property.NumberOfUnits = Convert.ToInt32(reader["number_of_units"]);
                        //The following if statement prevents an exception by checking if the database value is null before setting the property
                        if (!Convert.IsDBNull(reader["image_source"]))
                        {
                            property.ImageSource = Convert.ToString(reader["image_source"]);
                        }
                        property.UnitsAtThisProperty = unitDAL.GetAvailableUnitsAtProperty(property.PropertyID);

                        returnedProperties.Add(property);
                    }
                }
            }
            catch (SqlException ex)
            {
                returnedProperties = new List<Property>();
                throw ex;
            }

            return returnedProperties;
        }

        public List<Property> GetPropertiesForOwner(int ownerID)
        {
            List<Property> result = new List<Property>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetPropertiesForOwner, connection);
                    cmd.Parameters.AddWithValue("@ownerID", ownerID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Property property = new Property();

                        property.PropertyID = Convert.ToInt32(reader["property_id"]);
                        property.OwnerID = Convert.ToInt32(reader["owner_id"]);
                        property.ManagerID = Convert.ToInt32(reader["manager_id"]);
                        property.PropertyName = Convert.ToString(reader["property_name"]);
                        property.PropertyType = Convert.ToString(reader["property_type"]);
                        property.NumberOfUnits = Convert.ToInt32(reader["number_of_units"]);
                        //The following if statement prevents an exception by checking if the database value is null before setting the property
                        if (!Convert.IsDBNull(reader["image_source"]))
                        {
                            property.ImageSource = Convert.ToString(reader["image_source"]);
                        }
                        property.UnitsAtThisProperty = unitDAL.GetAllUnitsAtProperty(property.PropertyID);

                        result.Add(property);
                    }
                }
            }
            catch (SqlException e)
            {
                result = new List<Property>();
                throw e;
            }

            return result;
        }
    }
}

//public List<Property> GetAllProperties()
//{
//    List<Property> returnedProperties = new List<Property>();

//    try
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();

//            SqlCommand cmd = new SqlCommand(SQL_GetAllProperties, connection);
//            SqlDataReader reader = cmd.ExecuteReader();

//            while (reader.Read())
//            {
//                Property property = new Property();

//                property.PropertyID = Convert.ToInt32(reader["property_id"]);
//                property.OwnerID = Convert.ToInt32(reader["owner_id"]);
//                property.ManagerID = Convert.ToInt32(reader["manager_id"]);
//                property.PropertyName = Convert.ToString(reader["property_name"]);
//                property.PropertyType = Convert.ToString(reader["property_type"]);
//                property.NumberOfUnits = Convert.ToInt32(reader["number_of_units"]);
//                property.ImageSource = Convert.ToString(reader["image_source"]);

//                returnedProperties.Add(property);
//            }
//        }
//    }
//    catch (SqlException ex)
//    {
//        returnedProperties = new List<Property>();
//        throw ex;
//    }

//    return returnedProperties;
//}
