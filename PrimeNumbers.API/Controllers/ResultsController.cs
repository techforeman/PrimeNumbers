using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeNumbers.API.Data;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IResultsRepository _repository;

        public ResultsController(ILoggerManager logger, IResultsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAllResults()
        {
            _logger.LogInfo("Took list of results from db");
            var results =  await _repository.GetAllResults();
            return Ok(results);
        }

        

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken)
        {   
            
            _logger.LogInfo($"{username} took try to search prime numbers within range {minRange} - {maxRange}");
            var result = await _repository.AddResult(minRange, maxRange, username, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
             if (!cancellationToken.IsCancellationRequested)
             {
                 _logger.LogInfo($"{username} receive results: {result.ResultValues}");
                  return Ok(result);
             }
        
            return Ok();
                
                

           
            
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
           var deletedData = await _repository.DeleteResult(id);
           return Ok(deletedData);
        }
    }
}
