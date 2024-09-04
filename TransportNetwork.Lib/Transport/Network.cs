using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Interfaces;

namespace TransportNetwork.Lib.Transport;

public class Network : ITransportNetwork
{
    private DirectedGraph _graph = new DirectedGraph();
    private List<ITransportRoute> _routes = [];

    /// <summary>
    /// Este método es una implementación modificada del algoritmo de Dijkstra, descrito en el artículo "Public
    /// transport route planning: Modified dijkstra's algorithm" (https://www.researchgate.net/publication/320829561_
    /// Public_transport_route_planning_Modified_dijkstra%27s_algorithm) cuyo objetivo es encontrar la ruta más corta
    /// entre dos nodos de un grafo ponderado, teniendo en cuenta las líneas de transporte público que pasan por cada
    /// nodo y aplicando penalizaciones en función de si es necesario caminar o cambiar de línea.
    /// </summary>
    /// <param name="weightedGraphMatrix"></param>
    /// <param name="lineMatrix"></param>
    /// <param name="sourceIndex"></param>
    /// <returns></returns>
    public (List<Node>, List<float>) ModifiedDijkstraAlgorithm(
        float[,] weightedGraphMatrix,
        List<Line>[,] lineMatrix,
        int sourceIndex)
    {
        var n = weightedGraphMatrix.GetLength(0);
        var cost = new List<float>(n);
        var path = new List<Node>(n);
        var lines = new List<List<Line>>(n);
        var unvisited = new List<Node>();

        for (var node = 0; node < n; node++)
        {
            cost[node] = float.PositiveInfinity;
            unvisited.Add(_graph.Nodes[node]);
        }

        cost[sourceIndex] = 0;

        while (unvisited.Count > 0)
        {
            var minCost = cost.Where((c, i) => unvisited.Contains(_graph.Nodes[i])).Min();
            var current = cost.IndexOf(minCost);
            unvisited.Remove(_graph.Nodes[current]);

            foreach (var adjacentNode in _graph.GetNeighbours(_graph.Nodes[current]))
            {
                var adjacent = _graph.Nodes.FindIndex(node => node == adjacentNode);
                var (penalty, intersectedLines) = PenaltyFunction(lines[current], lineMatrix[current, adjacent]);
                var alternativeCost = cost[current] + weightedGraphMatrix[current, adjacent] + penalty;
                if (alternativeCost < cost[adjacent])
                {
                    cost[adjacent] = alternativeCost;
                    path[adjacent] = path[current];
                    lines[adjacent] = intersectedLines;
                }
            }
        }

        return (path, cost);
    }
    
    float WALKING_PENALTY = 10f;
    float TRANSFER_PENALTY = 1.0f;

    /// <summary>
    /// Este método calcula la penalización a aplicar en función de si es necesario caminar o cambiar de línea, es un
    /// método auxiliar del algoritmo de Dijkstra modificado.
    /// </summary>
    /// <param name="currentLines"></param>
    /// <param name="adjacentLines"></param>
    /// <returns></returns>
    private (float, List<Line>) PenaltyFunction(List<Line> currentLines, List<Line> adjacentLines)
    {
        var intersectedLines = new List<Line>();
        float penalty = 0;
        if (currentLines.Count == 0)
        {
            if (adjacentLines.Count == 0)
            {
                penalty = WALKING_PENALTY;
            }
            else
            {
                penalty = TRANSFER_PENALTY;
                intersectedLines = adjacentLines;
            }
        }
        else
        {
            foreach (var line in currentLines)
            {
                if (adjacentLines.Contains(line))
                {
                    intersectedLines.Add(line);
                }
            }

            if (intersectedLines.Count == 0)
            {
                penalty = TRANSFER_PENALTY;
                intersectedLines = adjacentLines;
            }
        }

        return (penalty, intersectedLines);
    }

    public List<ITransportRoute> GetRoutes()
    {
        return _routes;
    }

    public List<List<Node>> GetPossiblePaths(Node start, Node end)
    {
        throw new NotImplementedException();
    }

    public List<Node> GetRecommendedPath(Node start, Node end)
    {
        throw new NotImplementedException();
    }

    public void AddPoint(string label)
    {
        var node = new Node
        {
            Id = Guid.NewGuid(),
            Label = label
        };
        _graph.AddNode(node);
    }

    public void AddRoute(int number, Node start)
    {
        var route = new Line
        {
            Number = number,
            FirstStop = start
        };
        _routes.Add(route);
    }

    public List<Node> GetPoints()
    {
        return _graph.Nodes;
    }
}