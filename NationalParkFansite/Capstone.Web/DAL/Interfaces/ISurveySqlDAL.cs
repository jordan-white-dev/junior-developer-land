using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Interfaces
{
    public interface ISurveySqlDAL
    {
        Dictionary<string, int> GetFavorites();
        bool AddSurveyToDatabase(Survey survey);
    }
}
