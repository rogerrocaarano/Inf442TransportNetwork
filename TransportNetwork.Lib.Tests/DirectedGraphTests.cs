using TransportNetwork.Lib.Graph;

namespace TransportNetwork.Lib.Tests;

public class DirectedGraphTests
{
    private DirectedGraph _graph;
    private List<Node> _nodes;

    [SetUp]
    public void Setup()
    {
        _graph = new DirectedGraph();
        _nodes =
        [
            new Node { Id = Guid.NewGuid(), Label = "A" },
            new Node { Id = Guid.NewGuid(), Label = "B" },
            new Node { Id = Guid.NewGuid(), Label = "C" },
            new Node { Id = Guid.NewGuid(), Label = "D" },
            new Node { Id = Guid.NewGuid(), Label = "E" },
            new Node { Id = Guid.NewGuid(), Label = "F" },
        ];
    }

    [Test]
    public void Test01_NumberOfNodes_EqualsNodesCount()
    {
        foreach (var n in _nodes)
        {
            _graph.AddNode(n);
        }

        Assert.That(_graph.Nodes, Has.Count.EqualTo(_nodes.Count));
    }

    [Test]
    public void Test02_AddNodeWithExistingId_ThrowsArgumentException()
    {
        _graph.AddNode(_nodes[0]);
        Assert.Throws<ArgumentException>(() => _graph.AddNode(_nodes[0]));
    }

    [Test]
    public void Test03_RemoveNodeWithExistingId_DecreasesNodesCount()
    {
        _graph.AddNode(_nodes[0]);
        _graph.RemoveNode(_nodes[0]);
        Assert.That(_graph.Nodes, Has.Count.EqualTo(0));
    }

