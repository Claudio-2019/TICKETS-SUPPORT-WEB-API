using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface ITickets
    {
        Task CreateTicket(TicketRequestModel newTickets);
        Task<List<TicketRequestModel>> GetCurrentTickets();
        Task UpdateTicket(string id, TicketRequestModel updateTicket);
        Task DeleteTicket(string id);
        Task<List<TicketRequestModel>> GetUserProfileTickets(string name);
    }
}
