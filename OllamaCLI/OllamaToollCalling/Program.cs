using System.Security.Cryptography;
using Microsoft.Extensions.AI;
using OllamaSharp;

internal class Program
{
    public static async Task Main()
    {
        
        string input = "Should I wear a coat today?";

        IChatClient client = new OllamaApiClient(
            new Uri("http://localhost:11434/"), "qwen2.5:0.5b");

        client = ChatClientBuilderChatClientExtensions
            .AsBuilder(client)
            .UseFunctionInvocation()
            .Build();

        ChatOptions options = new()
        {
            Tools = [AIFunctionFactory.Create(GetCurrentWeather)]
        };

        var reponse = client.GetStreamingResponseAsync(input, options);

        await foreach (var update in reponse)
        {
            Console.Write(update.Text);
        }
    }

    private static string GetCurrentWeather()
    {
        if (Random.Shared.NextDouble() > 0.5)
        {
            return "It's sunny and warm today.";
        }
        else
        {
            return "It's raining today.";
        }
    }
}