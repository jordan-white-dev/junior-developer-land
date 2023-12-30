using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private IParkSqlDAL parkSqlDAL;
        private ISurveySqlDAL surveySqlDAL;

        public SurveyController(IParkSqlDAL parkSqlDAL, ISurveySqlDAL surveySqlDAL)
        {
            this.surveySqlDAL = surveySqlDAL;
            this.parkSqlDAL = parkSqlDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ParkSelectList = parkSqlDAL.GetParksSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Survey model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParkSelectList = parkSqlDAL.GetParksSelectList();
                return View(model);
            }
            else
            {
            surveySqlDAL.AddSurveyToDatabase(model);

            return RedirectToAction("Favorites");
            }
        }

        public IActionResult Favorites()
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel();
            surveyViewModel.Parks = parkSqlDAL.GetAllParks();
            surveyViewModel.Surveys = surveySqlDAL.GetFavorites();
            return View(surveyViewModel);
        }
    }
}