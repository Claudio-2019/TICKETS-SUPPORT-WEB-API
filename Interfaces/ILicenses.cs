using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    public interface ILicenses
    {
        Task CreateLicense(LicensesModel newLicense);
        Task<List<LicensesModel>> GetCurrentLicenses();
        Task UpdateLicenses(string id, LicensesModel updateLicense);
        Task DeleteLicense(string id);
    }
}
