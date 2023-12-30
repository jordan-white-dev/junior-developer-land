using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkWeatherViewModel
    {
        public Park Park { get; set; }

        public List<Weather> Weather { get; set; }

        public string TempType { get; set; }
    }
}
