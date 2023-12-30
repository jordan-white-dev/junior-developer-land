using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkSqlDAL
    {
        private string connectionString;

        private const string SQL_GetAllParks = "SELECT * FROM park;";
        private const string SQL_GetPark = "SELECT * FROM park WHERE parkCode = @parkCode;";

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.Code = Convert.ToString(reader["parkCode"]);
                        park.Name = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                        park.NumberOfSites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.VisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.Quote = Convert.ToString(reader["inspirationalQuote"]);
                        park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.Description = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                        parks.Add(park);
                    }
                }
            }
            catch (SqlException)
            {
                parks = new List<Park>();
            }

            return parks;
        }

        public Park GetPark(string parkCode)
        {
            Park park = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        park.Code = Convert.ToString(reader["parkCode"]);
                        park.Name = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                        park.NumberOfSites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.VisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.Quote = Convert.ToString(reader["inspirationalQuote"]);
                        park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.Description = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                    }
                }
            }
            catch (SqlException)
            {
                park = new Park();
            }

            return park;
        }

        public List<SelectListItem> GetParksSelectList()
        {
            List<SelectListItem> output = new List<SelectListItem>();

            //Always wrap connection to a database in a try-catch block
            try
            {
                //Create a SqlConnection to our database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetAllParks;
                    cmd.Connection = connection;

                    // Execute the query to the database
                    SqlDataReader reader = cmd.ExecuteReader();

                    // The results come back as a SqlDataReader. Loop through each of the rows
                    // and add to the output list
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();

                        item.Text = Convert.ToString(reader["parkName"]);
                        item.Value = Convert.ToString(reader["parkCode"]);

                        output.Add(item);
                    }
                }
            }
            catch (SqlException ex)
            {
                output = new List<SelectListItem>();
            }

            // Return the list of continents
            return output;
        }
    }
}
