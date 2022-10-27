using FuelManagementApplication.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("/FuelStation/")]
    public class QueueController : Controller
    {
        private readonly IFuelQueueRepository _queueRepository;

        public QueueController(IFuelQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }


    }
}
