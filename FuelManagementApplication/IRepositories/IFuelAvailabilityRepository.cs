using FuelManagementApplication.Models;
using FuelManagementApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.IRepositories
{
    public interface IFuelAvailabilityRepository
    {
        Task<FuelAvailability> GetRecordByStationId(Guid stationId);
        Task<FuelAvailability> AddNewRecord(FuelAvailability fuelAvailability);
        Task<FuelAvailability> UpdateFuelStatus(FuelStatusViewModel fuelStatus);
        Task<FuelAvailability> UpdateRecordByVehicalOwnerIn(FuelAvailabilityViewModel fuelAvailabilityView);
        Task<FuelAvailability> UpdateRecordByVehicalOwnerOut(FuelAvailabilityViewModel fuelAvailabilityView);
    }
}
