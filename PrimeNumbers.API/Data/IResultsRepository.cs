using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Data
{
    public interface IResultsRepository
    {
        Task<IEnumerable<Result>> GetAllResults();
        Task<Result> AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken);

        Task<Result> DeleteResult(int id);
         
    }
}