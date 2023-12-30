using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "A unit ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Unit ID: ")]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "A tenant ID is required")]
        [Display(Name = "Tenant ID: ")]
        public int TenantID { get; set; }

        [Required(ErrorMessage = "Payment amount is required")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Payment Amount: ")]
        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Please enter a payment month")]
        [Display(Name = "Choose the month being paid: ")]
        public int PaymentForMonth { get; set; }

        //public IList<SelectListItem> MonthsList = new List<SelectListItem>()
        //{
        //new SelectListItem() { Text="January", Value="1"},
        //new SelectListItem() { Text="February", Value="2"},
        //new SelectListItem() { Text="March", Value="3"},
        //new SelectListItem() { Text="April", Value="4"},
        //new SelectListItem() { Text="May", Value="5"},
        //new SelectListItem() { Text="June", Value="6"},
        //new SelectListItem() { Text="July", Value="7"},
        //new SelectListItem() { Text="August", Value="8"},
        //new SelectListItem() { Text="September", Value="9"},
        //new SelectListItem() { Text="October", Value="10"},
        //new SelectListItem() { Text="November", Value="11"},
        //new SelectListItem() { Text="December", Value="12"}
        //};
    }
}
