using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class OwnerController : HomeController
    {

        public OwnerController(IApplicationDAL applicationDAL, IPropertyDAL propertyDAL, IHttpContextAccessor contextAccessor, IUserDAL userDAL, IUnitDAL unitDAL, IAuthProvider authProvider, IServiceRequestDAL serviceRequestDAL,
            IPaymentDAL paymentDAL) : 
            base(applicationDAL, propertyDAL, contextAccessor, userDAL, unitDAL, authProvider, serviceRequestDAL, paymentDAL)
        {

        }

        [AuthorizationFilter("owner", "manager")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Property()
        {
            Property property = new Property();
            return View(property);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Property(Property property)
        {
            propertyDAL.AddProperty(property);

            return RedirectToAction("Unit");
        }

        [HttpGet]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Unit()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Unit(Unit unit)
        {
            unitDAL.AddUnit(unit);

            return RedirectToAction("Unit");
        }

        [HttpGet]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Review()
        {
            List<Application> applications = applicationDAL.GetAllUnreviewedApplications();

            return View(applications);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Approve(int applicationID)
        {
                applicationDAL.ApproveApplication(applicationID);

                return RedirectToAction("Review");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Deny(int applicationID)
        {
            applicationDAL.DenyApplication(applicationID);

            return RedirectToAction("Review");
        }

        [HttpGet]
        [AuthorizationFilter("owner", "manager")]
        public IActionResult Statistics()
        {
            //TODO: Implement get owner's ID
            User user = GetCurrentUser();
            int currentOwnerID = user.UserID;

            OwnersPropertiesViewModel statisticsForOwnerProperties = new OwnersPropertiesViewModel();

            statisticsForOwnerProperties.CurrentOwnerProperties = propertyDAL.GetPropertiesForOwner(currentOwnerID);

            return View(statisticsForOwnerProperties);
        }
    }
}