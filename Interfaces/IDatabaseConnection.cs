using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface IDatabaseConnection
    {
        string GetConnectionString();
    }
}
