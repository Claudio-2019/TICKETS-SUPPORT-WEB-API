using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        

    }
}
