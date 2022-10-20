using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicalOwnerControler : ControllerBase
    {
        private readonly IVehicalOwnerRepository vehicalOwnerRepository;

        public VehicalOwnerControler(IVehicalOwnerRepository vehicalOwnerRepository)
        {
            this.vehicalOwnerRepository = vehicalOwnerRepository;
        }

        [HttpGet]
        [Route("GetAllVehicalOwners")]
        public IEnumerable<VehicalOwner> GetAllStations()
        {
            var vehicalOwners = vehicalOwnerRepository.GetVehicalOwners();
            return vehicalOwners;
        }

        [HttpPost]
        [Route("AddNewVehicalOwner")]
        public async Task<VehicalOwner> AddNewVehicalOwner(VehicalOwner vehicalOwner)
        {
            var result = await vehicalOwnerRepository.AddVehicalOwnerAsync(vehicalOwner);
            return result;
        }

        [HttpPut]
        [Route("UpdateVehicalOwner")]
        public async Task<VehicalOwner> UpdateVehicalOwner(VehicalOwner vehicalOwner)
        {
            var result = await vehicalOwnerRepository.UpdateVehicalOwner(vehicalOwner);
            return result;
        }

        [HttpDelete]
        [Route("DeleteVehicalOwner")]
        public async Task<string> DeleteVehicalOwner(VehicalOwner vehicalOwner)
        {
            var result = await vehicalOwnerRepository.DeleteVehicalOwner(vehicalOwner);
            return result;
        }
    }
}
