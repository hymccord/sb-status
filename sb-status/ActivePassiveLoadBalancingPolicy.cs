using Yarp.ReverseProxy.LoadBalancing;
using Yarp.ReverseProxy.Model;
using Yarp.ReverseProxy.Utilities;

namespace sb_status;

public class ActivePassiveLoadBalancingPolicy : ILoadBalancingPolicy
{
    private readonly IRandomFactory _randomFactory;

    public ActivePassiveLoadBalancingPolicy(IRandomFactory randomFactory)
    {
        _randomFactory = randomFactory;
    }

    public string Name => "ActivePassive";

    public DestinationState? PickDestination(HttpContext context, ClusterState cluster, IReadOnlyList<DestinationState> availableDestinations)
    {
        if (availableDestinations.Count == 0)
        {
            return null;
        }

        var officialDestination = availableDestinations.FirstOrDefault(d => d.DestinationId == "official");
        if (officialDestination is not null)
        {
            return officialDestination;
        }

        // Pick two, and then return the least busy. This avoids the effort of searching the whole list, but
        // still avoids overloading a single destination.
        var destinationCount = availableDestinations.Count;
        var random = _randomFactory.CreateRandomInstance();
        var first = availableDestinations[random.Next(destinationCount)];
        var second = availableDestinations[random.Next(destinationCount)];
        return (first.ConcurrentRequestCount <= second.ConcurrentRequestCount) ? first : second;
    }
}
