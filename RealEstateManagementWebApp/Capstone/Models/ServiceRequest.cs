using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ServiceRequest
    {
        public int RequestID { get; set; }

        [Required(ErrorMessage = "A tenant ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Tenant ID: ")]
        public int TenantID { get; set; }

        [Required(ErrorMessage = "A description is required")]
        [Display(Name = "Description: ")]
        public string Description { get; set; }

        [Display(Name = "Check if this is an emergency")]
        public bool IsEmergency { get; set; }

        [Required(ErrorMessage = "A category is required")]
        [Display(Name = "Please choose a service category: ")]
        public string Category { get; set; }

        public bool IsCompleted { get; set; }

        public IList<SelectListItem> CategoriesList = new List<SelectListItem>()
        {
        new SelectListItem() { Text="Plumbing"},
        new SelectListItem() { Text="Electrical"},
        new SelectListItem() { Text="Air Conditioning"},
        new SelectListItem() { Text="Appliances"},
        new SelectListItem() { Text="Other"}
        };
    }
}
