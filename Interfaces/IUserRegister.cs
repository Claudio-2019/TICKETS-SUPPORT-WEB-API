using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface IUserRegister
    {
        Task CreateUserAccount(UserRegisterModel UserData);
        Task CreateAdminAccount(UserAdminRegister AdminData);
        Task<List<UserRegisterModel>> GetCurrentUsers();
        Task<List<UserAdminRegister>> GetCurrentAdmin();

        Task DeleteUser(string _id);

        Task UpdateUser(UserRegisterModel update);
     
    }
}
