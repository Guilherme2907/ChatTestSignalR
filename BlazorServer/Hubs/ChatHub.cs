using Microsoft.AspNetCore.SignalR;

namespace BlazorServer.Hubs;

public class ChatHub : Hub
{
    private static Dictionary<string, string> users = new ();

    public Task SendMessage(string sender, string message, string recipient)
    {
        var connectionIdRecipient= users[recipient];
        var connectionIdSender = users[sender];
        return Clients.Clients(connectionIdRecipient, connectionIdSender).SendAsync("ReceiveMessage", sender, message);
    }

    public override Task OnConnectedAsync()
    {
        string userId = Context.GetHttpContext().Request.Query["user"];

        users[userId] = Context.ConnectionId;

        return base.OnConnectedAsync();
    }
}
