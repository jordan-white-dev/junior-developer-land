using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Unit
    {
        public int UnitID { get; set; }

        [Required(ErrorMessage = "A property ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Property ID:")]
        public int PropertyID { get; set; }

        public int TenantID { get; set; }

        [Required(ErrorMessage = "Monthly rent is required")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Monthly Rent:")]
        public decimal MonthlyRent { get; set; }

        [Required(ErrorMessage = "Square feet is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Square Feet:")]
        public int SquareFeet { get; set; }

        [Required(ErrorMessage = "Number of bedrooms is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "# of Bedrooms:")]
        public int NumberOfBeds { get; set; }

        [Required(ErrorMessage = "Number of baths is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "# of Bathrooms:")]
        public double NumberOfBaths { get; set; }

        [Required(ErrorMessage = "A description is required")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Display(Name = "Tagline:")]
        public string Tagline { get; set; }

        [DataType(DataType.ImageUrl, ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Add a link to an image:")]
        public string ImageSource { get; set; }

        [Required(ErrorMessage = "Application fee is required")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Application Fee:")]
        public decimal ApplicationFee { get; set; }

        [Required(ErrorMessage = "Security deposit is required")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Security Deposit:")]
        public decimal SecurityDeposit { get; set; }

        [Required(ErrorMessage = "Pet Deposit is required")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Pet Deposit:")]
        public decimal PetDeposit { get; set; }

        [Required(ErrorMessage = "An address is required")]
        [Display(Name = "Address Line 1:")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2:")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "A city is required")]
        [Display(Name = "City:")]
        public string City { get; set; }

        [Required(ErrorMessage = "A state is required")]
        [Display(Name = "State:")]
        public string State { get; set; }

        [Required(ErrorMessage = "A zip code is required")]
        [Display(Name = "Zip Code:")]
        public int ZipCode { get; set; }

        public bool WasherDryer { get; set; }

        public bool AllowCats { get; set; }

        public bool AllowDogs { get; set; }

        [Required(ErrorMessage = "A description of the parking situation is required")]
        [Display(Name = "Parking:")]
        public string ParkingSpots { get; set; }

        public bool Gym { get; set; }

        public bool Pool { get; set; }

        public bool IsVacant
        {
            get
            {
                // TenantID will default to 0 when unoccupied
                return (TenantID == 0);
            }
        }

        public decimal RentCollectedYTD { get; set; }
    }
}
