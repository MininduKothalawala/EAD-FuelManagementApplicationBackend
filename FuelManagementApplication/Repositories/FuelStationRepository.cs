using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Repositories
{
    public class FuelStationRepository : IFuelStationRepository
    {
        private readonly IConfiguration configuration;

        public FuelStationRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<FuelStation> AddStationAsync(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").InsertOneAsync(fuelStation);
            
            return fuelStation;
        }

        public List<FuelStation> GetStations()
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            List<FuelStation> fuelStations = results.ToList();
            return fuelStations;
        }

        public List<FuelStation> GetStationById(Guid stationId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            List<FuelStation> fuelStations = results.Where(x => x.Id == stationId).ToList();
            return fuelStations;
        }

        public async Task<FuelStation> UpdateStation(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelStation>.Filter.Eq("Id", fuelStation.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").ReplaceOneAsync(filter, fuelStation);

            return fuelStation;
        }

        public async Task<string> DeleteStation(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelStation>.Filter.Eq("Id", fuelStation.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").DeleteOneAsync(filter);

            return "Successfully Deleted";
        }
    }
}
