using System.Collections.Concurrent;
using BackgroundServices.Models;
using BackgroundServices.Repositories;

namespace BackgroundServices.Services;

public class PluginProcessorService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PluginProcessorService> _logger;
    private readonly ConcurrentDictionary<Guid, Plugin> _activePlugins = new();
    private readonly SemaphoreSlim _semaphore = new(10);
    private bool _runnig = false;
    
    public PluginProcessorService(IServiceScopeFactory serviceScopeFactory, ILogger<PluginProcessorService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    public void StartProcessing(Guid pluginId)
    {
        if (_activePlugins.ContainsKey(pluginId))
        {
            _logger.LogInformation("Plugin {PluginId} is already running", pluginId);
            return;
        }
        using var scope = _serviceScopeFactory.CreateScope();
        var pluginRepository = scope.ServiceProvider.GetRequiredService<IPluginRepository>();
        var plugin = pluginRepository.GetPluginsAsync().Result.FirstOrDefault(p => p.Id == pluginId);
        if (plugin == null)
        {
            _logger.LogWarning("Plugin {PluginId} not found", pluginId);
            return;
        }
        _activePlugins.TryAdd(pluginId, plugin);
        _logger.LogInformation("Starting processing for plugin {PluginId}", pluginId);
        _runnig = true;
        _logger.LogInformation("Starting processing");
    }

    public void StartAllProcessing()
    {
        _runnig = true;
        _logger.LogInformation("Starting processing");
    }

    public void StopProcessing(Guid pluginId)
    {
        if (_activePlugins.TryRemove(pluginId, out _))
        {
            _logger.LogInformation("Stopping processing for plugin {PluginId}", pluginId);
        }
    }
    
    public void StopAllProcessing()
    {
        _runnig = false;
        _activePlugins.Clear();
        _logger.LogInformation("Stopping all processing");
    }

    public async Task RestartProcessing()
    {
        StopAllProcessing();
        await Task.Delay(1000);
        StartAllProcessing();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            if (!_runnig)
            {
                await Task.Delay(1000);
                continue;
            }
            
            using var scope = _serviceScopeFactory.CreateScope();
            var pluginRepository = scope.ServiceProvider.GetRequiredService<IPluginRepository>();

            if (_activePlugins.IsEmpty)
            {
                var plugins = await pluginRepository.GetPluginsAsync();
                foreach (var plugin in plugins)
                    _activePlugins.TryAdd(plugin.Id, plugin);
            }
            
            _logger.LogInformation("Start processing of {PluginsCount} plugins", _activePlugins.Count);
            
            var tasks = _activePlugins.Values.Select(ProcessPluginAsync).ToList();
            await Task.WhenAll(tasks);
            
            _logger.LogInformation("End processing of {PluginsCount} plugins and waiting 30 sec", _activePlugins.Count);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task ProcessPluginAsync(Plugin plugin)
    {
        await _semaphore.WaitAsync();
        try
        {
            _logger.LogInformation("Processing plugin {PluginName} - {PluginId}", plugin.Name,  plugin.Id);

            var messages = await plugin.QueueClient.GetMessagesAsync();
            foreach (var message in messages)
            {
                await plugin.ProcessMessageAsync(message);
                _logger.LogInformation("Processed message {MessageId} from plugin {PluginName} - {PluginId}", message.Id, plugin.Name, plugin.Id);
            }
            
            _logger.LogInformation("End processing plugin {PluginName} - {PluginId}", plugin.Name,  plugin.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing plugin {PluginName} - {PluginId}", plugin.Name,  plugin.Id);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}