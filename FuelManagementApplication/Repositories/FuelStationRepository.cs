﻿using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.ViewModels;
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

        //Add station details
        public async Task<FuelStation> AddStationAsync(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").InsertOneAsync(fuelStation);
            
            FuelAvailability fuelAvailability = new FuelAvailability();
            fuelAvailability.StationId = fuelStation.Id;
            fuelAvailability.Username = fuelStation.Username;

            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").InsertOneAsync(fuelAvailability);

            return fuelStation;
        }

        //Get all stations as list
        public List<FuelStation> GetStations()
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            List<FuelStation> fuelStations = results.ToList();
            return fuelStations;
        }

        //Get fuel station details by id
        public List<FuelStation> GetStationById(Guid stationId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            List<FuelStation> fuelStations = results.Where(x => x.Id == stationId).ToList();
            return fuelStations;
        }

        //Get fuel station details by username
        public async Task<FuelStation> GetStationByUserName(string username)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").AsQueryable();
            var fuelStation = results.Where(x => x.Username == username).FirstOrDefault();
            return fuelStation;
        }

        //Update fuel station details
        public async Task<FuelStation> UpdateStation(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelStation>.Filter.Eq("Id", fuelStation.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").ReplaceOneAsync(filter, fuelStation);

            return fuelStation;
        }

        //Delete fuel station details
        public async Task<string> DeleteStation(FuelStation fuelStation)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelStation>.Filter.Eq("Id", fuelStation.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelStation>("FuelStation").DeleteOneAsync(filter);

            return "Successfully Deleted";
        }
    }
}
