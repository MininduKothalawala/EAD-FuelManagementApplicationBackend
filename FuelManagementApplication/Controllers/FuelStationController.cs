using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.Utilities;
using FuelManagementApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("/FuelStation/")]
    public class FuelStationController : ControllerBase
    {
        private readonly IFuelStationRepository fuelStationRepository;
        private readonly IFuelAvailabilityRepository fuelAvailabilityRepository;

        public FuelStationController(IFuelStationRepository fuelStationRepository, 
            IFuelAvailabilityRepository fuelAvailabilityRepository)
        {
            this.fuelStationRepository = fuelStationRepository;
            this.fuelAvailabilityRepository = fuelAvailabilityRepository;
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

        [HttpGet]
        [Route("GetStationByUsername")]
        public IActionResult GetStationByUserName(string username)
        {
            try
            {
                var fuelStation = fuelStationRepository.GetStationByUserName(username);

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

        [HttpGet]
        [Route("GetAvailabillityById")]
        public IActionResult GetFuelAvailabilityByStationById(Guid id)
        {
            try
            {
                var fuelAvailability = fuelAvailabilityRepository.GetRecordByStationId(id);

                if (fuelAvailability == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(fuelAvailability);
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

        [HttpPut]
        [Route("UpdateFuelStatus")]
        public async Task<IActionResult> UpdateFuelStatus(FuelStatusViewModel fuelStatus)
        {
            try
            {
                var station = await fuelAvailabilityRepository.UpdateFuelStatus(fuelStatus);
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
