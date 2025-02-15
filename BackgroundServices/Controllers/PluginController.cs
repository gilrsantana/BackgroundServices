using BackgroundServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServices.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PluginController : ControllerBase
{
    private readonly PluginProcessorService _pluginProcessorService;
    private readonly ILogger<PluginController> _logger;

    public PluginController(PluginProcessorService pluginProcessorService, ILogger<PluginController> logger)
    {
        _pluginProcessorService = pluginProcessorService;
        _logger = logger;
    }
    
    [HttpPost("start-all")]
    public IActionResult StartProcessing()
    {
        _pluginProcessorService.StartAllProcessing();
        _logger.LogInformation("Processing started by API");
        return Ok("Processing started");
    }
    
    [HttpPost("start/{id}")]
    public IActionResult StartProcessing(Guid id)
    {
        _pluginProcessorService.StartProcessing(id);
        _logger.LogInformation("Processing started by API for plugin {PluginId}", id);
        return Ok($"Processing of Plugin {id} started");
    }
    
    [HttpPost("stop/{id}")]
    public IActionResult StopProcessing(Guid id)
    {
        _pluginProcessorService.StopProcessing(id);
        _logger.LogInformation("Processing stopped by API for plugin {PluginId}", id);
        return Ok($"Processing of Plugin {id} stopped");
    }
    
    [HttpPost("stop-all")]
    public IActionResult StopAllProcessing()
    {
        _pluginProcessorService.StopAllProcessing();
        _logger.LogInformation("All processing stopped by API");
        return Ok("All processing stopped");
    }
    
    [HttpPost("restart")]
    public async Task<IActionResult> RestartProcessing()
    {
        await _pluginProcessorService.RestartProcessing();
        _logger.LogInformation("Processing restarted by API");
        return Ok("Processing restarted");
    }
}