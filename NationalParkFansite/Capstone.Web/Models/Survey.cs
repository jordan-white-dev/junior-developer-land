using Capstone.Web.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {
        [Required]
        [Display(Name = "Favorite National Park: ")]
        public string ParkCode { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        [Display(Name = "Your email: ")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "State of residence: ")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Activity level: ")]
        public string ActivityLevel { get; set; }

        public static IList<SelectListItem> statesList = new List<SelectListItem>()
        {
        new SelectListItem() { Text="AL", Value="Alabama"},
        new SelectListItem() { Text="AK", Value="Alaska"},
        new SelectListItem() { Text="AZ", Value="Arizona"},
        new SelectListItem() { Text="AR", Value="Arkansas"},
        new SelectListItem() { Text="CA", Value="California"},
        new SelectListItem() { Text="CO", Value="Colorado"},
        new SelectListItem() { Text="CT", Value="Connecticut"},
        new SelectListItem() { Text="DC", Value="District of Columbia"},
        new SelectListItem() { Text="DE", Value="Delaware"},
        new SelectListItem() { Text="FL", Value="Florida"},
        new SelectListItem() { Text="GA", Value="Georgia"},
        new SelectListItem() { Text="HI", Value="Hawaii"},
        new SelectListItem() { Text="ID", Value="Idaho"},
        new SelectListItem() { Text="IL", Value="Illinois"},
        new SelectListItem() { Text="IN", Value="Indiana"},
        new SelectListItem() { Text="IA", Value="Iowa"},
        new SelectListItem() { Text="KS", Value="Kansas"},
        new SelectListItem() { Text="KY", Value="Kentucky"},
        new SelectListItem() { Text="LA", Value="Louisiana"},
        new SelectListItem() { Text="ME", Value="Maine"},
        new SelectListItem() { Text="MD", Value="Maryland"},
        new SelectListItem() { Text="MA", Value="Massachusetts"},
        new SelectListItem() { Text="MI", Value="Michigan"},
        new SelectListItem() { Text="MN", Value="Minnesota"},
        new SelectListItem() { Text="MS", Value="Mississippi"},
        new SelectListItem() { Text="MO", Value="Missouri"},
        new SelectListItem() { Text="MT", Value="Montana"},
        new SelectListItem() { Text="NE", Value="Nebraska"},
        new SelectListItem() { Text="NV", Value="Nevada"},
        new SelectListItem() { Text="NH", Value="New Hampshire"},
        new SelectListItem() { Text="NJ", Value="New Jersey"},
        new SelectListItem() { Text="NM", Value="New Mexico"},
        new SelectListItem() { Text="NY", Value="New York"},
        new SelectListItem() { Text="NC", Value="North Carolina"},
        new SelectListItem() { Text="ND", Value="North Dakota"},
        new SelectListItem() { Text="OH", Value="Ohio"},
        new SelectListItem() { Text="OK", Value="Oklahoma"},
        new SelectListItem() { Text="OR", Value="Oregon"},
        new SelectListItem() { Text="PA", Value="Pennsylvania"},
        new SelectListItem() { Text="PR", Value="Puerto Rico"},
        new SelectListItem() { Text="RI", Value="Rhode Island"},
        new SelectListItem() { Text="SC", Value="South Carolina"},
        new SelectListItem() { Text="SD", Value="South Dakota"},
        new SelectListItem() { Text="TN", Value="Tennessee"},
        new SelectListItem() { Text="TX", Value="Texas"},
        new SelectListItem() { Text="UT", Value="Utah"},
        new SelectListItem() { Text="VT", Value="Vermont"},
        new SelectListItem() { Text="VA", Value="Virginia"},
        new SelectListItem() { Text="WA", Value="Washington"},
        new SelectListItem() { Text="WV", Value="West Virginia"},
        new SelectListItem() { Text="WI", Value="Wisconsin"},
        new SelectListItem() { Text="WY", Value="Wyoming"}
        };

    }
}
