using TransportNetwork.Lib.Graph;
using TransportNetwork.Lib.Transport;

namespace TransportNetwork.Lib.Tests;

public class TransportNetworkTests
{
    private Network _network;
    private List<string> _labels;
    
    [SetUp]
    public void Setup()
    {
        _network = new Network();
        _labels = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];
        foreach (var label in _labels)
        {
            _network.AddPoint(label);
        }
        var nodes = _network.GetPoints();
        _network.AddRoute(1, nodes.Find(n => n.Label == "A"));
        _network.AddRoute(2, nodes.Find(n => n.Label == "A"));
        _network.AddRoute(3, nodes.Find(n => n.Label == "A"));
        _network.AddRoute(4, nodes.Find(n => n.Label == "A"));
        _network.AddRoute(5, nodes.Find(n => n.Label == "A"));
        
        var routes = _network.GetRoutes();
        
    }
    
    [Test]
    public void Test01_PointsCount()
    {
        Assert.That(_network.GetPoints(), Has.Count.EqualTo(_labels.Count));
    }
    
    [Test]
    public void Test02_RoutesCount()
    {

    }
}