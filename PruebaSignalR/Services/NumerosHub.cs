using Microsoft.AspNetCore.SignalR;

namespace PruebaSignalR.Services
{
    public class NumerosHub:Hub
    {
        public async Task Incrementar()
        {
            await Clients.All.SendAsync("Incremento");
        }

        public async Task Decrementar()
        {
            await Clients.All.SendAsync("Decremento");
        }
    }
}
