using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ProgressHub : Hub
{
    public async Task SendProgressUpdate(string progressMessage)
    {
        await Clients.All.SendAsync("ReceiveProgressUpdate", progressMessage);
    }
}