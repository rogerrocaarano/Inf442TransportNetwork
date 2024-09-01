using TransportNetwork.Lib.Graph;

namespace TransportNetwork.Lib.Interfaces;

public interface IGraph
{
    void AddNode(Node node);
    void AddEdge(Edge edge);
    void RemoveNode(Node node);
    void RemoveEdge(Edge edge);
    void UpdateNode(Node node);
    void UpdateEdge(Edge edge);
}