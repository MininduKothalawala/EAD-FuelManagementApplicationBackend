using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("/VehicleOwner/")]
    public class VehicalOwnerControler : ControllerBase
    {
        private readonly IVehicalOwnerRepository vehicalOwnerRepository;

        public VehicalOwnerControler(IVehicalOwnerRepository vehicalOwnerRepository)
        {
            this.vehicalOwnerRepository = vehicalOwnerRepository;
        }

        [HttpGet]
        [Route("GetAllVehicalOwners")]
        public IActionResult GetAllStations()
        {
            try
            {
                var vehicalOwners = vehicalOwnerRepository.GetVehicalOwners();
                if (vehicalOwners == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(vehicalOwners);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetVehicleOwnerByUsername")]
        public async Task<IActionResult> GetVehicleOwnerByUsername(string username)
        {
            try
            {
                var result = await vehicalOwnerRepository.GetVehicleOwnerByUserName(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("AddNewVehicalOwner")]
        public async Task<IActionResult> AddNewVehicalOwner(VehicalOwner vehicalOwner)
        {
            try
            {
                var result = await vehicalOwnerRepository.AddVehicalOwnerAsync(vehicalOwner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateVehicalOwner")]
        public async Task<IActionResult> UpdateVehicalOwner(VehicalOwner vehicalOwner)
        {
            try
            {
                var result = await vehicalOwnerRepository.UpdateVehicalOwner(vehicalOwner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteVehicalOwner")]
        public async Task<IActionResult> DeleteVehicalOwner(VehicalOwner vehicalOwner)
        {
            try
            {
                var result = await vehicalOwnerRepository.DeleteVehicalOwner(vehicalOwner);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
