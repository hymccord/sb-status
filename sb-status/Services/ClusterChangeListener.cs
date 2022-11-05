using Yarp.ReverseProxy.Model;

namespace sb_status.Services;

public class ClusterStatusIndicator : IClusterChangeListener, IOfficialClusterStatus
{
    private ClusterState? _sbCluster;

    public ClusterStatusIndicator()
    {
    }

    public DestinationHealth Health => _sbCluster?.Destinations.Single(d => d.Value.DestinationId == "official").Value.Health.Active ?? DestinationHealth.Unknown;

    public IReadOnlyList<DestinationState>? Backups => _sbCluster?.Destinations.Where(d => d.Key != "official").Select(d => d.Value).ToList();

    public void OnClusterAdded(ClusterState cluster)
    {
        if (cluster.ClusterId == "sb-cluster")
        {
            _sbCluster = cluster;
        }
    }

    public void OnClusterChanged(ClusterState cluster)
    {
        
    }

    public void OnClusterRemoved(ClusterState cluster)
    {
        if (cluster.ClusterId == "sb-cluster")
        {
            _sbCluster = null;
        }
    }
}

public interface IOfficialClusterStatus
{
    DestinationHealth Health { get; }
    IReadOnlyList<DestinationState>? Backups { get; }
}
