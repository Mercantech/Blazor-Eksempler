using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private static List<string> userNames = new List<string>
    {
        "PixelPenguin89",
        "CodeCactus3000",
        "BlazorBeastie",
        "FunkyFerret42",
        "DebugDingo",
        "ChattyCheetah",
        "WittyWalrus77",
        "LazyLlama88",
        "BinaryBumblebee",
        "QuantumQuokka",
        "APIAlpaca",
        "404FoxNotFound",
        "SyntaxSloth",
        "DataDodo9000",
        "VariableViper",
        "NullNarwhal",
        "LoopingLobster",
        "EventEmitterEagle",
        "BlazorBuffalo",
        "SQLSeagull",
        "CacheKoala",
        "GiggleGiraffe",
        "RoutingRaccoon",
        "SessionShark",
        "BlazingBeaver"
    };

    private static Random random = new Random();

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public string GetUniqueUserName()
    {
        if (userNames.Count == 0)
        {
            return "AnonymousAardvark";
        }

        int index = random.Next(userNames.Count);
        string uniqueUserName = userNames[index];
        userNames.RemoveAt(index);
        return uniqueUserName;
    }

    public async Task Typing(string user)
    {
        await Clients.Others.SendAsync("UserTyping", user);
    }

    public override async Task OnConnectedAsync()
    {
        var user = GetUniqueUserName();
        await Clients.All.SendAsync("UserConnected", user);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var user = GetUniqueUserName();
        await Clients.All.SendAsync("UserDisconnected", user);
        await base.OnDisconnectedAsync(exception);
    }
} 