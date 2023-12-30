using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherSqlDAL
    {
        private string connectionString;

        private const string SQL_WeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode;";

        public WeatherSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetWeatherAtPark(string parkCode)
        {
            List<Weather> weathers = new List<Weather>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_WeatherForPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Weather weather = new Weather();
                        weather.Code = Convert.ToString(reader["parkCode"]);
                        weather.ForecastDay = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.LowFahrenheit = Convert.ToDouble(reader["low"]);
                        weather.HighFahrenheit = Convert.ToDouble(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        if (weather.Forecast == "partly cloudy")
                        {
                            weather.Forecast = "partlyCloudy";
                        }
                        weathers.Add(weather);
                    }
                }
            }
            catch (SqlException)
            {
                weathers = new List<Weather>();
            }

            return weathers;
        }
    }
}
