using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.ViewModels;
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
        private readonly IFuelAvailabilityRepository fuelAvailabilityRepository;

        public FuelQueueRepository(IConfiguration configuration, IFuelAvailabilityRepository fuelAvailabilityRepository)
        {
            this.configuration = configuration;
            this.fuelAvailabilityRepository = fuelAvailabilityRepository;
        }

        //Add new record
        //This will trigger when user join to the queue
        public async Task<FuelQueue> AddNewRecord(FuelQueue fuelQueue)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelQueue>("FuelQueue").InsertOneAsync(fuelQueue);

            FuelAvailabilityViewModel fuelAvailabilityViewModel = new FuelAvailabilityViewModel();
            fuelAvailabilityViewModel.StationId = fuelQueue.StationId;
            fuelAvailabilityViewModel.FuelType = fuelQueue.FuelType;

            await fuelAvailabilityRepository.UpdateRecordByVehicalOwnerIn(fuelAvailabilityViewModel);

            return fuelQueue;
        }

        //Trigger this one when user leave the queue
        public async Task<FuelQueue> MarkOutTime(FuelQueue fuelQueue)
        {
            //Get time spent in queue
            TimeSpan ts = (TimeSpan)(fuelQueue.OutTime - fuelQueue.InTime);
            fuelQueue.TimeSpentInQueue = (float)ts.TotalHours;

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelQueue>.Filter.Eq("Id", fuelQueue.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelQueue>("FuelQueue").ReplaceOneAsync(filter, fuelQueue);

            FuelAvailabilityViewModel fuelAvailabilityViewModel = new FuelAvailabilityViewModel();
            fuelAvailabilityViewModel.StationId = fuelQueue.StationId;
            fuelAvailabilityViewModel.FuelType = fuelQueue.FuelType;
            fuelAvailabilityViewModel.TimeSpentInQueue = fuelQueue.TimeSpentInQueue;

            await fuelAvailabilityRepository.UpdateRecordByVehicalOwnerOut(fuelAvailabilityViewModel);

            return fuelQueue;
        }
    }
}
