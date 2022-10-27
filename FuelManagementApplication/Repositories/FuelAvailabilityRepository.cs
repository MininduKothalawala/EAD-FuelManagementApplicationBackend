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

        //Add new record
        public async Task<FuelAvailability> AddNewRecord(FuelAvailability fuelAvailability)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").InsertOneAsync(fuelAvailability);

            return fuelAvailability;
        }

        //Get record by id
        public async Task<FuelAvailability> GetRecordByStationId(Guid stationId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").AsQueryable();
            var fuelAvailabilitie = results.Where(x => x.StationId == stationId).FirstOrDefault();
            return fuelAvailabilitie;
        }

        //Update fuel station fuel status details
        public async Task<FuelAvailability> UpdateFuelStatus(FuelStatusViewModel fuelStatus)
        {
            FuelAvailability availability = await GetRecordByStationId(fuelStatus.StationId);

            if (fuelStatus.fuelFinished && !fuelStatus.fuelArrived)
            {
                if(fuelStatus.FuelType == "Petrol")
                {
                    availability.IsPetrolAvailable = false;
                }
                else if(fuelStatus.FuelType == "Desel")
                {
                    availability.IsDeselAvailable = false;
                }
            }
            else if(!fuelStatus.fuelFinished && fuelStatus.fuelArrived)
            {
                if (fuelStatus.FuelType == "Petrol")
                {
                    availability.IsPetrolAvailable = true;
                }
                else if (fuelStatus.FuelType == "Desel")
                {
                    availability.IsDeselAvailable = true;
                }
            }

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelAvailability>.Filter.Eq("StationId", fuelStatus.StationId);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelStation").ReplaceOneAsync(filter, availability);

            return availability;
        }

        //Using when trigger when user join to fuel queue.
        //It automaticaly increse fuel queue vehicle count.
        public async Task<FuelAvailability> UpdateRecordByVehicalOwnerIn(FuelAvailabilityViewModel fuelAvailabilityView)
        {
            //Get Fuel Availability
            FuelAvailability fuelAvailability = await GetRecordByStationId(fuelAvailabilityView.StationId);

            if(fuelAvailability == null)
            {
                return null;
            }
            //Update Record by vehical details
            if(fuelAvailabilityView.FuelType == "Desel")
            {
                fuelAvailability.NumberOfDeselVehicalsInQueue = fuelAvailability.NumberOfDeselVehicalsInQueue + 1;
            }
            else if (fuelAvailabilityView.FuelType == "Petrol")
            {
                fuelAvailability.NumberOfPetrolVehicalsInQueue = fuelAvailability.NumberOfPetrolVehicalsInQueue + 1;
            }

            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<FuelAvailability>.Filter.Eq("Id", fuelAvailability.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<FuelAvailability>("FuelAvailability").ReplaceOneAsync(filter, fuelAvailability);

            return fuelAvailability;
        }

        //Using when trigger when user leave the fuel queue.
        //It automaticaly decrese fuel queue vehicle count.
        //It automaticaly calculate avarage time spent in queue.
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

            if (fuelAvailabilityView.FuelType == "Desel")
            {
                fuelAvailability.NumberOfDeselVehicalsInQueue = fuelAvailability.NumberOfDeselVehicalsInQueue - 1;
            }
            else if (fuelAvailabilityView.FuelType == "Petrol")
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
