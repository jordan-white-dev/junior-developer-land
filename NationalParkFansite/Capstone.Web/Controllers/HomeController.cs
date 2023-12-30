using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkSqlDAL parkSqlDAL;
        private IWeatherSqlDAL weatherSqlDAL;
        private const string TEMP_FAHRENHEIT = "Fahrenheit";
        private const string TEMP_CELCIUS = "Celcius";

        public HomeController(IParkSqlDAL parkSqlDAL, IWeatherSqlDAL weatherSqlDAL)
        {
            this.parkSqlDAL = parkSqlDAL;
            this.weatherSqlDAL = weatherSqlDAL;
        }

        public IActionResult Index()
        {
            List<Park> parks = parkSqlDAL.GetAllParks();
            return View(parks);
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            ParkWeatherViewModel parkWeatherViewModel = new ParkWeatherViewModel();
            Park park = parkSqlDAL.GetPark(id);
            List<Weather> weathers = weatherSqlDAL.GetWeatherAtPark(id);
            parkWeatherViewModel.Park = park;
            parkWeatherViewModel.Weather = weathers;
            if (HttpContext.Session.GetString("CurrentTemp") == null)
            {
                HttpContext.Session.Set("CurrentTemp", TEMP_FAHRENHEIT);
            }
            parkWeatherViewModel.TempType = HttpContext.Session.GetString("CurrentTemp");
            return View(parkWeatherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CelciusDetail(string id)
        {
            HttpContext.Session.Set("CurrentTemp", TEMP_CELCIUS);
            return RedirectToAction("Detail", new {id = id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FahrenheitDetail(string id)
        {
            HttpContext.Session.Set("CurrentTemp", TEMP_FAHRENHEIT);
            return RedirectToAction("Detail", new { id = id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
