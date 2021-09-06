using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.LicenseServices;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        private readonly LicensesServices licensesServices = new LicensesServices();

        [HttpGet]
        public async Task<IActionResult> GetLicenses()
        {
            return Ok(await licensesServices.GetCurrentLicenses());
        }
        [HttpPost]
        public async Task<IActionResult> Licenses([FromBody] LicensesModel license)
        {
            if (license != null)
            {
                await licensesServices.CreateLicense(license);

                return Ok("SE HA CREADO LA LICENCIA: "+license.LicenseName);
            }
            else
            {
                return BadRequest("EL OBJETO A INSERTAR ES INVALIDO");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenses(string id)
        {
            if (id != null)
            {
                await licensesServices.DeleteLicense(id);
                return Ok("LA LICENCIA: " + id + " SE HA ELIMINADO");
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL VALIDAR EL ID O ES NULO");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutLicenses([FromBody] LicensesModel updateLicense)
        {
            if (updateLicense != null)
            {
                await licensesServices.UpdateLicenses(updateLicense._id, updateLicense);

                return Ok("SE ACTUALIZO LA LICENCIA: "+updateLicense.LicenseName);
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL ACTUALIZAR LA LICENCIA: " + updateLicense.LicenseName);
            }
        }
    }
}
