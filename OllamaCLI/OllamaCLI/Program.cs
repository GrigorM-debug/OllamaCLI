using Microsoft.Extensions.AI;
using OllamaSharp;
using OllamaSharp.Models.Chat;
using System.Text;
using ChatRole = Microsoft.Extensions.AI.ChatRole;

Console.WriteLine("Hello, my name is Ollama! Ask everything.");

IChatClient client = new OllamaApiClient(
    new Uri("http://localhost:11434/"), "tinyllama");

List <ChatMessage> chatHistory = [new(ChatRole.Assistant, """
                                                   You answer any question. You are a helpful assistant.
                                                   """)];

while (true)
{
    // Get input
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("\nYou: ");
    var input = Console.ReadLine()!;
    chatHistory.Add(new(ChatRole.User, input));

    // Get reply
    try
    {
        var response = await client.GetResponseAsync(chatHistory);

        chatHistory.AddMessages(response);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Ollama: {response.Text}");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
    }
}