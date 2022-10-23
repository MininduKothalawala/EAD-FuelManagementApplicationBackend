using FuelManagementApplication.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.IRepositories
{
    public interface IFuelQueueRepository
    {
        Task<FuelQueue> AddNewRecord(FuelQueue fuelQueue);
        Task<FuelQueue> MarkOutTime(FuelQueue fuelQueue);
    }
}