    [Test]
    public void Test04_RemoveNodeWithNonExistingId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _graph.RemoveNode(_nodes[0]));
    }

    [Test]
    public void Test05_AddEdgeWithSourceNodeNotInGraph_ThrowsArgumentException()
    {
        _graph.AddNode(_nodes[1]);
        var source = _nodes[0];
        var destination = _nodes[1];
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination,
            Cost = 1
        };
        Assert.Throws<ArgumentException>(() => _graph.AddEdge(edge));
    }
    
    [Test]
    public void Test06_AddEdgeWithDestinationNodeNotInGraph_ThrowsArgumentException()
    {
        _graph.AddNode(_nodes[0]);
        var source = _nodes[0];
        var destination = _nodes[1];
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination,
            Cost = 1
        };
        Assert.Throws<ArgumentException>(() => _graph.AddEdge(edge));
    }
    
    [Test]
    public void Test07_AddEdgeWithExistingId_ThrowsArgumentException()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        var source = _nodes[0];
        var destination = _nodes[1];
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination,
            Cost = 1
        };
        _graph.AddEdge(edge);
        Assert.Throws<ArgumentException>(() => _graph.AddEdge(edge));
    }
    
    [Test]
    public void Test08_RemoveEdgeWithExistingId_DecreasesEdgesCount()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        var source = _nodes[0];
        var destination = _nodes[1];
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination,
            Cost = 1
        };
        _graph.AddEdge(edge);
        _graph.RemoveEdge(edge);
        Assert.That(_graph.Edges, Has.Count.EqualTo(0));
    }
    
    [Test]
    public void Test09_RemoveEdgeWithNonExistingId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _graph.RemoveEdge(new Edge()));
    }
    
    [Test]
    public void Test10_UpdateNodeWithExistingId_UpdatesNode()
    {
        _graph.AddNode(_nodes[0]);
        var node = _nodes[0];
        node.Label = "Z";
        _graph.UpdateNode(node);
        Assert.That(_graph.Nodes[0].Label, Is.EqualTo("Z"));
    }
    
    [Test]
    public void Test11_UpdateNodeWithNonExistingId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _graph.UpdateNode(new Node()));
    }
    
    [Test]
    public void Test12_GetNodeByIdWithExistingId_ReturnsNode()
    {
        _graph.AddNode(_nodes[0]);
        var node = _graph.GetNodeById(_nodes[0].Id);
        Assert.That(node, Is.EqualTo(_nodes[0]));
    }
    
    [Test]
    public void Test13_GetNodeByIdWithNonExistingId_ReturnsNull()
    {
        var node = _graph.GetNodeById(Guid.NewGuid());
        Assert.That(node, Is.Null);
    }
    
    [Test]
    public void Test14_GetEdgeByIdWithExistingId_ReturnsEdge()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        var source = _nodes[0];
        var destination = _nodes[1];
        var edge = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination,
            Cost = 1
        };
        _graph.AddEdge(edge);
        var e = _graph.GetEdgeById(edge.Id);
        Assert.That(e, Is.EqualTo(edge));
    }
    
    [Test]
    public void Test15_GetEdgeByIdWithNonExistingId_ReturnsNull()
    {
        var edge = _graph.GetEdgeById(Guid.NewGuid());
        Assert.That(edge, Is.Null);
    }
    
    [Test]
    public void Test16_GetNeighboursWithExistingNode_ReturnsNeighbours()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        _graph.AddNode(_nodes[2]);
        var source = _nodes[0];
        var destination1 = _nodes[1];
        var destination2 = _nodes[2];
        var edge1 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination1,
            Cost = 1
        };
        var edge2 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = destination2,
            Cost = 1
        };
        _graph.AddEdge(edge1);
        _graph.AddEdge(edge2);
        var neighbours = _graph.GetNeighbours(source);
        Assert.That(neighbours, Has.Count.EqualTo(2));
    }
    
    [Test]
    public void Test17_GetNeighboursWithNonExistingNode_ReturnsEmptyList()
    {
        var neighbours = _graph.GetNeighbours(new Node());
        Assert.That(neighbours, Has.Count.EqualTo(0));
    }
    
    [Test]
    public void Test18_DepthFirstSearchAlgorithmWithExistingRoute_ReturnsRoute()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        _graph.AddNode(_nodes[2]);
        _graph.AddNode(_nodes[3]);
        _graph.AddNode(_nodes[4]);
        _graph.AddNode(_nodes[5]);
        var source = _nodes[0];
        var destination = _nodes[5];
        var edge1 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = _nodes[1],
            Cost = 1
        };
        var edge2 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[1],
            Destination = _nodes[2],
            Cost = 1
        };
        var edge3 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[2],
            Destination = _nodes[3],
            Cost = 1
        };
        var edge4 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[3],
            Destination = _nodes[4],
            Cost = 1
        };
        var edge5 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[4],
            Destination = destination,
            Cost = 1
        };
        _graph.AddEdge(edge1);
        _graph.AddEdge(edge2);
        _graph.AddEdge(edge3);
        _graph.AddEdge(edge4);
        _graph.AddEdge(edge5);
        var routes = _graph.DepthFirstSearchAlgorithm(source, destination);
        Assert.That(routes, Has.Count.EqualTo(1));
    }
    
    [Test]
    public void Test19_DepthFirstSearchAlgorithmWithNonExistingRoute_ReturnsEmptyList()
    {
        _graph.AddNode(_nodes[0]);
        _graph.AddNode(_nodes[1]);
        _graph.AddNode(_nodes[2]);
        _graph.AddNode(_nodes[3]);
        _graph.AddNode(_nodes[4]);
        _graph.AddNode(_nodes[5]);
        var source = _nodes[0];
        var destination = _nodes[5];
        var edge1 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = source,
            Destination = _nodes[1],
            Cost = 1
        };
        var edge2 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[1],
            Destination = _nodes[2],
            Cost = 1
        };
        var edge3 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[2],
            Destination = _nodes[3],
            Cost = 1
        };
        var edge4 = new Edge
        {
            Id = Guid.NewGuid(),
            Source = _nodes[3],
            Destination = _nodes[4],
            Cost = 1
        };
        _graph.AddEdge(edge1);
        _graph.AddEdge(edge2);
        _graph.AddEdge(edge3);
        _graph.AddEdge(edge4);
        var routes = _graph.DepthFirstSearchAlgorithm(source, destination);
        Assert.That(routes, Has.Count.EqualTo(0));
    }

    [Test]
    public void Test20_DepthFirstSearchAlgorithmWithMultipleExistingRoute_ReturnsRoutes()
    {
        for (var i = 0; i < 4; i++)
        {
            _graph.AddNode(_nodes[i]);
        }
        _graph.AddEdge(
            new Edge
            {
                Id = Guid.NewGuid(),
                Source = _nodes[0],
                Destination = _nodes[1],
                Cost = 1
            }
        );
        _graph.AddEdge(
            new Edge
            {
                Id = Guid.NewGuid(),
                Source = _nodes[1],
                Destination = _nodes[2],
                Cost = 1
            }
        );
        _graph.AddEdge(
            new Edge
            {
                Id = Guid.NewGuid(),
                Source = _nodes[1],
                Destination = _nodes[3],
                Cost = 1
            }
        );
        _graph.AddEdge(
            new Edge
            {
                Id = Guid.NewGuid(),
                Source = _nodes[2],
                Destination = _nodes[3],
                Cost = 1
            }
        );
        var route1 = new List<Node> { _nodes[0], _nodes[1], _nodes[2], _nodes[3] };
        var route2 = new List<Node> { _nodes[0], _nodes[1], _nodes[3] };
        var routes = _graph.DepthFirstSearchAlgorithm(_nodes[0], _nodes[3]);
        Assert.That(routes, Has.Count.EqualTo(2));
        Assert.That(routes, Has.One.EqualTo(route1));
        Assert.That(routes, Has.One.EqualTo(route2));
    }
    
    
}