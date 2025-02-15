namespace BackgroundServices.Models;

public class QueueClient
{
    public async Task<List<Message>> GetMessagesAsync()
    {
        await Task.Delay(3000);
        return new List<Message>
        {
            new() { Id = Guid.NewGuid(), Content = $"Message 1 - {Guid.NewGuid().ToString()}" },
            new() { Id = Guid.NewGuid(), Content = $"Message 2 - {Guid.NewGuid().ToString()}" },
            new() { Id = Guid.NewGuid(), Content = $"Message 3 - {Guid.NewGuid().ToString()}" }
        };
    }
}