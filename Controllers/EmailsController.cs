using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.EmailService;
namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private EmailServices ServiceEmail = new EmailServices();
        private IConfiguration configuration;
        private string SystemEmail;
        private string SystemPass;

        public EmailsController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmailCredentials()
        {
            return Ok(await ServiceEmail.GetEmails());
        }
       
        [HttpPost]
        public async Task<IActionResult> PostNewEmail([FromBody] EmailMessageModel message)
        {
            SystemEmail = configuration.GetSection("EmailService").GetSection("CorreoElectronico").Value;
            SystemPass = configuration.GetSection("EmailService").GetSection("Password").Value;

            if (message == null)
            {
                return BadRequest("OCURRIO UN ERROR AL ENVIAR EL CORREO ELECTRONICO, EL MENSAJE NO ES VALIDO");
            }
            else
            {
                await ServiceEmail.SendEmail(message, SystemEmail, SystemPass);

                return Ok("CORREO ELECTRONICO ENVIADO AL USUARIO: " + message.EmailAddress);
            }
        }
    }
}
