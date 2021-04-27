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
        Task CreateAdminAccount(UserAdminRegisterModel AdminData);
        Task<List<UserRegisterModel>> GetCurrentUsers();
        Task<List<UserAdminRegisterModel>> GetCurrentAdmin();

        Task DeleteUser(string _id);

        Task UpdateUser(UserRegisterModel update);

        Task<List<UserRegisterModel>> GetCurrentSessionUser(string email);
        Task<List<UserAdminRegisterModel>> GetCurrentSessionAdministrator(string email);

    }
}
