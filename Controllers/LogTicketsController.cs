using DinkToPdf;
using DinkToPdf.Contracts;
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

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTicketsController : ControllerBase
    {
        private LogsTickets LogsTicketsService = new LogsTickets();
        private ServiceTickets ticketsService = new ServiceTickets();
        private readonly IConverter pdfConverter;

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

        [HttpGet]
        [Route("LogTickets/DownloadHistoryTickets")]
        public async Task<IActionResult> DownloadHistoryTickets()
        {
            try
            {
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    //Out = @"D:\PDFCreator\Employee_Report.pdf"  USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    //HtmlContent = TemplateGenerator.GetHTMLString(),
                    Page = "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                //_converter.Convert(pdf); IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

                var file = pdfConverter.Convert(pdf);

                //return Ok("Successfully created PDF document.");
                //return File(file, "application/pdf", "EmployeeReport.pdf");
                return File(file, "application/pdf");
            }
            catch ( Exception error)
            {

                return BadRequest(error.Message);
            }
        }

    }
}
