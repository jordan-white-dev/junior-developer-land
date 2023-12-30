using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalCost { get; set; }


        public override string ToString()
        {
            return ReservationID.ToString().PadRight(16)  + SiteID.ToString().PadRight(9) + Name.PadRight(35) + FromDate.ToShortDateString().PadRight(14)  + ToDate.ToShortDateString();
        }
    }
}
