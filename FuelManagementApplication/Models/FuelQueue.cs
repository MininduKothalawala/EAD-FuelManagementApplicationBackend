using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FuelManagementApplication.Models
{
    public class FuelQueue
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public Guid VehicalOwnerId { get; set; }
        public string FuelType { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public float TimeSpentInQueue { get; set; }
    }
}
