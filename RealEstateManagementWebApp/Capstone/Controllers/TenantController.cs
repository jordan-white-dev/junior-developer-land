using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    public class TenantController : HomeController
    {

        public TenantController(IApplicationDAL applicationDAL, IPropertyDAL propertyDAL, IHttpContextAccessor contextAccessor, IUserDAL userDAL, IUnitDAL unitDAL, IAuthProvider authProvider, IServiceRequestDAL serviceRequestDAL,
            IPaymentDAL paymentDAL)
            : base( applicationDAL,  propertyDAL,  contextAccessor,  userDAL,  unitDAL,  authProvider, serviceRequestDAL, paymentDAL)
        {

        }
        [AuthorizationFilter("tenant", "manager")]
        public new IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AuthorizationFilter("tenant", "manager")]
        public IActionResult Submit()
        {
            ServiceRequest serviceRequest = new ServiceRequest();
            return View(serviceRequest);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Submit(ServiceRequest serviceRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceRequest);
            }
            else
            {
                serviceRequestDAL.AddServiceRequest(serviceRequest);

                return RedirectToAction("Submit");
            }
        }

        [HttpGet]
        [AuthorizationFilter("tenant", "manager")]
        public IActionResult Rent()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Rent(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View(payment);
            }
            else
            {
                paymentDAL.SubmitPayment(payment);

                return RedirectToAction("Rent");
            }
        }
    }
}