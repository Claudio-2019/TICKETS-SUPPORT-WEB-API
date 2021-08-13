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

            await Task.Run(() =>
            {

                MailMessage notificacion = new MailMessage();
                SmtpClient servicioSMTP = new SmtpClient();
                notificacion.From = new MailAddress("cgonzalez@mbs.ed.cr", "CURRENT TICKET STATUS");
                notificacion.To.Add(new MailAddress(newLogTicket.EmailToNotifitication));
                notificacion.Subject = "THE TICKET " + newLogTicket.TicketNumber + "HAS BEEN CLOSED BY THE ADMINISTRATOR";
                notificacion.IsBodyHtml = true;

                notificacion.Body = 

    "< style >"+
        "#MessageContainer {+"+
            "border - style: solid;"+
                "border - color: black;"+

            "}"+

        "#ContainerSolution {"+
            "margin - left: 50px;"+
            "margin - right: 50px;"+
        "}"+

        "hr {"+
            "color: black;"+
        "}"+

    "table {"+
            "width: 100%;"+
            "border: 1px solid black;"+
            "border-color: black;"+
            "border-style: solid;"+
            "border-collapse: collapse;"+
        "}"+

"td"+
"{"+
    "text - align: center;"+
"border: 1px solid black;"+
"}"+

"h2"+
"{"+
    "margin - left: 20px;"+
"}"+
".button {"+
            "display: block;"+
            "width: 500px;"+
            "height: 25px;"+
            "background: #29b330;"+
            "padding: 10px;"+
            "text - align: center;"+
            "border - radius: 5px;"+
            "color: white;"+
            "font - weight: bold;"+
            "line - height: 25px;"+
            "}"+
"}"+
    "</ style >"+

        "< div id=MessageContainer>"+
 

         "<h2> THIS TICKET "+newLogTicket.TicketNumber+" HAS BEEN CLOSED FOR: "+newLogTicket.Name+"</h2>"+
        "<hr>"+

        "<div id=ContainerSolution>"+

            "<table>"+

                "<thead>"+
                    "<tr>"+
                        "<th>TYPE OF SUPPORT</th>"+
                        "<th>DETAILS ABOUT THE PROBLEM</th>"+
                        "<th>SOLUTION REGISTERED</th>"+
                    "</tr>"+
                "</thead>"+
                "<tbody>"+
                    "<tr>"+
                        "<td>"+newLogTicket.TypeRequest+"</td>"+
                        "<td>+"+newLogTicket.Details+"</td>"+
                        "<td>"+newLogTicket.SolutionDetails+"</td>"+
                    "</tr>"+
                "</tbody>"+

           "</table>"+
        "</div>"+
        "<hr>"+
       " <a class='button' target='blank'"+ "href='https://ticketsmbssupport.netlify.app'>"+"IF YOU WANT TO SEND A NEW TICKET, CLICK HERE </ a > "+
    "</div>";

                servicioSMTP.Port = 587;
                servicioSMTP.Host = "smtp.gmail.com";
                servicioSMTP.EnableSsl = true;
                servicioSMTP.UseDefaultCredentials = false;
                servicioSMTP.Credentials = new NetworkCredential("cgonzalez@mbs.ed.cr", "IT.s0p0rt3.MBS1");
                servicioSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                servicioSMTP.Send(notificacion);

            });
        }

        public async Task<List<LogTicketModel>> GetCurrentTicketsLogs()
        {
            return await CollectionLogsTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
