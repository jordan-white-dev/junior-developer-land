using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        public int CampgroundID { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupants { get; set; }
        public bool Accessible { get; set; }
        public bool Utilities { get; set; }
        public int MaxRVLength { get; set; }

        public override string ToString()
        {
            string isAccessible = Accessible ? "Yes" : "No";
            string hasUtilities = Utilities ? "Yes" : "No";
            string rvLengthString = MaxRVLength == 0 ? "N/A" : MaxRVLength.ToString();
            return SiteNumber.ToString().PadRight(12) + MaxOccupants.ToString().PadRight(14) + isAccessible.PadRight(15) + rvLengthString.PadRight(17) + hasUtilities.PadRight(11);
        }
    }
}
