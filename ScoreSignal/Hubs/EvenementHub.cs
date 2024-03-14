using Microsoft.AspNetCore.SignalR;
namespace ScoreSignal.Hubs
{
public class EvenementHub : Hub
{
public async Task SendMessage()
{
await Clients.All.SendAsync("EvenementChange");
}
}
}