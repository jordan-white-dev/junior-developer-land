using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IUnitDAL
    {
        bool AddUnit(Unit unit);
        List<Unit> GetAvailableUnitsAtProperty(int propertyID);
        List<Unit> GetAllUnitsAtProperty(int propertyID);
    }
}
