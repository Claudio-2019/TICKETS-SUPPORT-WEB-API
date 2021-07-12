using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Models;

namespace WEB_API_TICKETS_SUPPORT.Interfaces
{
    interface IInventoryItems
    {
        Task InsertItemToInventory(InventoryItemModel item);
        Task<List<InventoryItemModel>> GetInventorySelected(string category);
        Task<List<InventoryItemModel>> GetInventoryComputers();
        Task DeleteItemFromInventory(string _id,string category);
        Task UpdateItemFromInventory(string id,InventoryItemModel update);

    }
}
