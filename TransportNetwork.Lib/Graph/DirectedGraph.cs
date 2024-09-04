using TransportNetwork.Lib.Interfaces;

namespace TransportNetwork.Lib.Graph;

public class DirectedGraph : IGraph
{
    public List<Node> Nodes { get; } = [];
    public List<Edge> Edges { get; } = [];

    public void AddNode(Node node)
    {
        if (GetNodeById(node.Id) == null)
        {
            Nodes.Add(node);
        }
        else
        {
            throw new ArgumentException("Node with this id already exists");
        }
    }

    public void AddEdge(Edge edge)
    {
        if (GetEdgeById(edge.Id) != null)
        {
            throw new ArgumentException("Edge with this id already exists");
        }

        if (GetNodeById(edge.Source.Id) == null)
        {
            throw new ArgumentException("Source node does not exist");
        }

        if (GetNodeById(edge.Destination.Id) == null)
        {
            throw new ArgumentException("Destination node does not exist");
        }

        Edges.Add(edge);
    }

    public void RemoveNode(Node node)
    {
        if (GetNodeById(node.Id) != null)
        {
            Nodes.Remove(node);
        }
        else
        {
            throw new ArgumentException("Node with this id does not exist");
        }
    }

    public void RemoveEdge(Edge edge)
    {
        if (GetEdgeById(edge.Id) != null)
        {
            Edges.Remove(edge);
        }
        else
        {
            throw new ArgumentException("Edge with this id does not exist");
        }
    }

    public void UpdateNode(Node node)
    {
        if (GetNodeById(node.Id) != null)
        {
            Nodes[Nodes.FindIndex(n => n.Id == node.Id)] = node;
        }
        else
        {
            throw new ArgumentException("Node with this id does not exist");
        }
    }

    public void UpdateEdge(Edge edge)
    {
        if (GetEdgeById(edge.Id) != null)
        {
            Edges[Edges.FindIndex(e => e.Id == edge.Id)] = edge;
        }
        else
        {
            throw new ArgumentException("Edge with this id does not exist");
        }
    }

    public Node? GetNodeById(Guid id)
    {
        return Nodes.FirstOrDefault(n => n.Id == id);
    }

    public Edge? GetEdgeById(Guid id)
    {
        return Edges.FirstOrDefault(e => e.Id == id);
    }

    public List<List<Node>> DepthFirstSearchAlgorithm(
        Node start,
        Node end,
        List<Node>? visitedNodes = null,
        List<List<Node>>? foundRoutes = null)
    {
        visitedNodes ??= [];
        foundRoutes ??= [];

        visitedNodes.Add(start);

        if (start == end)
        {
            foundRoutes.Add(visitedNodes);
        }

        foreach (var node in GetNeighbours(start))
        {
            DepthFirstSearchAlgorithm(node, end, visitedNodes.ToList(), foundRoutes);
        }

        return foundRoutes;
    }

    public List<Node> GetNeighbours(Node node)
    {
        return Edges
            .Where(e => e.Source == node)
            .Select(e => e.Destination)
            .ToList();
    }
    
    
}