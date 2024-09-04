using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Transport;

namespace TransportNetwork.Lib.Interfaces;

public interface ITransportNetwork
{
    List<ITransportRoute> GetRoutes();
    List<List<Node>> GetPossiblePaths(Node start, Node end);
    List<Node> GetRecommendedPath(Node start, Node end);
    void AddPoint(string label);
    void AddRoute(int number, Node start);
    List<Node> GetPoints();
}