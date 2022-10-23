using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.ViewModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Repositories
{
    public class FuelAvailabilityRepository : IFuelAvailabilityRepository
    {
        private readonly IConfiguration configuration;

        public FuelAvailabilityRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<FuelAvailability> AddNewRecord(FuelAvailability fuelAvailability)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").InsertOneAsync(fuelAvailability);

            return fuelAvailability;
        }

        public async Task<FuelAvailability> GetRecordByStationId(Guid stationId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").AsQueryable();
            var fuelAvailabilitie = results.Where(x => x.StationId == stationId).FirstOrDefault();
            return fuelAvailabilitie;
        }

        public async Task<FuelAvailability> UpdateRecordByVehicalOwnerIn(FuelAvailabilityViewModel fuelAvailabilityView)
        {
            //Get Fuel Availability
            FuelAvailability fuelAvailability = await GetRecordByStationId(fuelAvailabilityView.StationId);

            if(fuelAvailability == null)
            {
                return null;
            }
            //Update Record by vehical details
            if(fuelAvailabilityView.FuelType == FuelTypes.Desel)
            {
                fuelAvailability.NumberOfDeselVehicalsInQueue = fuelAvailability.NumberOfDeselVehicalsInQueue + 1;
            }
            else if (fuelAvailabilityView.FuelType == FuelTypes.Petrol)
            {
                fuelAvailability.NumberOfPetrolVehicalsInQueue = fuelAvailability.NumberOfPetrolVehicalsInQueue + 1;
            }

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelAvailability>.Filter.Eq("Id", fuelAvailability.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").ReplaceOneAsync(filter, fuelAvailability);

            return fuelAvailability;
        }

        public async Task<FuelAvailability> UpdateRecordByVehicalOwnerOut(FuelAvailabilityViewModel fuelAvailabilityView)
        {
            //Get Fuel Availability
            FuelAvailability fuelAvailability = await GetRecordByStationId(fuelAvailabilityView.StationId);

            if (fuelAvailability == null)
            {
                return null;
            }

            //Update Record by vehical details
            fuelAvailability.AvarageTimeInQueue = ((fuelAvailability.AvarageTimeInQueue*fuelAvailability.NumberOfDeselVehicalsInQueue) + fuelAvailabilityView.TimeSpentInQueue) 
                / (fuelAvailability.TotalNumberOfVehicalsGotFuel + 1);

            fuelAvailability.TotalNumberOfVehicalsGotFuel = fuelAvailability.TotalNumberOfVehicalsGotFuel + 1;

            if (fuelAvailabilityView.FuelType == FuelTypes.Desel)
            {
                fuelAvailability.NumberOfDeselVehicalsInQueue = fuelAvailability.NumberOfDeselVehicalsInQueue - 1;
            }
            else if (fuelAvailabilityView.FuelType == FuelTypes.Petrol)
            {
                fuelAvailability.NumberOfPetrolVehicalsInQueue = fuelAvailability.NumberOfPetrolVehicalsInQueue - 1;
            }

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelAvailability>.Filter.Eq("Id", fuelAvailability.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").ReplaceOneAsync(filter, fuelAvailability);

            return fuelAvailability;
        }
    }
}
