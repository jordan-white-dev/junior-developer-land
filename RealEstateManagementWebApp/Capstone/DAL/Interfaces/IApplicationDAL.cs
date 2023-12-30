using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IApplicationDAL
    {
        bool AddApplication(Application application);
        List<Application> GetAllUnreviewedApplications();
        bool ApproveApplication(int applicationID);
        bool DenyApplication(int applicationID);
        List<Application> GetAllApplications();
        //List<Application> GetAllApprovedApplications();
        //List<Application> GetAllDeniedApplications();
    }
}
