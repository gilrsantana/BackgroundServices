using BackgroundServices.Models;

namespace BackgroundServices.Repositories;

public class PluginRepository : IPluginRepository
{
    public async Task<List<Plugin>> GetPluginsAsync()
    {
        return new List<Plugin>
        {
            new() { Id = Guid.NewGuid(), Name = "Plugin 1", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 2", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 3", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 4", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 5", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 6", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 7", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 8", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 9", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 10", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 11", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 12", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 13", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 14", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 15", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 16", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 17", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 18", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 19", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 20", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 21", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 22", QueueClient = new QueueClient() },
            new() { Id = Guid.NewGuid(), Name = "Plugin 23", QueueClient = new QueueClient() }
        };
    }
}