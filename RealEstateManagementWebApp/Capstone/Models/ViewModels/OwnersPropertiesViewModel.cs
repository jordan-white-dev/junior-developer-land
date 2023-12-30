using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModels
{
    public class OwnersPropertiesViewModel
    {
        public List<Property> CurrentOwnerProperties { get; set; }

        public decimal AverageBaseRentAllUnits()
        {
            decimal result = 0.0M;
            int numberOfUnits = 0;

            foreach (Property property in CurrentOwnerProperties)
            {
                foreach (Unit unit in property.UnitsAtThisProperty)
                {
                    result += unit.MonthlyRent;
                    numberOfUnits++;
                }
            }

            return result /= numberOfUnits;
        }

        // TODO: Unsure how to calculate this just yet. Currently we have no lease details or loan payments
        public decimal AverageVacancyForAllUnits()
        {
            decimal result = 0.0M;
            int numberOfUnits = 0;

            foreach (Property property in CurrentOwnerProperties)
            {
                result += property.GetVacancyRate();
                numberOfUnits += property.UnitsAtThisProperty.Count;
            }

            return Math.Round(result / numberOfUnits, 2);
        }

        public decimal SumRentCollectedYTD()
        {
            decimal result = 0.0M;

            foreach(Property property in CurrentOwnerProperties)
            {
                foreach (Unit unit in property.UnitsAtThisProperty)
                {
                    result += unit.RentCollectedYTD;
                }
            }

            return result;
        }
    }
}