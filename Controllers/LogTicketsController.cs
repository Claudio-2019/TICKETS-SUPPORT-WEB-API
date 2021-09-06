using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services;
using WEB_API_TICKETS_SUPPORT.Services.LogsTickets;
using WEB_API_TICKETS_SUPPORT.Utilities;
using Wkhtmltopdf.NetCore;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTicketsController : ControllerBase
    {
        private LogsTickets LogsTicketsService = new LogsTickets();
        private ServiceTickets ticketsService = new ServiceTickets();

        readonly IGeneratePdf _CreatePdfReport;
       

        [HttpPost]
        public async Task<IActionResult> SaveTicketHistory([FromBody] LogTicketModel record) 
        {
            if (record != null)
            {
                await ticketsService.DeleteTicket(record._id);

                record._id = "";

                await LogsTicketsService.CreateLogTicket(record);

           
                return Ok("SE GUARDO EL REGISTRO DEL TICKET: "+record.TicketNumber);
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL INSERTA LA INFORMACION AL REGISTRO");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLogsTickets()
        {
            return Ok(await LogsTicketsService.GetCurrentTicketsLogs());
        }
      
    }
}
