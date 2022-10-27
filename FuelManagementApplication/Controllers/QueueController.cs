using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("/Queue/")]
    public class QueueController : Controller
    {
        private readonly IFuelQueueRepository _queueRepository;

        public QueueController(IFuelQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        [HttpPost]
        [Route("CheckIn")]
        public async Task<IActionResult> CheckInToQueue(FuelQueue queue)
        {
            try
            {
                var result = await _queueRepository.AddNewRecord(queue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("CheckOut")]
        public async Task<IActionResult> CheckOutFromQueue(FuelQueue queue)
        {
            try
            {
                var result = await _queueRepository.MarkOutTime(queue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
