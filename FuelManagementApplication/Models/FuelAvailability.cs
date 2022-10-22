using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FuelManagementApplication.Models
{
    public class FuelAvailability
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public bool IsPetrolAvailable { get; set; }
        public bool IsDeselAvailable { get; set; }
        public int NumberOfPetrolVehicalsInQueue { get; set; }
        public int NumberOfDeselVehicalsInQueue { get; set; }
        public float AvarageTimeInQueue { get; set; }
        public int TotalNumberOfVehicalsGotFuel { get; set; }
    }
}
