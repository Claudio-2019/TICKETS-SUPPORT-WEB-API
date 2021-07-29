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

namespace WEB_API_TICKETS_SUPPORT.Services
{
    public class ServiceTickets : ITickets
    {

        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<TicketRequestModel> CollectionTickets;

        public ServiceTickets()
        {
            CollectionTickets = accessDB.database.GetCollection<TicketRequestModel>("CurrentTickets");
        }

        public async Task CreateTicket(TicketRequestModel newTickets)
        {
            await CollectionTickets.InsertOneAsync(newTickets);

            await Task.Run(() =>
            {

                MailMessage notificacion = new MailMessage();
                SmtpClient servicioSMTP = new SmtpClient();
                notificacion.From = new MailAddress("cgonzalez@mbs.ed.cr", "Technical Support");
                notificacion.To.Add(new MailAddress(newTickets.Email));
                notificacion.Subject = "Technical Support Request Notification: " + newTickets.TicketNumber;
                notificacion.IsBodyHtml = true;
                notificacion.Body =
                "<div style='border-style: solid; border-color: black';>" +
                "<h2 style='margin-left: 1cm;'>Your request for technical support has been received.</h2>" +
                "<hr style='margin-left: 0.5cm;'> " +
                "<h2 style='margin-left: 0.5cm;'>Details:</h2>" +
                "<ul>" +
                "<li> Ticket Number:" + newTickets.TicketNumber + "</li>" +
                "<hr>" +
                "<li> Request Details:" + "<br>" + newTickets.Details + "</li>" +
                "</ul>" +
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
      

        public async Task DeleteTicket(string id)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, id);

            await CollectionTickets.DeleteOneAsync(FiltroConsulta);
        }

        public async Task<List<TicketRequestModel>> GetCurrentTickets()
        {
            return await CollectionTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task UpdateTicket(string id, TicketRequestModel updateTicket)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, updateTicket._id);

            await CollectionTickets.ReplaceOneAsync(FiltroConsulta, updateTicket);
        }

        public async Task<List<TicketRequestModel>> GetUserProfileTickets(string nameClient)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq("Name", nameClient);

            return await CollectionTickets.FindAsync(FiltroConsulta).Result.ToListAsync();
        }
    }
}
