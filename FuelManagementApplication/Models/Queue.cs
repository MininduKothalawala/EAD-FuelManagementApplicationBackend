using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Models
{
    public class Queue
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid StationId { get; set; }
        public Guid VehicalOwnerId { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public bool IsFuelAvailable { get; set; }
        public float TimeSpentInQueue { get; set; }
    }
}
