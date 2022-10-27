using FuelManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.IRepositories
{
    public interface IVehicalOwnerRepository
    {
        List<VehicalOwner> GetVehicalOwners();
        Task<VehicalOwner> GetVehicleOwnerByUserName(string username);
        Task<VehicalOwner> AddVehicalOwnerAsync(VehicalOwner vehicalOwner);
        Task<VehicalOwner> UpdateVehicalOwner(VehicalOwner vehicalOwner);
        Task<string> DeleteVehicalOwner(VehicalOwner vehicalOwner);
    }
}
