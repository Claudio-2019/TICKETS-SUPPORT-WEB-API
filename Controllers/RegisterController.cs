using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services;
using WEB_API_TICKETS_SUPPORT.Services.SystemRegistrations;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private ServiceUserRegister RegisterUserService = new ServiceUserRegister();
        private ServiceRegistrations systemRegister = new ServiceRegistrations();

        [HttpPost]
        public async Task<IActionResult> PostNewUser([FromBody] UserRegisterModel user)
        {

            if (user != null && user.Role.Contains("User"))
            {
                await RegisterUserService.CreateUserAccount(user);

                return Ok("Se ha registrado el Usuario: " + user.Name);
            }
            else if (User != null && user.Role == "Admin")
            {
                UserAdminRegisterModel AdminData = new UserAdminRegisterModel()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Pass = user.Pass,
                    Role = user.Role
                };
                await RegisterUserService.CreateAdminAccount(AdminData);

                return Ok("Se ha registrado el Usuario: " + AdminData.Name + "como administrador del sistema");
            }
            else
            {
                return BadRequest("Ocurrio un error al registrar el usuario");
            }

        }
    }
}
