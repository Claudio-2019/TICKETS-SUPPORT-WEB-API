using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API_TICKETS_SUPPORT.Interfaces;
using WEB_API_TICKETS_SUPPORT.Models;
using WEB_API_TICKETS_SUPPORT.Services.DatabaseAccess;

namespace WEB_API_TICKETS_SUPPORT.Services.InventoryService
{
    public class InventoryServices : IInventoryItems
    {
        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<InventoryItemModel> CollectionInventoryComputers;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventoryNetworkDevices;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventorySecurityCameras;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventorySoundEquipment;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventoryMultimedia;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventoryTabletsIpads;
        private readonly IMongoCollection<InventoryItemModel> CollectionInventorySoftwareUtilities;

        public InventoryServices()
        {
            CollectionInventoryComputers = accessDB.database.GetCollection<InventoryItemModel>("InventoryComputers");
            CollectionInventoryNetworkDevices = accessDB.database.GetCollection<InventoryItemModel>("InventoryNetworkDevices");
            CollectionInventorySecurityCameras = accessDB.database.GetCollection<InventoryItemModel>("InventorySecurityCameras");
            CollectionInventorySoundEquipment = accessDB.database.GetCollection<InventoryItemModel>("InventorySoundEquipment");
            CollectionInventoryMultimedia = accessDB.database.GetCollection<InventoryItemModel>("InventoryMultimedia");
            CollectionInventoryTabletsIpads = accessDB.database.GetCollection<InventoryItemModel>("InventoryTabletsIpads");
            CollectionInventorySoftwareUtilities = accessDB.database.GetCollection<InventoryItemModel>("InventorySoftware");
        }

        public async Task InsertItemToInventory(InventoryItemModel item)
        {

            switch (item.Category)
            {
                case "COMPUTERS":
                    await CollectionInventoryComputers.InsertOneAsync(item);
                    break;
                case "NETWORK DEVICES":
                    await CollectionInventoryNetworkDevices.InsertOneAsync(item);
                    break;
                case "SECURITY CAMERAS":
                    await CollectionInventorySecurityCameras.InsertOneAsync(item);
                    break;
                case "SOUND EQUIPMENT":
                    await CollectionInventorySoundEquipment.InsertOneAsync(item);
                    break;
                case "PROYECTION AND MULTIMEDIA DEVICES":
                    await CollectionInventoryMultimedia.InsertOneAsync(item);
                    break;
                case "TABLETS OR IPADS":
                    await CollectionInventoryTabletsIpads.InsertOneAsync(item);
                    break;
                case "SOFTWARE UTILITIES":
                    await CollectionInventorySoftwareUtilities.InsertOneAsync(item);
                    break;
                default:
                    break;
            }
        }

        public async Task<List<InventoryItemModel>> GetInventorySelected(string categorySelected)
        {
            switch (categorySelected)
            {
                case "COMPUTERS":
                    return await CollectionInventoryComputers.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "NETWORK DEVICES":
                    return await CollectionInventoryNetworkDevices.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "SECURITY CAMERAS":
                    return await CollectionInventorySecurityCameras.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "SOUND EQUIPMENT":
                    return await CollectionInventorySoundEquipment.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "PROYECTION AND MULTIMEDIA DEVICES":
                    return await CollectionInventoryMultimedia.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "TABLETS OR IPADS":
                    return await CollectionInventoryTabletsIpads.FindAsync(new BsonDocument()).Result.ToListAsync();
                case "SOFTWARE UTILITIES":
                    return await CollectionInventorySoftwareUtilities.FindAsync(new BsonDocument()).Result.ToListAsync();
                default:
                    return null;
            }


        }

        public Task DeleteItemFromInventory(string _id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemFromInventory(InventoryItemModel update)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InventoryItemModel>> GetInventoryComputers()
        {
            return await CollectionInventoryComputers.FindAsync(new BsonDocument()).Result.ToListAsync();

        }
    }
}
