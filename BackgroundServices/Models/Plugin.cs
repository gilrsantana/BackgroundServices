namespace BackgroundServices.Models;

public class Plugin
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public QueueClient QueueClient { get; set; }


    public async Task ProcessMessageAsync(Message message)
    {
        Console.WriteLine($"Processing message: {message.Content} from plugin {Name}");
        await Task.Delay(3000);
        Console.WriteLine($"Message processed: {message.Content} from plugin {Name}");
    }
}