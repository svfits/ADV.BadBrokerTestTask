
using ADV.BadBroker.WebService.BL;

namespace ADV.BadBroker.WebService.BackgroundServices;

/// <summary>
/// can be done according to the schedule or as here through a pause
/// </summary>
public class BackgroundWriteOff : IHostedService, IDisposable
{
    private readonly IServiceProvider Services;
    private readonly ILogger<BackgroundWriteOff> logger;
    private Timer? _timer = null;

    public BackgroundWriteOff(ILogger<BackgroundWriteOff> logger, IServiceProvider services)
    {
        Services = services;
        this.logger = logger;
    }

    private async void DoWork(object? state)
    {
        logger.LogInformation("BackgroundWriteOff is working.");

        using var scope = Services.CreateScope();
        var writeOff = scope.ServiceProvider.GetRequiredService<IWriteOff>();

        var dtNow = DateTime.Now;

        try
        {
            await writeOff.Accrual(dtNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error BackgroundWriteOff executing");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("BackgroundWriteOff Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();

    /// <summary>
    /// if you really want to stop the process
    /// </summary>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("BackgroundWriteOff Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}
