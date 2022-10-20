using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Models
{
    public class VehicalOwner
    {
        [BsonId]
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public FuelTypes FuelType { get; set; }
        public string VehicalNo { get; set; }
    }

    public enum FuelTypes
    {
        Petrol = 0,
        Desel = 1
    }
}
