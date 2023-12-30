using Capstone.DAL.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class UnitDAL : IUnitDAL
    {
        private const string SQL_AddUnit = "INSERT INTO unit (property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, address_line_2, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (@propertyID, @monthlyRent, @squareFeet, @numberOfBeds, @numberOfBaths, @description, @tagline, @imageSource, @applicationFee, @securityDeposit, @petDeposit, @addressLine1, @addressLine2, @city, @state, @zipCode, @washerDryer, @allowCats, @allowDogs, @parkingSpots, @gym, @pool);";
        private const string SQL_GetAvailableUnitsAtProperty = "SELECT * FROM unit JOIN property ON property.property_id = unit.property_id WHERE unit.property_id = @propertyID AND unit.tenant_id IS NULL;";

        private const string SQL_GetAllUnitsAtProperty = "SELECT * FROM unit JOIN property ON property.property_id = unit.property_id WHERE unit.property_id = @propertyID;";

        private string connectionString;

        private PaymentDAL payDAL;

        public UnitDAL(string connectionString)
        {
            this.connectionString = connectionString;
            payDAL = new PaymentDAL(connectionString);
        }

        public bool AddUnit(Unit unit)
        {
            bool result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddUnit, connection);
                    cmd.Parameters.AddWithValue("@propertyID", unit.PropertyID);
                    cmd.Parameters.AddWithValue("@monthlyRent", unit.MonthlyRent);
                    cmd.Parameters.AddWithValue("@squareFeet", unit.SquareFeet);
                    cmd.Parameters.AddWithValue("@numberOfBeds", unit.NumberOfBeds);
                    cmd.Parameters.AddWithValue("@numberOfBaths", unit.NumberOfBaths);
                    cmd.Parameters.AddWithValue("@description", unit.Description);
                    cmd.Parameters.AddWithValue("@tagline", unit.Tagline);
                    cmd.Parameters.AddWithValue("@imageSource", unit.ImageSource ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@applicationFee", unit.ApplicationFee);
                    cmd.Parameters.AddWithValue("@securityDeposit", unit.SecurityDeposit);
                    cmd.Parameters.AddWithValue("@petDeposit", unit.PetDeposit);
                    cmd.Parameters.AddWithValue("@addressLine1", unit.AddressLine1);
                    cmd.Parameters.AddWithValue("@addressLine2", unit.AddressLine2 ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@city", unit.City);
                    cmd.Parameters.AddWithValue("@state", unit.State);
                    cmd.Parameters.AddWithValue("@zipCode", unit.ZipCode);
                    cmd.Parameters.AddWithValue("@washerDryer", unit.WasherDryer);
                    cmd.Parameters.AddWithValue("@allowCats", unit.AllowCats);
                    cmd.Parameters.AddWithValue("@allowDogs", unit.AllowDogs);
                    cmd.Parameters.AddWithValue("@parkingSpots", unit.ParkingSpots ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@gym", unit.Gym);
                    cmd.Parameters.AddWithValue("@pool", unit.Pool);

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

        public List<Unit> GetAvailableUnitsAtProperty(int propertyID)
        {
            List<Unit> returnedUnits = new List<Unit>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableUnitsAtProperty, connection);
                    cmd.Parameters.AddWithValue("@propertyID", propertyID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Unit unit = new Unit();

                        unit.UnitID = Convert.ToInt32(reader["unit_id"]);
                        unit.PropertyID = Convert.ToInt32(reader["property_id"]);
                        //The following if statements prevent an exception by checking if the database value is null before setting the property
                        if (!Convert.IsDBNull(reader["tenant_id"]))
                        {
                            unit.TenantID = Convert.ToInt32(reader["tenant_id"]);
                        }
                        unit.MonthlyRent = Convert.ToDecimal(reader["monthly_rent"]);
                        unit.SquareFeet = Convert.ToInt32(reader["square_feet"]);
                        unit.NumberOfBeds = Convert.ToInt32(reader["number_of_beds"]);
                        unit.NumberOfBaths = Convert.ToDouble(reader["number_of_baths"]);
                        if (!Convert.IsDBNull(reader["description"]))
                        {
                            unit.Description = Convert.ToString(reader["description"]);
                        }
                        if (!Convert.IsDBNull(reader["tagline"]))
                        {
                            unit.Tagline = Convert.ToString(reader["tagline"]);
                        }
                        if (!Convert.IsDBNull(reader["image_source"]))
                        {
                            unit.ImageSource = Convert.ToString(reader["image_source"]);
                        }
                        unit.ApplicationFee = Convert.ToDecimal(reader["application_fee"]);
                        unit.SecurityDeposit = Convert.ToDecimal(reader["security_deposit"]);
                        unit.PetDeposit = Convert.ToDecimal(reader["pet_deposit"]);
                        unit.AddressLine1 = Convert.ToString(reader["address_line_1"]);
                        if (!Convert.IsDBNull(reader["address_line_2"]))
                        {
                            unit.AddressLine2 = Convert.ToString(reader["address_line_2"]);
                        }
                        unit.City = Convert.ToString(reader["city"]);
                        unit.State = Convert.ToString(reader["us_state"]);
                        unit.ZipCode = Convert.ToInt32(reader["zip_code"]);
                        if (!Convert.IsDBNull(reader["washer_dryer"]))
                        {
                            unit.WasherDryer = Convert.ToBoolean(reader["washer_dryer"]);
                        }
                        if (!Convert.IsDBNull(reader["allow_cats"]))
                        {
                            unit.AllowCats = Convert.ToBoolean(reader["allow_cats"]);
                        }
                        if (!Convert.IsDBNull(reader["allow_dogs"]))
                        {
                            unit.AllowDogs = Convert.ToBoolean(reader["allow_dogs"]);
                        }
                        if (!Convert.IsDBNull(reader["parking_spots"]))
                        {
                            unit.ParkingSpots = Convert.ToString(reader["parking_spots"]);
                        }
                        if (!Convert.IsDBNull(reader["gym"]))
                        {
                            unit.Gym = Convert.ToBoolean(reader["gym"]);
                        }
                        if (!Convert.IsDBNull(reader["pool"]))
                        {
                            unit.Pool = Convert.ToBoolean(reader["pool"]);
                        }

                        unit.RentCollectedYTD = payDAL.GetYTDPaymentsforUnit(unit.UnitID);

                        returnedUnits.Add(unit);
                    }
                }
            }
            catch (SqlException ex)
            {
                returnedUnits = new List<Unit>();
                throw ex;
            }


            return returnedUnits;
        }

        //GetAllUnitsAtProperty
        public List<Unit> GetAllUnitsAtProperty(int propertyID)
        {
            List<Unit> returnedUnits = new List<Unit>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllUnitsAtProperty, connection);
                    cmd.Parameters.AddWithValue("@propertyID", propertyID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Unit unit = new Unit();

                        unit.UnitID = Convert.ToInt32(reader["unit_id"]);
                        unit.PropertyID = Convert.ToInt32(reader["property_id"]);
                        //The following if statements prevent an exception by checking if the database value is null before setting the property
                        if (!Convert.IsDBNull(reader["tenant_id"]))
                        {
                            unit.TenantID = Convert.ToInt32(reader["tenant_id"]);
                        }
                        unit.MonthlyRent = Convert.ToDecimal(reader["monthly_rent"]);
                        unit.SquareFeet = Convert.ToInt32(reader["square_feet"]);
                        unit.NumberOfBeds = Convert.ToInt32(reader["number_of_beds"]);
                        unit.NumberOfBaths = Convert.ToDouble(reader["number_of_baths"]);
                        if (!Convert.IsDBNull(reader["description"]))
                        {
                            unit.Description = Convert.ToString(reader["description"]);
                        }
                        if (!Convert.IsDBNull(reader["tagline"]))
                        {
                            unit.Tagline = Convert.ToString(reader["tagline"]);
                        }
                        if (!Convert.IsDBNull(reader["image_source"]))
                        {
                            unit.ImageSource = Convert.ToString(reader["image_source"]);
                        }
                        unit.ApplicationFee = Convert.ToDecimal(reader["application_fee"]);
                        unit.SecurityDeposit = Convert.ToDecimal(reader["security_deposit"]);
                        unit.PetDeposit = Convert.ToDecimal(reader["pet_deposit"]);
                        unit.AddressLine1 = Convert.ToString(reader["address_line_1"]);
                        if (!Convert.IsDBNull(reader["address_line_2"]))
                        {
                            unit.AddressLine2 = Convert.ToString(reader["address_line_2"]);
                        }
                        unit.City = Convert.ToString(reader["city"]);
                        unit.State = Convert.ToString(reader["us_state"]);
                        unit.ZipCode = Convert.ToInt32(reader["zip_code"]);
                        if (!Convert.IsDBNull(reader["washer_dryer"]))
                        {
                            unit.WasherDryer = Convert.ToBoolean(reader["washer_dryer"]);
                        }
                        if (!Convert.IsDBNull(reader["allow_cats"]))
                        {
                            unit.AllowCats = Convert.ToBoolean(reader["allow_cats"]);
                        }
                        if (!Convert.IsDBNull(reader["allow_dogs"]))
                        {
                            unit.AllowDogs = Convert.ToBoolean(reader["allow_dogs"]);
                        }
                        if (!Convert.IsDBNull(reader["parking_spots"]))
                        {
                            unit.ParkingSpots = Convert.ToString(reader["parking_spots"]);
                        }
                        if (!Convert.IsDBNull(reader["gym"]))
                        {
                            unit.Gym = Convert.ToBoolean(reader["gym"]);
                        }
                        if (!Convert.IsDBNull(reader["pool"]))
                        {
                            unit.Pool = Convert.ToBoolean(reader["pool"]);
                        }

                        unit.RentCollectedYTD = payDAL.GetYTDPaymentsforUnit(unit.UnitID);

                        returnedUnits.Add(unit);
                    }
                }
            }
            catch (SqlException ex)
            {
                returnedUnits = new List<Unit>();
                throw ex;
            }

            return returnedUnits;

        }
    }
}
