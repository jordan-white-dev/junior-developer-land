using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Capstone.DAL.Interfaces;
using Capstone.Providers.Auth;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        protected IApplicationDAL applicationDAL;
        protected IPropertyDAL propertyDAL;
        protected IUnitDAL unitDAL;
        protected readonly IUserDAL userDAL;
        protected IServiceRequestDAL serviceRequestDAL;
        protected IPaymentDAL paymentDAL;
        protected readonly IAuthProvider authProvider;
        protected readonly IHttpContextAccessor contextAccessor;
        public static string SessionKey = "Auth_User";

        public HomeController(IApplicationDAL applicationDAL, IPropertyDAL propertyDAL, IHttpContextAccessor contextAccessor,
            IUserDAL userDAL, IUnitDAL unitDAL, IAuthProvider authProvider, IServiceRequestDAL serviceRequestDAL,
            IPaymentDAL paymentDAL)
        {
            this.applicationDAL = applicationDAL;
            this.contextAccessor = contextAccessor;
            this.userDAL = userDAL;
            this.authProvider = authProvider;
            this.propertyDAL = propertyDAL;
            this.unitDAL = unitDAL;
            this.serviceRequestDAL = serviceRequestDAL;
            this.paymentDAL = paymentDAL;
        }

        public IActionResult Home()
        {
            if (IsLoggedIn)
            {

            }
            return View();
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Apply(Application application)
        {
            if (!ModelState.IsValid)
            {
                return View(application);
            }
            else
            {
                applicationDAL.AddApplication(application);

                return RedirectToAction("Apply");
            }
        }

        [HttpGet]
        public IActionResult Search()
        {
            SearchViewModel model = new SearchViewModel();
            model.CurrentAvailableProperties = propertyDAL.GetAvailableProperties();

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            model.CurrentAvailableProperties = propertyDAL.GetAvailableProperties();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                model.AdvancedPropertySearch();
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //ISession Session => contextAccessor.HttpContext.Session;

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool IsLoggedIn
        {
            get
            {
                bool result = false;
                result = !String.IsNullOrEmpty(HttpContext.Session.GetString(SessionKey));
                return result;
            }
        }

        /// <summary>
        /// Signs the user in and saves their username in session.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SignIn(string emailaddress, string password)
        {
            var user = userDAL.GetUser(emailaddress);
            var hashProvider = new HashProvider();

            if (user != null && hashProvider.VerifyPasswordMatch(user.Password, password, user.Salt))
            {
                HttpContext.Session.SetString(SessionKey, user.EmailAddress);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Logs the user out by clearing their session data.
        /// </summary>
        public void LogOff()
        {
            HttpContext.Session.Clear();
        }

        /// <summary>
        /// Gets the user using the current username in session.
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            var username = HttpContext.Session.GetString(SessionKey);

            if (!String.IsNullOrEmpty(username))
            {
                return userDAL.GetUser(username);
            }

            return null;
        }
    }
}
