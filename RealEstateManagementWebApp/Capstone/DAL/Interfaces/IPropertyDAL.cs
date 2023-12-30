using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IPropertyDAL
    {
        bool AddProperty(Property property);
        //List<Property> GetAllProperties();
        List<Property> GetAvailableProperties();
        List<Property> GetPropertiesForOwner(int ownerID);
    }
}
