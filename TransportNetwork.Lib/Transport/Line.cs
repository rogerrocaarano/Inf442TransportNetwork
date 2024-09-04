using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Interfaces;

namespace TransportNetwork.Lib.Transport;

public class Line: ITransportRoute
{
    public int Number { get; set; }
    public List<Edge> Route { get; set; } = [];
    public Node FirstStop { get; set; }
    public void AddRouteSegment(Node start, Node end, float cost)
    {
        throw new NotImplementedException();
    }

    public void AddStop(Node node, float cost)
    {
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = Route.Last().Destination,
            Destination = node,
            Cost = cost
        };
        Route.Add(edge);
    }

    public List<Node> GetRoute()
    {
        var route = new List<Node>();
        route.Add(FirstStop);
        foreach (var edge in Route)
        {
            route.Add(edge.Destination);
        }

        return route;
    }
}