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
    public class VehicalOwnerRepository : IVehicalOwnerRepository
    {
        private readonly IConfiguration configuration;

        public VehicalOwnerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //Add new vehicle user details
        //Using in registration
        public async Task<VehicalOwner> AddVehicalOwnerAsync(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").InsertOneAsync(vehicalOwner);

            return vehicalOwner;
        }

        //Delete user
        public async Task<string> DeleteVehicalOwner(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<VehicalOwner>.Filter.Eq("Id", vehicalOwner.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").DeleteOneAsync(filter);

            return "Successfully Deleted";
        }

        //Get all vehicle users
        public List<VehicalOwner> GetVehicalOwners()
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").AsQueryable();
            List<VehicalOwner> reviews = results.ToList();
            return reviews;
        }

        //Get record by username
        public async Task<VehicalOwner> GetVehicleOwnerByUserName(string username)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").AsQueryable();
            var vehicleOwner = results.Where(x => x.Username == username).FirstOrDefault();
            return vehicleOwner;
        }

        //Update vehicle user details
        public async Task<VehicalOwner> UpdateVehicalOwner(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<VehicalOwner>.Filter.Eq("Id", vehicalOwner.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").ReplaceOneAsync(filter, vehicalOwner);

            return vehicalOwner;
        }
    }
}
