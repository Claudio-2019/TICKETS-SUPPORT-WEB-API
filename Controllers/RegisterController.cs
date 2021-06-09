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
            else if (User != null && user.Role == "Approve")
            {
                CurrentRegistrationModel UserData = new CurrentRegistrationModel()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Pass = user.Pass,
                    Role = "User"
                };
                await systemRegister.ApproveUser(UserData);

                return Ok("Se ha registrado el cliente: " + UserData.Name + "como usuario del sistema");
            }
            else
            {
                return BadRequest("Ocurrio un error al registrar el usuario");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetRegistrations()
        {
            return Ok(await systemRegister.GetCurrentRegistrations());
        }
    }
}
