using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeNumbers.API.Controllers.HubConfig;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Data
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly DataContext _context;
        

        public ResultsRepository(DataContext context)
        {
            _context = context;
            
        }

           public async Task<IEnumerable<Result>> GetAllResults()
        {
            var results =  await _context.Results.ToListAsync();
            return(results);            
        }

        public async Task<Result> AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken)
        {

            Result res = new Result();
            DateTime dateOfStart = DateTime.Now;
            res.DateOfStart = dateOfStart;
            string strPrimeValues = "";
            for (int i = minRange; i <= maxRange; i++)
            {
                 //await Task.Delay(100);
                var isPrime = await CheckIsPrimeAsync(i);
                if (isPrime==true)
                {
                   strPrimeValues = strPrimeValues + $"{i}; ";
                }
            }
            res.ResultValues = strPrimeValues.TrimEnd();
            res.MinRangeData = minRange;
            res.MaxRangeData = maxRange;
            DateTime dateOfEnd = DateTime.Now;
            res.DateOfEnd = dateOfEnd;
            res.UserName = username;
            await _context.Results.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;

        }

       

        public async Task<Result> DeleteResult(int id)
        {
            
            var data = await _context.Results.FindAsync(id);
            _context.Results.Remove(data);
            await _context.SaveChangesAsync();
            return (data);
        }


         private async Task<bool> CheckIsPrimeAsync(int number)
        {
            await Task.Delay(0);
            if (number <= 1) return false;
            if (number == 2) return true;
            if(number % 2 == 0) return false;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i= 3; i<=boundary; i+=2 )
            {
                if (number % i == 0)
                    return false;
            }
                
            return true;        
            
        }


    }
}