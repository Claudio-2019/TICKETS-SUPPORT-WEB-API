using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface IEmails
    {
        Task SendEmail(EmailMessageModel message, string email, string pass);

        Task<List<EmailMessageModel>> GetEmails();
    }
}
