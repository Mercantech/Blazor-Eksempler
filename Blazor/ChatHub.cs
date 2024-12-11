using Microsoft.AspNetCore.SignalR;
using DomainModels.EFCore;
using Blazor.Data;

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

    private readonly ApplicationDbContext _dbContext;

    public ChatHub(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SendMessage(string user, string message)
    {
        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid().ToString(),
            User = user,
            Message = message,
            CreatedAt = DateTime.UtcNow.AddHours(2),
            UpdatedAt = DateTime.UtcNow.AddHours(2)
        };

        _dbContext.ChatMessages.Add(chatMessage);
        await _dbContext.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveMessage", chatMessage.User, chatMessage.Message);
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

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = GetUniqueUserName();
        await Clients.All.SendAsync("UserDisconnected", user);
        await base.OnDisconnectedAsync(exception);
    }
} 