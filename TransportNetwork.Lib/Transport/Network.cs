using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Interfaces;

namespace TransportNetwork.Lib.Transport;

public class Network : ITransportNetwork
{
    private DirectedGraph _graph = new DirectedGraph();
    private List<BusLine> _busLines = [];

    public List<List<Node>> GetPossibleRoutes(Node start, Node end)
    {
        throw new NotImplementedException();
    }

    public List<Node> GetShortestRoute(Node start, Node end)
    {
        throw new NotImplementedException();
    }

    public int GetRouteCost(List<Node> route)
    {
        throw new NotImplementedException();
    }

    public void AddBusLines(List<BusLine> busLines)
    {
        throw new NotImplementedException();
    }

    
}