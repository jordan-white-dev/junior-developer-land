using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyViewModel
    {
        public List<Park> Parks { get; set; }

        public Dictionary<string, int> Surveys { get; set; }
    }
}
