using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ServiceUserRegister ServiceUsers = new ServiceUserRegister();

        [HttpGet]
        public async Task<IActionResult> GetSystemUsers()
        {
            var users = await ServiceUsers.GetCurrentUsers();

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser( string id)
        {
            if (id.Equals(""))
            {
                return BadRequest("ocurrio un error al ingresar el id");
            }
            else
            {
                await ServiceUsers.DeleteUser(id);

                return Ok("Se la eliminado el usuario: " + id + " de la base de datos!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegisterModel UserUpdated)
        {
            if (UserUpdated != null)
            {
                await ServiceUsers.UpdateUser(UserUpdated);

                return Ok("El usuario: "+ UserUpdated.Name+ " ha sido actualizado en la base de datos");
            }
            else
            {
                return BadRequest("Ocurrio un error al actualizar el usuario: "+UserUpdated.Name);
            }
        }
    }
}
