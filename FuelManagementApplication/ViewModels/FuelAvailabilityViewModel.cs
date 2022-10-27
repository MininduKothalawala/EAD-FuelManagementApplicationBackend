using FuelManagementApplication.Models;
using System;

namespace FuelManagementApplication.ViewModels
{
    public class FuelAvailabilityViewModel
    {
        public Guid StationId { get; set; }
        public string FuelType { get; set; }
        public float TimeSpentInQueue { get; set; }
    }

    public class FuelStatusViewModel
    {
        public string StationName { get; set; }
        public string FuelType { get; set; }
        public bool fuelArrived { get; set; }
        public bool fuelFinished { get; set; }
    }
}
