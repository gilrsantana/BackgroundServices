using BackgroundServices.Models;

namespace BackgroundServices.Repositories;

public interface IPluginRepository
{
    Task<List<Plugin>> GetPluginsAsync();
}