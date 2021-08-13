using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
        private readonly ServiceTickets TicketsService = new ServiceTickets();

        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] TicketRequestModel ticket)
        {

            if (ticket != null)
            {
                await TicketsService.CreateTicket(ticket);

                return Ok("SE HA GENERADO EL TICKET: " + ticket.TicketNumber + "SERA ATENDIDO EN LO MAS PRONTO POSIBLE DEPENDIENDO DE LA COLA DE TICKETS");
            }
            else if (ticket.Name == "" || ticket.Email == "" || ticket.Details == "")
            {
                return BadRequest("NO HAY DATOS SUFICIENTES PARA ABRIR ESTE TICKET!");

            }
            else
            {
                return NotFound("OCURRIO UN PROBLEMA AL CONECTARSE AL SERVIDOR O AL GENERAR EL TICKET, INTENTELO NUEVAMENTE");
            }

        }
      
        [HttpGet("{name}")]
        public async Task<IActionResult> GetClientTickets(string Name)
        {
            List<TicketRequestModel> TicketsFromUser = new List<TicketRequestModel>();

            if (Name.Equals(""))
            {
                return BadRequest("OCURRIO UN ERROR AL CARGAR LO TICKETS POR EL NOMBRE");
            }
            else
            {
                var FilterTicket = TicketsService.GetUserProfileTickets(Name);

                foreach (TicketRequestModel item in FilterTicket.Result)
                {
                    TicketsFromUser.Add(new TicketRequestModel {
                        TicketNumber = item.TicketNumber,
                        TypeRequest = item.TypeRequest,
                        Details=item.Details});
                }

                return Ok(TicketsFromUser);
            }


        }
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await TicketsService.GetCurrentTickets());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (id.Equals(""))
            {
                return BadRequest("EL ID DEL PRODUCTO NO ES VALIDO, VERIFIQUE LA INFORMACION CON EL ADMINISTRADOR");
            }
            else
            {
                await TicketsService.DeleteTicket(id);


                return Ok("EL TICKET: " + id + " HA SIDO COMPLETADO!");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketRequestModel updatedTicket)
        {
            if (updatedTicket == null || updatedTicket.Equals(""))
            {
                return BadRequest("TICKET NO VALIDO, VERIFICAR INFORMACION");
            }
            else
            {
                await TicketsService.UpdateTicket(updatedTicket._id, updatedTicket);

                return Ok("EL CAMBIO FUE EXITOSO EN EL TICKET: "+updatedTicket.TicketNumber);
            }
        }
    }
}
