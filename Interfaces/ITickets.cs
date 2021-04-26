using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface ITickets
    {
        Task CreateTicket(TicketRequest newTickets);
        Task<List<TicketRequest>> GetCurrentTickets();
        Task UpdateTicket(TicketRequest updateTicket);
        Task DeleteTicket(string id);
    }
}
