using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.Models.ViewModels.Account;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Http;
using Capstone.DAL.Interfaces;

namespace Capstone.Controllers
{

    public class AccountController : HomeController
    {


        public AccountController(IApplicationDAL applicationDAL, IPropertyDAL propertyDAL, IHttpContextAccessor contextAccessor, IUserDAL userDAL, IUnitDAL unitDAL, IAuthProvider authProvider, IServiceRequestDAL serviceRequestDAL,
            IPaymentDAL paymentDAL)
            : base(applicationDAL, propertyDAL, contextAccessor, userDAL, unitDAL, authProvider, serviceRequestDAL, paymentDAL)
        {

        }



        [AuthorizationFilter("manager", "owner", "tenant", "user")]
        [HttpGet]
        public IActionResult Index()
        {
            User user = GetCurrentUser();

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                bool validLogin = SignIn(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {
                    return RedirectToAction("Home", "Home");
                }
            }
            ModelState.Clear();
            loginViewModel = new LoginViewModel();
            loginViewModel.failedLogin = true;
            return View(loginViewModel);
        }

        public IActionResult LogOut()
        {
            LogOff();

            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Register(registerViewModel.Email, registerViewModel.Password, "user",
                    registerViewModel.PhoneNumber, registerViewModel.FirstName, registerViewModel.LastName) == false)
                {
                    return RedirectToAction("Error", "Home");
                }


                return RedirectToAction("Home", "Home");
            }
            ModelState.Clear();
            registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }


    }
}