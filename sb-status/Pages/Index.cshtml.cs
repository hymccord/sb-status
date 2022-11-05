using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using sb_status.Services;

using Yarp.ReverseProxy.Model;

namespace sb_status.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public DestinationHealth Health { get; init; }

    public IReadOnlyList<DestinationState> Backups { get; }

    public IndexModel(ILogger<IndexModel> logger, IOfficialClusterStatus officialClusterStatus)
    {
        _logger = logger;
        Health = officialClusterStatus.Health;
        Backups = officialClusterStatus.Backups ?? new List<DestinationState>();
    }

    public void OnGet()
    {

    }

    public static string HeathToEmoji(DestinationHealth status) => status switch
    {
        DestinationHealth.Healthy => "✔",
        DestinationHealth.Unhealthy => "💀",
        DestinationHealth.Unknown=> "❔",
        _ => throw new NotImplementedException()
    };
}