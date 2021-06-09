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

namespace WEB_API_TICKETS_SUPPORT.Services.SystemRegistrations
{
    public class ServiceRegistrations : ICurrentRegistrations
    {


        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<CurrentRegistrationModel> CollectionRegistrations;
        private readonly IMongoCollection<CurrentRegistrationModel> CollectionUsers;


        public ServiceRegistrations()
        {
            CollectionRegistrations = accessDB.database.GetCollection<CurrentRegistrationModel>("SystemRegistrations");

            CollectionUsers = accessDB.database.GetCollection<CurrentRegistrationModel>("CurrentUsers");

        }

        public async Task<List<CurrentRegistrationModel>> GetCurrentRegistrations()
        {
            return await CollectionRegistrations.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task ApproveUser(CurrentRegistrationModel newUser)
        {
            await CollectionUsers.InsertOneAsync(newUser);

            await Task.Run(() =>
            {

                MailMessage notificacion = new MailMessage();
                SmtpClient servicioSMTP = new SmtpClient();
                notificacion.From = new MailAddress("cgonzalez@mbs.ed.cr", "Account Registration Service Status");
                notificacion.To.Add(new MailAddress(newUser.Email));
                notificacion.Subject = "Account Registration " + newUser.Name;
                notificacion.IsBodyHtml = true;
                notificacion.Body =
                "<div style='border-style: solid; border-color: black';>" + "<h2 style='margin-left: 1cm;'>Your submission for get register has been approve!</h2>" + "<hr style='margin-left: 0.5cm;'> " + "<h2 style='margin-left: 0.5cm;'>Details:</h2>" + "<h4> YOUR ACCOUNT ITS ACTIVE IN OUR IT DEPARTMENT, PLEASE MAKE THE FOLLOW STEPS:</h4>" + "<br>" + "<ul>" + "" +
                "<li>Your Password would be: " +"<strong>" + newUser.Pass +"<strong>"+" (Remember to keep this password safe!) "+"(Only administrators can reset it in case of losing o forgot)"+ "</li>" +
                "<br>"+
                "<li>To get access now, please make click in this link: "+ "<a href='https://ticketsmbssupport.netlify.app'>Tickets Suport App<a>" + "</li>" +
                "<br>"+
                "<li>When you send a new ticket request, the system will send you a small notification to: "+newUser.Email+"<li>"+
                "<br>"+
                "<strong>If you have some questions about the system o a special need, please contact the system administrator in the app<string>"+
                "</ul>" +
                "<hr>"+
                "<p>Thank you!! for using our app! "+newUser.Name+"<p>"+
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

        public async Task RejectUser(string id)
        {
            var FiltroConsulta = Builders<CurrentRegistrationModel>.Filter.Eq(X => X._id, id);

            await CollectionRegistrations.DeleteOneAsync(FiltroConsulta);
        }
    }
}
