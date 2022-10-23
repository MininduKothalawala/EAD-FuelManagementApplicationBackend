using FuelManagementApplication.Models;
using System;

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
