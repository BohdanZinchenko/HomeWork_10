using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebPart_Task_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        

        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task ShowStatus()
        {
            _logger.LogInformation($"{"http://localhost:5000/status"}");
            await Response.WriteAsync("Student of PM tech Academy\n");
            await Response.WriteAsync("Task 10\n");
            await Response.WriteAsync("Zinchenko Bohdan");
        }

    }
}
