using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FuelManagementApplication.Models
{
    public class VehicalOwner
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string OwnerName { get; set; }
        public string FuelType { get; set; }
        public string VehicalNo { get; set; }
    }
}
