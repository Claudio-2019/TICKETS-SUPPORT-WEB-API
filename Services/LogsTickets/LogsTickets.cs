using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services.LogsTickets
{
    public class LogsTickets : ILogsTickets
    {
        internal DatabaseConnection connection = new DatabaseConnection();

        private readonly IMongoCollection<LogTicketModel> CollectionLogsTickets;

        private readonly HttpContext ServerFiles;

        public LogsTickets()
        {
            CollectionLogsTickets = connection.database.GetCollection<LogTicketModel>("TicketsHistoryLog");
        }

        public async Task CreateLogTicket(LogTicketModel newLogTicket)
        {
            await CollectionLogsTickets.InsertOneAsync(newLogTicket);

    //        await Task.Run(() =>
    //        {

    //            MailMessage notificacion = new MailMessage();
    //            SmtpClient servicioSMTP = new SmtpClient();
    //            notificacion.From = new MailAddress("cgonzalez@mbs.ed.cr", "TICKET STATUS");
    //            notificacion.To.Add(new MailAddress(newLogTicket.EmailToNotifitication));
    //            notificacion.Subject = "THE TICKET " + newLogTicket.TicketNumber + "HAS BEEN CLOSED BY THE ADMINISTRATOR";
    //            notificacion.IsBodyHtml = true;

    //            notificacion.Body =
    //            "< body >"+
    //"< div style = 'border-style: solid; border-color: black;' >"+


    //     "< div style = 'margin-left: 0.5cm;' >"+


    //          "< h3 > YOUR TICKET HAS BEEN RESOLVED</ h3 >"+


    //         </ div >+


    //         < hr >+


    //         < div style = "margin-left: 0.5cm; width: 100%;" >+


    //              < style >
    //                  table,
    //            th,
    //            td {
    //            border: 1px solid black;
    //                border - collapse: collapse;
    //            }

    //            td {
    //                text - align: center;
    //            }
    //        </ style >+
    //        < table >+
    //            < thead >+
    //                < th ># Ticket</th>+
    //                < th > Tipo de problema</ th >+

                
    //                   < th > Detalles de solucion</ th >+



    //                  </ thead >+


    //                  < tbody >+


    //                      < tr >+


    //                          < td > test </ td >


    //                          < td > test </ td >


    //                          < td > test </ td >


    //                      </ tr >



    //                  </ tbody >


    //              </ table >


    //          </ div >


    //          < br >


    //      </ div >
    //  </ body >

    //                  servicioSMTP.Port = 587;
    //            servicioSMTP.Host = "smtp.gmail.com";
    //            servicioSMTP.EnableSsl = true;
    //            servicioSMTP.UseDefaultCredentials = false;
    //            servicioSMTP.Credentials = new NetworkCredential("cgonzalez@mbs.ed.cr", "IT.s0p0rt3.MBS1");
    //            servicioSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            servicioSMTP.Send(notificacion);

    //        });
        }

        public async Task<List<LogTicketModel>> GetCurrentTicketsLogs()
        {
            return await CollectionLogsTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
