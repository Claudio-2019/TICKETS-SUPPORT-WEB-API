using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface IContacts
    {
        Task AddContact(ContactModel contactInfo);

        Task<List<ContactModel>> GetContacts();
    }
}
