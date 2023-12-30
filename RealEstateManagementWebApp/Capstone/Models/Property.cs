using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Property
    {
        public int PropertyID { get; set; }

        [Required(ErrorMessage = "An owner ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Owner ID:")]
        public int OwnerID { get; set; }

        [Required(ErrorMessage = "A manager ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Manager ID:")]
        public int ManagerID { get; set; }

        [Required(ErrorMessage = "A property name is required")]
        [Display(Name = "Property Name:")]
        public string PropertyName { get; set; }

        [Display(Name = "Choose a property type:")]
        public string PropertyType { get; set; }

        [Required(ErrorMessage = "Number of units is required")]
        [Range(1, 100, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "# of Units:")]
        public int NumberOfUnits { get; set; }

        [DataType(DataType.ImageUrl, ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Add a link to an image of your property: ")]
        public string ImageSource { get; set; } = "";

        public List<Unit> UnitsAtThisProperty { get; set; }

        public IList<SelectListItem> PropertyTypesList = new List<SelectListItem>()
        {
        new SelectListItem() { Text="Single Family"},
        new SelectListItem() { Text="Duplex"},
        new SelectListItem() { Text="Triplex"},
        new SelectListItem() { Text="Fourplex"},
        new SelectListItem() { Text="Multi-Family"},
        new SelectListItem() { Text="Condo"},
        new SelectListItem() { Text="Mobile Home"}
        };

        // Get Vacancy % at property(?)
        public decimal GetVacancyRate()
        {
            int vacantCount = 0;

            foreach (var unit in UnitsAtThisProperty)
            {
                if(unit.IsVacant)
                {
                    vacantCount++;
                }
            }

            return vacantCount / UnitsAtThisProperty.Count;
        }

        public decimal GetTotalScheduledGrossRents()
        {
            decimal result = 0.0M;

            // TODO: Consider this a placeholder. 
            // Would need to add actually get payments for a month, or account for additional fees.
            //Technically this would need all "Occupied Units" 
            foreach (Unit unit in UnitsAtThisProperty)
            {
                result += unit.MonthlyRent;
            }

            return result * 12;
        }

        public decimal GetVacancyLoss()
        {
            return GetTotalScheduledGrossRents() * GetVacancyRate();
        }

        public decimal GetEffectiveGrossIncome()
        {
            return GetTotalScheduledGrossRents() - GetVacancyLoss();
        }

        // Get Potential Fees for occupied (though this would need a call to applications to get occupants)
    }
}
