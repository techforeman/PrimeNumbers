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
    public class ResultsXMLController : ControllerBase
    {
        private ILoggerManager _logger;
        private IResultsRepositoryXML _repository;

        public ResultsXMLController(ILoggerManager logger, IResultsRepositoryXML repository)
        {
            _logger = logger;
            _repository = repository;
        }
        // GET api/values
        [HttpGet]
        public IActionResult GetAllResults()
        {
            _logger.LogInfo("Took list of results from XML");
            var results =  _repository.GetAllResults();
            return Ok(results);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteResult(int id)
        {
           var deletedData = _repository.DeleteResult(id);
           return Ok(deletedData);
        }

        

         // POST api/values
        [HttpPost]
        public IActionResult AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken)
        {   
            
            _logger.LogInfo($"{username} took try to search prime numbers within range {minRange} - {maxRange} using XML controller");
            var result = _repository.AddResult(minRange, maxRange, username, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
             if (!cancellationToken.IsCancellationRequested)
             {
                 _logger.LogInfo($"{username} receive results: {result.ResultValues} using XML controller");
                  return Ok(result);
             }
        
            return Ok();
                
                

           
            
        }

    } 
}
