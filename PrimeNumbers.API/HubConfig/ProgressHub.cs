using Microsoft.AspNetCore.SignalR;

namespace PrimeNumbers.API.Controllers.HubConfig
{
    public class ProgressHub : Hub
    {
        public void SendToAll(string currentNumber)
        {
            Clients.All.SendAsync("Send", currentNumber);
        } 
    }
}    