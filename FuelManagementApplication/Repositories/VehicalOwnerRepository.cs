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

        public async Task<VehicalOwner> AddVehicalOwnerAsync(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").InsertOneAsync(vehicalOwner);

            return vehicalOwner;
        }

        public async Task<string> DeleteVehicalOwner(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<VehicalOwner>.Filter.Eq("Id", vehicalOwner.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").DeleteOneAsync(filter);

            return "Successfully Deleted";
        }

        public List<VehicalOwner> GetVehicalOwners()
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").AsQueryable();
            List<VehicalOwner> reviews = results.ToList();
            return reviews;
        }

        public async Task<VehicalOwner> UpdateVehicalOwner(VehicalOwner vehicalOwner)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<VehicalOwner>.Filter.Eq("Id", vehicalOwner.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<VehicalOwner>("VehicalOwner").ReplaceOneAsync(filter, vehicalOwner);

            return vehicalOwner;
        }
    }
}
