using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Repositories
{
    public class FuelQueueRepository : IFuelQueueRepository
    {
        private readonly IConfiguration configuration;

        public FuelQueueRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<FuelQueue> AddNewRecord(FuelQueue fuelQueue)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelQueue>("FuelQueue").InsertOneAsync(fuelQueue);

            return fuelQueue;
        }

        public async Task<FuelQueue> MarkOutTime(FuelQueue fuelQueue)
        {
            //Get time spent in queue
            TimeSpan ts = (TimeSpan)(fuelQueue.OutTime - fuelQueue.InTime);
            fuelQueue.TimeSpentInQueue = (float)ts.TotalHours;

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelQueue>.Filter.Eq("Id", fuelQueue.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelQueue>("FuelQueue").ReplaceOneAsync(filter, fuelQueue);

            return fuelQueue;
        }
    }
}
