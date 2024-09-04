using TransportNetwork.Lib.Graph;

namespace TransportNetwork.Lib.Interfaces;

public interface ITransportRoute
{
    void AddRouteSegment(Node start, Node end, float cost);
    void AddStop(Node node, float cost);
    List<Node> GetRoute();
}