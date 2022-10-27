using FuelManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.IRepositories
{
    public interface IFuelStationRepository
    {
        List<FuelStation> GetStations();
        List<FuelStation> GetStationById(Guid stationId);
        List<FuelStation> GetStationByUserName(string username);
        Task<FuelStation> AddStationAsync(FuelStation fuelStation);
        Task<FuelStation> UpdateStation(FuelStation fuelStation);
        Task<string> DeleteStation(FuelStation fuelStation);
    }
}
