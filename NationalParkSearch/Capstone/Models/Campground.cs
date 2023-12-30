using System;
using System.Collections.Generic;
using System.Text;
using Capstone;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampID { get; set; }
        public int ParkID { get; set; }
        public string Name { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public decimal DailyFee { get; set; }

        public override string ToString()
        {
            return @"#" + CampID + " " + FromMonth + " " + ToMonth + " " + string.Format("c2", DailyFee);
        }
    }
}
