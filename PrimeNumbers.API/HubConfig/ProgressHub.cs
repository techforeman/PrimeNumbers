using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PrimeNumbers.API.HubConfig;

namespace PrimeNumbers.API.Controllers.HubConfig
{
    public class ProgressHub : Hub, IProgressHub
    {
        

        public async Task SendToAll(string number)
        {
            await Clients.All.SendAsync("Send", number);
        }
    }
}    