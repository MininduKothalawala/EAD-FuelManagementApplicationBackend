using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult GetAllStations()
        {
            try
            {
                var fuelStations = fuelStationRepository.GetStations();

                if(fuelStations == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(fuelStations);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetStationById")]
        public IActionResult GetStationById(Guid id)
        {
            try
            {
                var fuelStation = fuelStationRepository.GetStationById(id);

                if (fuelStation == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(fuelStation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPost]
        [Route("AddNewStation")]
        public async Task<IActionResult> AddNewStation(FuelStation fuelStation)
        {
            try
            {
                var station = await fuelStationRepository.AddStationAsync(fuelStation);
                return Ok(station);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateStation")]
        public async Task<IActionResult> UpdateStation(FuelStation fuelStation)
        {
            try
            {
                var station = await fuelStationRepository.UpdateStation(fuelStation);
                return Ok(station);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteStation")]
        public async Task<IActionResult> DeleteStation(FuelStation fuelStation)
        {
            try
            {
                var station = await fuelStationRepository.DeleteStation(fuelStation);
                return Ok(station);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
