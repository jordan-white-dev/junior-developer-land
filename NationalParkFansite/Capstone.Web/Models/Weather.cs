using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string Code { get; set; }

        public int ForecastDay { get; set; }

        public double LowFahrenheit { get; set; }

        public double HighFahrenheit { get; set; }

        public double LowCelsius
        {
            get
            {
                return (LowFahrenheit - 32) * 0.5556;
            }
        }

        public double HighCelsius
        {
            get
            {
                return (HighFahrenheit - 32) * 0.5556;
            }
        }

        public string Forecast { get; set; }

        public string WeatherAdvice
        {
            get
            {
                switch (Forecast)
                {
                    case "snow":
                        return "Be sure to pack snowshoes!";
                    case "rain":
                        return "Be sure to pack rain gear and wear waterproof shoes!";
                    case "thuderstorms":
                        return "Please seek shelter and avoid hiking on exposed ridges!";
                    case "sun":
                        return "Be sure to pack sunblock!";
                    default:
                        return null;
                }
            }

        }

    }
}
