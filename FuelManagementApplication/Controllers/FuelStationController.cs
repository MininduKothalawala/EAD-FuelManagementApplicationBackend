using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelStationController : ControllerBase
    {
        private readonly IFuelStationRepository fuelStationRepository;

        public FuelStationController(IFuelStationRepository fuelStationRepository)
        {
            this.fuelStationRepository = fuelStationRepository;
        }

        [HttpGet]
        [Route("GetAllStations")]
        public IEnumerable<FuelStation> GetAllStations()
        {
            var fuelStations = fuelStationRepository.GetStations();
            return fuelStations;
        }

        [HttpPost]
        [Route("AddNewStation")]
        public async Task<FuelStation> AddNewStation(FuelStation fuelStation)
        {
            var station = await fuelStationRepository.AddStationAsync(fuelStation);
            return station;
        }

        [HttpPut]
        [Route("UpdateStation")]
        public async Task<FuelStation> UpdateStation(FuelStation fuelStation)
        {
            var station = await fuelStationRepository.UpdateStation(fuelStation);
            return station;
        }

        [HttpDelete]
        [Route("DeleteStation")]
        public async Task<string> DeleteStation(FuelStation fuelStation)
        {
            var station = await fuelStationRepository.DeleteStation(fuelStation);
            return station;
        }
    }
}
