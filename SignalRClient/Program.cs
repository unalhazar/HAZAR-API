using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
           .WithUrl("http://localhost:5116/notificationHub")
           .Build();

connection.On<string, string>("ReceiveNotification", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to the hub.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

await connection.StopAsync();