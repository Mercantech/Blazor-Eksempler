using Microsoft.AspNetCore.SignalR;

public class QuizHub : Hub
{
    private static Dictionary<string, DateTime> questionTimers = new();
    private const int QUESTION_TIME = 10; // Sekunder per spørgsmål
    private static Dictionary<string, HashSet<string>> quizPlayers = new();
    private static Dictionary<string, string> quizHosts = new(); // Tracker quiz hosts

    public async Task JoinQuiz(string quizId, string playerName)
    {
        // Opret quiz gruppe hvis den ikke findes
        if (!quizPlayers.ContainsKey(quizId))
        {
            quizPlayers[quizId] = new HashSet<string>();
            quizHosts[quizId] = playerName; // Første spiller bliver host
        }

        // Tilføj spiller til gruppen
        quizPlayers[quizId].Add(playerName);

        // Tilføj forbindelse til SignalR gruppe
        await Groups.AddToGroupAsync(Context.ConnectionId, quizId);

        // Send liste over eksisterende spillere og host information
        await Clients.Caller.SendAsync("ExistingPlayers", quizPlayers[quizId].ToList(), quizHosts[quizId]);

        // Informer andre om den nye spiller
        await Clients.Group(quizId).SendAsync("PlayerJoined", playerName);
    }

    public async Task SubmitAnswer(string quizId, string playerName, int questionIndex, int answerIndex, bool isCorrect)
    {
        await Clients.Group(quizId).SendAsync("AnswerSubmitted", playerName, questionIndex, isCorrect);
    }
    public async Task NotifyPlayerAnswer(string quizId, string playerName, bool isCorrect)
    {
        await Clients.Group(quizId).SendAsync("PlayerAnswered", playerName, isCorrect);
    }

    public async Task RestartQuiz(string quizId)
    {
        await Clients.Group(quizId).SendAsync("QuizRestarted");
    }
    public async Task StartQuiz(string quizId)
    {
        questionTimers[quizId] = DateTime.UtcNow;
        await Clients.Group(quizId).SendAsync("QuizStarted", DateTime.UtcNow);
    }
    public async Task PlayerAnswered(string quizId, string playerName)
    {
        await Clients.Group(quizId).SendAsync("PlayerAnswered", playerName);
    }

    public async Task ProgressToNextQuestion(string quizId, int nextQuestionIndex)
    {
        questionTimers[quizId] = DateTime.UtcNow;
        await Clients.Group(quizId).SendAsync("NextQuestion", nextQuestionIndex, DateTime.UtcNow);
    }
}