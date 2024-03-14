using Microsoft.AspNetCore.SignalR;
namespace ScoreSignal.Hubs
{
public class MatchHub : Hub
{
public async Task SendMessage()
{
await Clients.All.SendAsync("MatchChange");
}
}
}