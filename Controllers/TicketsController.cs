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
    public class TicketsController : ControllerBase
    {
        private ServiceTickets TicketsService = new ServiceTickets();

        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] TicketRequestModel ticket)
        {

            if (ticket != null)
            {
                await TicketsService.CreateTicket(ticket);

                return Ok("SE HA GENERADO EL TICKET: "+ticket.TicketNumber+"SERA ATENDIDO EN LA PRO NTO POSIBLE DEPENDIENDO DE LA COLA DE TICKETS");
            }
            else if(ticket.Name == "" || ticket.Email == "" || ticket.Details == "")
            {
                return BadRequest("NO HAY DATOS SUFICIENTES PARA ABRIR ESTE TICKET!");

            }
            else
            {
                return NotFound("OCURRIO UN PROBLEMA AL CONECTARSE AL SERVIDOR O AL GENERAR EL TICKET, INTENTELO NUEVAMENTE");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await TicketsService.GetCurrentTickets());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTicket([FromBody] TicketRequestModel id)
        {
            if (id.Equals(""))

            {
                return BadRequest("EL ID DEL PRODUCTO NO ES VALIDO, VERIFIQUE LA INFORMACION CON EL ADMINISTRADOR");
            }
            else
            {
                await TicketsService.DeleteTicket(id._id);

                return Ok("EL TICKET: " + id.TicketNumber + " HA SIDO ELIMINADO!");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketRequestModel updatedTicket)
        {
            if (updatedTicket == null || updatedTicket.Equals(""))
            {
                return BadRequest("PRODUCTO NO VALIDO, VERIFICAR INFORMACION");
            }
            else
            {
                await TicketsService.UpdateTicket(updatedTicket._id, updatedTicket);

                return Ok("EL CAMBIO FUE EXITOSO");
            }
        }
    }
}
