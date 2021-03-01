using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebPart_Task_1.Controllers
{
    [Route("primes")]
    [ApiController]
    public class PrimesController : ControllerBase
    {
        private readonly ILogger<PrimesController> _logger;
        private readonly PrimesNumbers _primesNubmers;
       public  PrimesController(ILogger<PrimesController> logger, PrimesNumbers primesNumbers)
        {
            _logger = logger;
            _primesNubmers = primesNumbers;
        }
        [HttpGet("{prime}")]
        public async Task FindPrime(string prime)
        {
            _logger.LogInformation($"Someone follow the link /primes/{prime} ");
            
            if(!int.TryParse(prime,out var resultNum))
            {
                _logger.LogError("Cant parse a number, return status code 400");
                Response.StatusCode = 400;
                
            }
            if (resultNum < 0)
            {
                _logger.LogError("Cant parse a number, return status code 400");
                Response.StatusCode = 400;
                
            }
            _logger.LogInformation("Start to detect the number for its primaries");
            _logger.LogInformation("Shown information about this number");
            Response.StatusCode = 200; 
            var result = await _primesNubmers.OneNumberPrimeAsync(resultNum);
            Response.StatusCode = result == HttpStatusCode.OK ? 200 : 400;
           

        }
        [HttpGet]
        public async Task<List<int>> FindPrimesInArray()
        {

            _logger.LogInformation($"Someone follow the link /primes?from=x&to=y ");

            _logger.LogInformation("Start to find all primaries in range");
            
            var from =Request.Query["from"].FirstOrDefault();
            if (!int.TryParse(from, out var fromResult))
            {
                _logger.LogError("Cant parse a (From), return status code 400");
                Response.StatusCode = 400;
                return null;
            }

            var to = Request.Query["to"].FirstOrDefault();
            if (!int.TryParse(to, out var toResult))
            {
                _logger.LogError("Cant parse a (To), return status code 400");
                Response.StatusCode = 400;
                return null;
            }

            if (fromResult > toResult)
            {
                _logger.LogInformation("From higher then to, end with status code 200");
                Response.StatusCode = 200;
                return null;
            }
            var result = await _primesNubmers.PrimeNumbersFindAsync(fromResult, toResult);
            if (result.Count == 0)
            {
                _logger.LogInformation("Not founded prime numbers, end with status code 200");
                Response.StatusCode = 200;
                return null ;
            }
            _logger.LogInformation("Founded primes numbers ");
           //await Response.WriteAsync(string.Join(",", result));
           return result;


        }
    }
}
