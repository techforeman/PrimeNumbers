using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Data
{
    public interface IResultsRepositoryXML
    {
        IEnumerable<ResultXml> GetAllResults();
        ResultXml AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken);

        bool DeleteResult(int id);

        void SaveXMLFile();
         
    }
}