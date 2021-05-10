using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface ILogsTickets
    {
        Task CreateLogTicket(LogTicketModel newLogTicket);
        Task<List<LogTicketModel>> GetCurrentTicketsLogs();
    }
}
