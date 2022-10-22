using FuelManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.ViewModels
{
    public class FuelAvailabilityViewModel
    {
        public Guid StationId { get; set; }
        public FuelTypes FuelType { get; set; }
        public bool IsFuelAvailable { get; set; }
        public float TimeSpentInQueue { get; set; }
    }
}
