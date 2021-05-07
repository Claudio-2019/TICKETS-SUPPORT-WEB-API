using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.ContactsServices;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ContactService serviceContact = new ContactService();

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(serviceContact.GetContacts().Result.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactModel contact)
        {
            if (contact != null)
            {
                await serviceContact.AddContact(contact);

                return Ok("EL CONTACTO HA SIDO AGREGADO A LA BASE DE DATOS");
            }
            else
            {
                return BadRequest("LA INFORMACION DEL CONTACTO EN INVALIDA, INTENTELO DE NUEVO!!");
            }
        }
    }
}
