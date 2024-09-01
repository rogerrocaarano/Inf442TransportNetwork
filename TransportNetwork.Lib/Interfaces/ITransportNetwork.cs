using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Transport;

namespace TransportNetwork.Lib.Interfaces;

public interface ITransportNetwork
{
    List<List<Node>> GetPossibleRoutes(Node start, Node end);
    List<Node> GetShortestRoute(Node start, Node end);
    int GetRouteCost(List<Node> route);
    void AddBusLines(List<BusLine> busLines);
}