

using System.Threading.Tasks;

namespace PrimeNumbers.API.HubConfig
{
    public interface IProgressHub
    {
     Task SendToAll(string number);
    }
}