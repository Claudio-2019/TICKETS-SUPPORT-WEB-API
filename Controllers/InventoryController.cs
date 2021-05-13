using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.InventoryService;

namespace WEB_API_TICKETS_SUPPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private InventoryServices inventoryServices = new InventoryServices();

        [HttpGet]
        public async Task<IActionResult> GetComputersInventory()
        {
            var computers = await inventoryServices.GetInventoryComputers();

            return Ok(computers);
        }
        [HttpGet("{CATEGORY}")]
        public async Task<IActionResult> GetInventorySelected(string Category)
        {
            List<InventoryItemModel> CategoryInventory = new List<InventoryItemModel>();
            if (Category == null || Category == "")
            {
                return BadRequest("LA CATEGORIA NO FUE SELECCIONADA O NO FUE ENCONTRADA!");
            }
            else
            {
                var FilterInventory = await inventoryServices.GetInventorySelected(Category);

                foreach (InventoryItemModel item in FilterInventory)
                {
                    CategoryInventory.Add(new InventoryItemModel
                    {
                        _id = item._id,
                       Code = item.Code,
                       Quantity = item.Quantity,
                       Tag = item.Tag,
                       Brand = item.Brand,
                       RoomLocation = item.RoomLocation,
                       Category = item.Category,
                       CurrentStatus = item.CurrentStatus
                    });
                }

                return Ok(CategoryInventory);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostItemInventory([FromBody] InventoryItemModel item)
        {
            if (item == null)
            {
                return NoContent();
            }
            else
            {
                await inventoryServices.InsertItemToInventory(item);

                return Ok("EL EQUIPO: "+item.Tag+" FUE INSERTADO EN LA BASE DE DATOS");
            }
        }
    }
}
