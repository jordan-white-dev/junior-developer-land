using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.DAL.Interfaces
{
    public interface IUserDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        User GetUser(string emailAddress);

        bool CreateUser(User user);


        bool UpdateUser(User user);

        bool DeleteUser(User user);

        List<User> GetTenantUsers();

        List<SelectListItem> GetUserEmailSelectList();

    }
}
